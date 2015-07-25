using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using o365dev.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace o365dev.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
                        //get the token from the cache.
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            ADALTokenCache UserTokenCache = new ADALTokenCache(signedInUserID);

            List<TokenCacheItem> items = UserTokenCache.ReadItems().Where(tc => tc.Resource == "https://graph.microsoft.com").ToList();

            string myAccessToken = items[0].AccessToken;

            //make a HTTPClient request for the information and include the token.
            // thanks to post Vardhaman Deshpande.
            string resourceUrl = string.Format("https://graph.microsoft.com/beta/{0}/users", ConfigurationManager.AppSettings["ida:Domain"]);
            List<Employee> json = new List<Employee>();

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, resourceUrl))
                {
                    request.Headers.Add("Authorization", "Bearer " + myAccessToken);
                    request.Headers.Add("Accept", "application/json;odata.metadata=minimal");
                    using (var response = client.SendAsync(request).Result)
                    {
                        var jsonResult = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                        json = JsonConvert.DeserializeObject<List<Employee>>(jsonResult["value"].ToString());
                    }
                }
            }

            return View(json.AsEnumerable<Employee>());
        }
    }
}