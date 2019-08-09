using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WCFChat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                OperationContext = OperationContext.Current
            };
            nextId++;
            SendMessage(user.Name + " подключился к чату!", 0);

            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(u => u.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage(user.Name + "покинул чат!", 0);
            }
        }

        public void SendMessage(string msg, int id)
        {
            foreach (var user in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var sender = users.FirstOrDefault(u => u.ID == id);
                if (sender != null)
                {
                    answer += ": " + user.Name + " ";
                }
                answer += msg;

                user.OperationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}
