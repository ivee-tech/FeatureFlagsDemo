using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestFrontEnd.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using TestFrontEnd.Interfaces;
using Microsoft.CommonLib;

namespace TestFrontEnd.Controllers
{
    public class HomeController : Controller
    {

        private readonly IConfigReader _config;
        private readonly INamesClient _names;
        // private readonly IFeatureFlags _features;
        // private readonly IGiphyClient _giphy;
        public HomeController(IConfigReader config, INamesClient names) // , IFeatureFlags features, IGiphyClient giphy)
        {
            _config = config;
            _names = names;
            // _features = features;
            // _giphy = giphy;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Name()
        {
            var nameModel = await _names.GetNameAsync();
            return View(nameModel);
        }

        /*
        public async Task<IActionResult> Giphy(string q)
        {
            // var enabled = _features.IsFeatureEnabled("Features:Giphy:Enabled");
            var enabled = _features.IsFeatureEnabledExpr("Features:Giphy:EnabledExpr");
            if(enabled)
            {
                string search = "flags";
                if(!string.IsNullOrEmpty(q))
                    search = q;
                var urls = await _giphy.GetGifsAsync(search);
                // ViewBag.Info = _config["Features:Giphy:UriFormat"];
                ViewBag.Urls = urls;
                return View();
            }
            return NotFound();
        }
        */

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
