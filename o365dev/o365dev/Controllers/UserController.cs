using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using o365dev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace o365dev.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            //get the token from the cache.
            string signedInUserID = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            ADALTokenCache UserTokenCache = new ADALTokenCache(signedInUserID);

            List<TokenCacheItem> items = UserTokenCache.ReadItems().Where(tc => tc.Resource == "https://graph.microsoft.com").ToList();

            string myAccessToken = items[0].AccessToken;

            //make a HTTPClient request for the information and include the token.
            // thanks to post Vardhaman Deshpande.
            string resourceUrl = "https://graph.microsoft.com/beta/me";

            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, resourceUrl))
                {
                    request.Headers.Add("Authorization", "Bearer " + myAccessToken);
                    request.Headers.Add("Accept", "application/json;odata.metadata=minimal");
                    using (var response = client.SendAsync(request).Result)
                    {
                        var json = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                        ViewBag.Name = json.ToString();
                    }
                }
            }

            return View();
        }
    }
}