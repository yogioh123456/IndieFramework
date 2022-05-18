using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDServer
{
    public class UserManager
    {
        private List<BsonDocument> UserList = new List<BsonDocument>();

        public void AddUser(BsonDocument user)
        {
            UserList.Add(user);
        }
    }
}
