using System;
using circle_backend.Models;

namespace circle_backend.Database.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly CircleDbContext _context;

        public UserRepository(CircleDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public User GetUserById(int userId) 
        {
            User user = _context.Users.Find(userId);
            return user;
        }

        public void SetUserReadyStatus(int userId, bool readyStatus) 
        {
            User user = _context.Users.Find(userId);
            user.Ready = readyStatus;
        }

        public void SetUserCompleteStatus(int userId, bool turnCompleteStatus)
        {
            User user = _context.Users.Find(userId);
            user.TurnComplete = turnCompleteStatus;
        }

        public void RemoveUser(int userId)
        {
            User user = _context.Users.Find(userId);
            _context.Users.Remove(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
