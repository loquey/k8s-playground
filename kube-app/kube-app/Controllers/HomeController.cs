using kube_app.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

using System.Diagnostics;

namespace kube_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;
        private const string KeyListId = "key_id";

        public HomeController(ILogger<HomeController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCache()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCache(CacheEntryModel model)
        {
            var keys = _cache.GetString(KeyListId);
            keys += ":" + model.Key;
            _cache.SetString(KeyListId, keys);

            _cache.SetString(model.Key, model.Value);
            ViewBag.Success = "Key addess successfully";
            return View();
        }

        public IActionResult Dump()
        {
            var cacheData = new List<CacheEntryModel>();
            var keys = _cache.GetString(KeyListId);

            if (keys is not null)
            {
                var keyList = keys.Split(':');

                foreach (var key in keyList)
                {
                    cacheData.Add(new CacheEntryModel
                    {
                        Key = key,
                        Value = _cache.GetString(key),
                    });
                }

            }
            return View(cacheData);
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