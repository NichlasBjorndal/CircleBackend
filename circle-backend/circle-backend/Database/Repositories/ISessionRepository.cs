using System;
using System.Collections.Generic;
using circle_backend.Models;

namespace circle_backend.Database.Repositories
{
    public interface ISessionRepository : IDisposable
    {
        void InsertSession(int sessionId, string code);
        void DeleteSession(int sessionId);
        Session GetSessionById(int sessionId);
        string GetCodeForSession(int sessionId);
        void InsertStartedStatusForSession(int sessionId, bool hasStarted);
        bool GetStartedStatusForSession(int sessionId);
        void InsertDrawingMessageForSession(int sessionId, DrawingMessage drawingMessage);
        List<DrawingMessage> GetDrawingMessagesForSession(int sessionId);
        void InsertTextMessageForSession(int sessionId, TextMessage textMessage);
        List<TextMessage>  GetTextMessagesForSession(int sessionId);
    }
}
