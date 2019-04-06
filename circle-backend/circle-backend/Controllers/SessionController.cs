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
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        [HttpGet("{sessionId}")]
        public Session GetSession(int sessionId)
        {
            return _sessionRepository.GetSessionById(sessionId);
        }

        [HttpGet("{sessionId}/code")]
        public string GetCode(int sessionId)
        {
            return _sessionRepository.GetCodeForSession(sessionId);
        }

        [HttpPut("{sessionId}/{code}")]
        public void InsertSession(int sessionId, string code)
        {
            _sessionRepository.InsertSession(sessionId,code);
        }

        [HttpPut("{sessionId}/insertMessage")]
        public void InsertTextMessage(int sessionId,[FromBody]TextMessage textMessage)
        {
            _sessionRepository.InsertTextMessageForSession(sessionId, textMessage);
        }

        [HttpPut("{sessionId}/insertDrawing")]
        public void InsertDrawingMessage(int sessionId, [FromBody]DrawingMessage drawingMessage)
        {
            _sessionRepository.InsertDrawingMessageForSession(sessionId, drawingMessage);
        }

        [HttpGet("{sessionId}/drawingmessages")]
        public List<DrawingMessage> GetDrawingMessagesForSession(int sessionId)
        {
            return _sessionRepository.GetDrawingMessagesForSession(sessionId);
        }

        [HttpGet("{sessionId}/textmessages")]
        public List<TextMessage> GetTextMessagesForSession(int sessionId)
        {
            return _sessionRepository.GetTextMessagesForSession(sessionId);
        }

        [HttpDelete("{sessionId}")]
        public void Delete(int sessionId)
        {
            _sessionRepository.DeleteSession(sessionId);
        }
    }
}
