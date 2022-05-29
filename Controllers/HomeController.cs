using ImageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ImageProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [ActionName("UploadImage")]
        public IActionResult Image()
        {
            return View("UploadImage");
        }
        static void blur_process()// this function is giving me stress. It should blur the image uploaded
        {
            Bitmap picture = new Bitmap("wwwroot/file.jpg");
            var radius = 10;
            StackBlur.StackBlur.Process(picture, radius);
            picture.Save("wwwroot/blurred_pic.jpg");
        }
     
        [HttpPost]
        public IActionResult UploadImage()// this image
        {
            var file = Request.Form.Files.GetFile("file");
            FileStream stream = new FileStream("wwwroot/file.jpg",
                FileMode.Create);
            file.CopyTo(stream);
            stream.Close();
            blur_process();
            return View();
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
