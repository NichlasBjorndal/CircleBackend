using System;
using System.Collections.Generic;
using System.Linq;
using circle_backend.Models;

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
            context.Add(new Session()
            {
                SessionId = sessionId,
                Code = code,
                HasStarted = false,
                TextMessages = new List<TextMessage>(),
                DrawingMessages = new List<DrawingMessage>()
            });
        }
		
        public void DeleteSession(int sessionId)
        {
            context.Sessions.Remove(context.Sessions.Find(sessionId));
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
        }

        public bool GetStartedStatusForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).HasStarted;
        }

        public void InsertDrawingMessageForSession(int sessionId, DrawingMessage drawingMessage)
        {
            var session = context.Sessions.Find(sessionId);
            session.DrawingMessages.Append(drawingMessage);
        }

        public IEnumerable<DrawingMessage> GetDrawingMessagesForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).DrawingMessages;
        }

        public void InsertTextMessageForSession(int sessionId, TextMessage textMessage)
        {
            var session = context.Sessions.Find(sessionId);
            session.TextMessages.Append(textMessage);
        }

        public IEnumerable<TextMessage> GetTextMessagesForSession(int sessionId)
        {
            return context.Sessions.Find(sessionId).TextMessages;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

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
