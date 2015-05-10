using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sozial.Controllers
{
    public class ApiController : Controller
    {
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
	}
}