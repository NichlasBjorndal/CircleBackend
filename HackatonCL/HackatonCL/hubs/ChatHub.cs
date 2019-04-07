using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackatonCL.repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Internal;

namespace HackatonCL.hubs
{
    public class ChatHub : Hub
    {
        private GroupManager groupManager;

        private string[] words = new[]
        {
            "cat", "sun", "cup", "ghost", "flower", "pie", "cow", "banana", "snowflake", "bug", "book", "jar", "snake",
            "light", "tree", "lips", "apple", "slide", "socks", "smile", "swing", "coat", "shoe", "water", "heart",
            "hat", "ocean", "kite", "dog", "mouth", "milk", "duck", "eyes", "skateboard", "bird", "boy", "apple",
            "person", "girl", "mouse", "ball", "house", "star", "nose", "bed", "whale", "jacket", "shirt", "hippo",
            "beach", "egg", "face", "cookie", "cheese", "ice", "cream"
        };

        public ChatHub(GroupManager groupManager)
        {
            this.groupManager = groupManager;
        }
        
        public async Task SendMessage(string message)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", Context.ConnectionId, message);
            
            await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, message);

        }

        public async Task SendMessageToGroup(string message, string roomname)
        {

           await Clients.Group(groupName: roomname).SendAsync("ReceiveMessage", Context.ConnectionId, "PROCEED");
        }
        
        public async Task InitiateGame(string roomname)
        {
            await Clients.All.SendAsync("INITIATE");
           await Clients.Group(groupName: roomname).SendAsync("ReceiveMessage", Context.ConnectionId, "INITIATE");
        }

        public async Task InitiateGameClient()
        {
            var count = words.Length;
            var index = new Random().Next(count);
            var word = words[index];

            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", Context.ConnectionId, "DRAW|" +word);
        }

        public async Task SendMessageToTarget(string message, string roomname)
        {
            var users = groupManager._groups.GetValueOrDefault(roomname);

            var count = users.Count;
            var pos = users.IndexOf(Context.ConnectionId);
            if (count-1 == pos)
            {
                var user = users.ElementAt(0);
                await Clients.Client(connectionId: user).SendAsync("ReceiveMessage", Context.ConnectionId, message);
                return;
            }
            else
            {

                var user = users.ElementAt(pos + 1);

                await Clients.Client(connectionId: user).SendAsync("ReceiveMessage", Context.ConnectionId, message);
                return;
            }

        }

        public async Task JoinRoom(string roomname)
        {

            if (!groupManager._groups.ContainsKey(roomname))
            {
                groupManager._groups.Add(roomname, new List<string>{ Context.ConnectionId});
            }
            else
            {

                var added = groupManager._groups[roomname];
                if (!added.Contains(Context.ConnectionId))
                {

                    added.Add(Context.ConnectionId);
                    groupManager._groups[roomname] = added;
                }

                
            }


            groupManager._groups.Count();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomname);
            await Clients.Group(groupName: roomname).SendAsync("ReceiveMessage", Context.ConnectionId,roomname);

        }

        public Task LeaveRoom(string roomname)
        {
            var added = groupManager._groups.GetValueOrDefault(roomname);
            added.Remove(Context.ConnectionId);
            groupManager._groups.Add(roomname, added);

            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomname);
        }

    }
}
