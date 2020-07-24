using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Services;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<IndexModel> _logger;

        private readonly IEnumerable<CarModelDetails> _carModelService;
        private readonly int _imageHeight = 480;
        private readonly int _imageWidth = 640;

        public bool ShowPrice { get; private set; } = false;
        public bool ShowImage { get; private set; } = false;

        [BindProperty]
        public CarDetails CarInfo { get; set; }

        [BindProperty]
        public int CarModelDetailId { get; set; }

        [BindProperty]
        public IFormFile ImageUpload { get; set; }

        public SelectList CarYearSL { get; } = new SelectList(Enumerable.Range(1930, (DateTime.Today.Year - 1929)).Reverse());
        public SelectList CarMakeSL { get; }

        public IndexModel(IWebHostEnvironment env, ILogger<IndexModel> logger, ICarModelService carFileModelService)
        {
            _env = env;
            _logger = logger;
            _carModelService = carFileModelService.GetDetails();
            CarMakeSL = new SelectList(_carModelService, "Id", "Model", default, "Make");
        }

        public void OnGet()
        {
            _logger.LogInformation("Got page");
        }

        public async Task OnPostAsync()
        {
            var selectedMakeModel = _carModelService.Where(x => CarModelDetailId == x.Id).FirstOrDefault();

            if (ImageUpload != null)
            {
                await ProcessUploadedImageAsync(ImageUpload);
                ShowImage = true;
            }

            CarInfo.Make = selectedMakeModel.Make;
            CarInfo.Model = selectedMakeModel.Model;

            _logger.LogInformation($"{CarInfo.Make}  | {CarInfo.Model}");
            _logger.LogInformation($"Predicted price for car {CarInfo.Year} from {CarInfo.Mileage} miles");
            ShowPrice = true;
        }

        private Image ResizeImage(MemoryStream imageStream)
        {
            // Create image from stream 
            using (var img = Image.FromStream(imageStream))
            {
                // Get original image dimensions
                int originalWidth = img.Width;
                var originalHeight = img.Height;

                // Preserve aspect ratio
                float ratioX = (float)_imageWidth / (float)originalWidth;
                float ratioY = (float)_imageHeight / (float)originalHeight;
                float ratio = Math.Min(ratioX, ratioY);

                float originalRatio = (float)originalWidth / originalHeight;

                // New width and height based on aspect ratio
                int resizedWidth = (int)(originalWidth * ratio);
                int resizedHeight = (int)(originalHeight * ratio);

                //Resize image
                Image resizedImage = new Bitmap(resizedHeight, resizedWidth);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawImage(img, 0, 0, resizedHeight, resizedWidth);
                }

                // Write resized image to memory stream
                resizedImage.Save(imageStream, ImageFormat.Jpeg);
                return resizedImage;
            }
        }

        private async Task ProcessUploadedImageAsync(IFormFile uploadedImage)
        {
            using (var ms = new MemoryStream())
            {
                //Copy image to memory stream
                await uploadedImage.CopyToAsync(ms);

                // Resize Image
                var img = ResizeImage(ms);

                // Convert image to base64 string
                var base64Image = Convert.ToBase64String(ms.ToArray());
                CarInfo.Base64Image = $"data:image/png;base64,{base64Image}";
            }
        }
    }
}
