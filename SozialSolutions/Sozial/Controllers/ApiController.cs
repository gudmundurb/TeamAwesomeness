using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using Sozial.Models;

namespace Sozial.Controllers
{
    public class ApiController : Controller
    {

        public string currUserSteamID()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string id = (from ApplicationUser n in db.Users
                        where n.UserName == User.Identity.Name
                        select n.steamId).Single();
            return id;
        }


        //
        // GET: /Api/
        public async Task<ActionResult> gameList()
        {
            using (var client = new HttpClient())
            {
                var url = new UriBuilder("http://api.steampowered.com/ISteamApps/GetAppList/v0001/");
                var response = await client.GetAsync(url.ToString());
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            
        }

        public async Task<ActionResult> profileInfo(string steamID)
        {
            using (var client = new HttpClient()){
                string apikey = ConfigurationManager.AppSettings["steamApiKey"];
                string template = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + apikey + "&steamids=" + steamID;
                var url = new UriBuilder(template);
                var response = await client.GetAsync(url.ToString());
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            
        }

	}
}

