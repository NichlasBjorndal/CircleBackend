using System;
using System.Collections.Generic;
using circle_backend.Models;

namespace circle_backend.Database.Repositories
{
    public interface ISessionRepository : IDisposable
    {
        void InsertSession(int sessionId, string code);
        Session GetSessionById(int sessionId);
        string GetCodeForSession(int sessionId);
        void InsertStartedStatusForSession(int sessionId, bool hasStarted);
        bool GetStartedStatusForSession(int sessionId);
        void InsertDrawingMessageForSession(int sessionId, DrawingMessage drawingMessage);
        IEnumerable<DrawingMessage> GetDrawingMessagesForSession(int sessionId);
        void InsertTextMessageForSession(int sessionId, TextMessage textMessage);
        IEnumerable<TextMessage>  GetTextMessagesForSession(int sessionId);
        void SaveChanges();

    }
}
