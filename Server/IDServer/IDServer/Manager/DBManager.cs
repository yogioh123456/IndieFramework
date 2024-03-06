using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDServer
{
    public class DBManager
    {
        private string connStr = "mongodb://127.0.0.1:27017";//本地
        //private string connStr = "administrator:6716057SanYe@mongodb://39.99.131.198:27017";
        IMongoDatabase mongoDatabase;
        private int index = 0;

        public DBManager()
        {
            Debug.Log("链接数据库 xian");
            //链接数据库xian
            var client = new MongoClient(connStr);
            mongoDatabase = client.GetDatabase("xian");

            IMongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>("user");
            List<BsonDocument> res = collection.Find(new BsonDocument()).ToList();
            index = res.Count + 1;
        }

        //通过用户名查询用户
        public BsonDocument GetUser(string name)
        {
            //查询user表
            IMongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>("user");
            List<BsonDocument> res = collection.Find(new BsonDocument()).ToList(); //查询整个数据集
            foreach (var item in res)
            {
                if (item["name"].Equals(name))
                {
                    Debug.Log("用户存在" + name);
                    return item;
                }
            }
            Debug.Log("用户不存在");
            return null;
        }

        public BsonDocument SetUserData(int id, string name, string password)
        {
            IMongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>("user");
            var document = new BsonDocument { { "id", id }, { "name", name }, { "password", password } };
            collection.InsertOne(document);
            Debug.Log("数据录入成功");
            return document;
        }

        public bool CreateUser(string name, string password)
        {
            BsonDocument user = GetUser(name);
            if (user == null)
            {
                user = SetUserData(index, name, password);
                index++;
                Game.GetComp<UserManager>().AddUser(user);
                return true;
            }
            else
            {
                Game.GetComp<UserManager>().AddUser(user);
                return false;
            }
        }
    }
}
