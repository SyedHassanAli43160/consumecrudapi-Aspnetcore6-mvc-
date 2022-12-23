using consumeapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using consumeapi.Helper;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace consumeapi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        userapi _Api = new userapi();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<user> users = new List<user>();
            HttpClient client = _Api.initial();
            HttpResponseMessage res = await client.GetAsync("user/Get");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<user>>(result);
            }
            return View(users);
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = new user();
            HttpClient client = _Api.initial();
            HttpResponseMessage res = await client.GetAsync($"user/Getuserbyid?id={id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<user>(result);
            }
            return View(user);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(user user)
        {

            HttpClient client = _Api.initial();

            var posttask = client.PostAsJsonAsync<user>("user/Postuser", user);
            posttask.Wait();
            var result = posttask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> update(user user)
        {
            var obj = new user();

            HttpClient client = _Api.initial();
            HttpResponseMessage res = await client.GetAsync($"user/Getuserbyid?id={user.Id}");
            var response = await client.PutAsJsonAsync($"user/putasync/{user.Id}",user);
            var result = res.Content.ReadAsStringAsync().Result;
            obj = JsonConvert.DeserializeObject<user>(result);
            if (response.IsSuccessStatusCode)
            {
              
                return RedirectToAction("Index");
            }
        return View(obj);

        }
    
        public async Task<IActionResult> Deleteuser(int id)
        {
            var user = new user();
            HttpClient client = _Api.initial();
            HttpResponseMessage res = await client.DeleteAsync($"user/Delete?id={id}");
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
     
    }  
}
