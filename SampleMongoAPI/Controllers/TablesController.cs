using SampleMongoAPI.Helpers;
using SampleMongoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SampleMongoAPI.Controllers
{
    public class TablesController : AsyncController
    {
        // GET: Tables
        [System.Web.Http.HttpGet]
        public ActionResult Index()
        {
            Database db = new Database();
            Result res = new Result();
            res = db.Get();

            if (Response != null)
            {
                Response.AddHeader("Access-Control-Allow-Origin", "*");
                Response.AddHeader("Access-Control-Allow-Methods", "GET,HEAD,POST,PUT,DELETE");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, ZUMO-API-VERSION, Content-Type, Accept, Authorization");
            }

            return new JsonResult()
            {
                Data = res,
                ContentType = "application/json",
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        // GET: Tables/Details/5
        //public ActionResult Details(string id)
        //{
        //    Database db = new Database();
        //    Result res = new Result();
        //    res = db.Get(id);

        //    if (Response != null)
        //    {
        //        Response.AddHeader("Access-Control-Allow-Origin", "*");
        //    }

        //    return Json(res, JsonRequestBehavior.AllowGet);
        //    //return View();
        //}

        // GET: Tables/Details/5
        [System.Web.Http.HttpGet]
        public ActionResult Details(string name, string location)
        {
            Database db = new Database();
            Result res = new Result();
            res = db.Get(name, location);

            if (Response != null)
            {
                Response.AddHeader("Access-Control-Allow-Origin", "*");
                Response.AddHeader("Access-Control-Allow-Methods", "GET,HEAD,POST");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, ZUMO-API-VERSION, Content-Type, Accept, Authorization");
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateMillion()
        {

            for (int i = 1; i <= 1000000; i++)
            {
                Database db = new Helpers.Database();
                Result res = new Helpers.Result();
                string name = "Name_" + i;
                string location = "Location_" + i;
                res = db.Post(name, location);
            }

            Result res2 = new Helpers.Result();
            res2.code = 0;
            res2.success = true;
            res2.message = "Inserted 1 million records successfully";

            if (Response != null)
            {
                Response.AddHeader("Access-Control-Allow-Origin", "*");
                Response.AddHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT,DELETE");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, ZUMO-API-VERSION, Content-Type, Accept, Authorization");
            }

            return Json(res2, JsonRequestBehavior.AllowGet);
            
        }

        // POST: Tables/Create
        [System.Web.Http.HttpPost]
        public ActionResult Create([FromBody] Users2 value)
        {
            Database db = new Helpers.Database();
            Result res = new Helpers.Result();

            res = db.Post(value.Name, value.Location);

            if (Response != null)
            {
                Response.AddHeader("Access-Control-Allow-Origin", "*");
                Response.AddHeader("Access-Control-Allow-Methods", "GET,HEAD,POST");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, ZUMO-API-VERSION, Content-Type, Accept, Authorization");
            }

            return Json(res);
        }

        // GET: Tables/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tables/Edit/5
        [System.Web.Http.HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Tables/Delete/5
        [System.Web.Http.HttpDelete]
        public ActionResult Delete(string id)
        {
            Database db = new Database();
            Result res = new Result();
            res = db.Delete(id);

            if (Response != null)
            {
                Response.AddHeader("Access-Control-Allow-Origin", "*");
                Response.AddHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT,DELETE");
                Response.AddHeader("Access-Control-Allow-Headers", "Origin, ZUMO-API-VERSION, Content-Type, Accept, Authorization");
            }


            return new JsonResult()
            {
                Data = res,
                ContentType = "application/json",
                MaxJsonLength = Int32.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
