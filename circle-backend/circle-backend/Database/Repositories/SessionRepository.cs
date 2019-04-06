using System;
using System.Collections.Generic;
using System.Linq;
using circle_backend.Models;
using circle_backend.Utilities;

namespace circle_backend.Database.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        CircleDbContext context;

        public SessionRepository(CircleDbContext context)
        {
            this.context = context;
        }

        public void InsertSession(int sessionId, string code)
        {
            context.Add(new Session
            {
                SessionId = sessionId,
                Code = code,
                HasStarted = false,
                TextMessages = new List<TextMessage>(),
                DrawingMessages = new List<DrawingMessage>()
            });
            context.SaveChanges();
        }

        public void DeleteSession(int sessionId)
        {
            context.Sessions.Remove(context.Sessions.Find(sessionId));
            context.SaveChanges();
        }

        public Session GetSessionById(int sessionId)
        {
            return context.Sessions.Find(sessionId);
        }

        public string GetCodeForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).Code;
        }

        public void InsertStartedStatusForSession(int sessionId, bool hasStarted)
        {
            var session = context.Sessions.Find(sessionId);
            session.HasStarted = hasStarted;
            context.SaveChanges();
        }

        public bool GetStartedStatusForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).HasStarted;
        }

        public void InsertDrawingMessageForSession(int sessionId, DrawingMessage drawingMessage)
        {
            var session = context.Sessions.Find(sessionId).DrawingMessages.Append(drawingMessage);
            context.SaveChanges();
        }

        public List<DrawingMessage> GetDrawingMessagesForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).DrawingMessages;
        }

        public void InsertTextMessageForSession(int sessionId, TextMessage textMessage)
        {
            var session = context.Sessions.Find(sessionId).TextMessages.Append(textMessage);
            context.SaveChanges();
        }

        public List<TextMessage> GetTextMessagesForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).TextMessages;
        }

        #region IDisposable Support
        bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposedValue = true;
            }

            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}