using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using CustomerServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomerServer.Controllers
{
    public class PetsController : ApiController
    {
        // GET: api/Customer
        private FinalList FinalListReturned = new FinalList();
        public PetsController()
        {
          
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Customer
        public FinalList Post()
        {
            getServiceData();
            return FinalListReturned;
        }

        public void getServiceData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://agl-developer-test.azurewebsites.net/people.json");
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }

            var releases = JArray.Parse(content);
            var deserialized = JsonConvert.DeserializeObject<IEnumerable<Data>>(content);
            var ss= deserialized
                            .Where(x => x.pets != null && x.pets.Count > 0 && x.pets.Where(y => y.TYPE == "Cat").Count() > 0)
                            //with pets and only include users with cats
                            .OrderBy(x => x.gender)//order by gender
                                                   //creating an anonymous object with[ gender and pet list] object list or you can create a type and assign to that
                            .Select(x => new { Genderme = x.gender, pet = x.pets.Where(z => z.TYPE == "Cat").OrderBy(z => z.NAME).Select(c => c).ToList() }).ToList();

            FinalListReturned.MaleLst = new List<string>();
            FinalListReturned.FemaleLst = new List<string>();
            foreach (var s in ss)
            {
                foreach(Pets spet in s.pet)
                { 
                    if (s.Genderme == "Male")
                        FinalListReturned.MaleLst.Add(spet.NAME);
                    else
                        FinalListReturned.FemaleLst.Add(spet.NAME);
                }

            }
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
