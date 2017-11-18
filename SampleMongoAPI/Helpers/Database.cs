using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using SampleMongoAPI.Models;
using System.Configuration;
using MongoDB.Driver.Builders;

namespace SampleMongoAPI.Helpers
{
    public class Database
    {
        public static string connectionString = ConfigurationManager.AppSettings["connString"];

        public Result Get()
        {
            Result res = new Result();
            try
            {
                MongoCollection collection = GetCollection();
                IQueryable<Users2> queryAll = collection.AsQueryable<Users2>();
                List<Users2> query = queryAll.ToList();

                res.code = 0;
                res.success = true;
                if (query.Count > Int32.MaxValue)
                {
                    res.data = query.Take(Int32.MaxValue - 1);
                }
                else { res.data = query; }
                res.message = "Queried successfully. Values count shown are up to Int32.MaxValue - 1";
            }
            catch(Exception ex)
            {
                res.code = 1;
                res.success = false;
                res.data = "";
                res.message = ex.ToString();
            }
            return res;
        }

        private MongoCollection GetCollection()
        {

                MongoClient client = new MongoClient(connectionString);
                MongoServer server = client.GetServer();
                MongoDatabase database = server.GetDatabase("testdb1");
                MongoCollection symbolcollection = database.GetCollection<Users2>("Users2");
                //query = symbolcollection.AsQueryable<Users2>();
         
            return symbolcollection;
        }

        public Result Get(string queryname) //need to change to string because by default, _id of mongoDB is string
        {
            Result res = new Result();
            try
            {
                MongoClient client = new MongoClient(connectionString);
                MongoServer server = client.GetServer();
                MongoDatabase database = server.GetDatabase("testdb1");
                MongoCollection symbolcollection = database.GetCollection<Users2>("Users2");
                List<Users2> query = symbolcollection.AsQueryable<Users2>().Where<Users2>(sb => sb.Name == queryname).ToList();

                res.code = 0;
                res.success = true;
                res.data = query.Take<Users2>(99999); //maxJsonLength propery is limited up to 100k but that should not affect the duration of getting the whole.
                res.message = "Queried successfully";
            }
            catch (Exception ex)
            {
                res.code = 1;
                res.success = false;
                res.data = "";
                res.message = ex.ToString();
            }
            return res;
        }

        public Result Get(string name, string location) //need to change to string because by default, _id of mongoDB is string
        {
            Result res = new Result();
            try
            {
                MongoCollection collection = GetCollection();
                IQueryable<Users2> queryAll = collection.AsQueryable<Users2>();
                List<Users2> query = queryAll.Where<Users2>(sb => sb.Name == name && sb.Location == location).ToList();

                res.code = 0;
                res.success = true;
                res.data = query.Take<Users2>(99999); //maxJsonLength propery is limited up to 100k but that should not affect the duration of getting the whole.
                res.message = "Queried successfully";
            }
            catch (Exception ex)
            {
                res.code = 1;
                res.success = false;
                res.data = "";
                res.message = ex.ToString();
            }
            return res;
        }

        public Result Post(string name, string location)
        {
            Result res = new Result();
            try
            {
                MongoCollection collection = GetCollection();

                Users2 insertUser = new Models.Users2();
                insertUser.Name = name;
                insertUser.Location = location;

                collection.Insert(insertUser);

                res.code = 0;
                res.success = true;
                res.message = "Inserted successfully";
            }
            catch (Exception ex)
            {
                res.code = 1;
                res.success = false;
                res.data = "";
                res.message = ex.ToString();
            }
            return res;
        }

        public Result Delete(string _idd)
        {
            Result res = new Result();
            try
            {
                MongoCollection collection = GetCollection();
                var querydel = collection.Remove(Query.EQ("_id", new ObjectId(_idd)));

                res.code = 0;
                res.success = true;
                res.message = "Deleted successfully.";
            }
            catch (Exception ex)
            {
                res.code = 1;
                res.success = false;
                res.data = "";
                res.message = ex.ToString();
            }
            return res;
        }
    }
}