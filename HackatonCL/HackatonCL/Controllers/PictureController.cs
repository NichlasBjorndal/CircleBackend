using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HackatonCL.hubs;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HackatonCL.Controllers
{
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        private IHubContext<ChatHub> messageHubContext;

        public PictureController(IHubContext<ChatHub> messageHubContext)
        {
            this.messageHubContext = messageHubContext;
        }

        [HttpGet("api/group/{groupName}")]
        public IEnumerable<string> Getgroup(string groupName)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("guessedWord/{user}/{guessedWord}")]
        public async Task<IActionResult> Post(string guessedWord,string user)
        {
            await messageHubContext.Clients.All.SendAsync("ReceiveMessage", user, guessedWord);
            return Ok();
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetAsync(string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files\\images", fileName);
            if (System.IO.File.Exists(path))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;

                return Ok(File(memory, "application/octet-stream", fileName).FileStream);

            }

            return NoContent();
        }

        [HttpPost("/api/pictureUpload")]
        public async Task<string> Post(IFormFile file)
        {
            //write to Files file
            if (file == null || file.Length == 0)
                return" file not selected";

            var datatype = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            //lets pray to god these guids dont overlap and causes a crash =C
            Guid guid = Guid.NewGuid();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Files\\images", guid.ToString()+".png");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return guid.ToString() + ".png";


        }

    }
}