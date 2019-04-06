using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using circle_backend.Database.Repositories;
using circle_backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace circle_backend.Controllers
{
    [Route("api/[controller]")]
    public class SessionsController : Controller
    {
        readonly ISessionRepository sessionRepository;

        public SessionsController(ISessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        [HttpGet("{sessionId}")]
        public Session GetSession(int sessionId)
        {
            return sessionRepository.GetSessionById(sessionId);
        }

        [HttpGet("{sessionId}/code")]
        public string GetCode(int sessionId)
        {
            return sessionRepository.GetCodeForSession(sessionId);
        }

        [HttpPut("{sessionId}/{code}")]
        public void Put(int sessionId, string code)
        {
            sessionRepository.InsertSession(sessionId,code);
            sessionRepository.InsertTextMessageForSession(sessionId, new TextMessage()
            {
                    
            });
        }

        [HttpDelete("{sessionId}")]
        public void Delete(int sessionId)
        {
            sessionRepository.DeleteSession(sessionId);
        }
    }
}
