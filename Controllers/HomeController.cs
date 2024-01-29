using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ApiPrueba.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";

            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();


            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            string apiKey = Convert.ToBase64String(key);

            var client = new RestClient("https://sandbox.array.io/api/user/v2");
            var request = new RestRequest("/resource/", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"address\":{\"street\":\"535 30 RD A\"," +
                "\"city\":\"HOUSTON\"," +
                "\"state\":\"TX\"," +
                "\"zip\":\"77001\"}," +
                "\"ssn\":\"txi_1KdN0\"," +
                "\"appKey\":\""+ myuuidAsString + "\"," +
                "\"dob\":\"1975-01-01\"," +
                "\"lastName\":\"Perez\"," +
                "\"firstName\":\"Humberto\"}", ParameterType.RequestBody);

           
            try
            {
                var response = await client.ExecuteAsync(request);
            }
            catch (Exception error)
            {
                // Log
            }



            return View();
        }
    }
}
