using System;
using circle_backend.Models;

namespace circle_backend.Database.Repositories
{
    public interface IUserRepository : IDisposable
    {
        void AddUser(User user);
        User GetUserById(int userId);
        void SetUserReadyStatus(int userId, bool readyStatus);
        void SetUserCompleteStatus(int userId, bool turnCompleteStatus);
        void RemoveUser(int userId);
        void Save();
    }
}
