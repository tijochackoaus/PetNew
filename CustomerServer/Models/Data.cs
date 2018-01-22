using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerServer.Models
{
    public class Data
    {
        public List<Pets> pets { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
    }

    public class FinalList
    {
        public List <string> MaleLst { get; set; }
        public List<string> FemaleLst { get; set; }
    }
    
    public class Pets
    {
        public string NAME { get; set; }
        public string TYPE { get; set; }
    }
}