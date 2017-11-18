using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleMongoAPI.Helpers
{
    public class Result
    {
        public int code { get; set; }
        public bool success { get; set; }
        public object data { get; set; } 
        public string message { get; set; }
    }
}