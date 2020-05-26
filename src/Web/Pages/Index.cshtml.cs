using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
        private readonly ILogger<IndexModel> _logger;

        private readonly IEnumerable<CarModelDetails> _carModelService;

        public bool ShowPrice { get; private set; } = false;

        [BindProperty]
        public CarDetails CarInfo { get; set; }

        [BindProperty]
        public int CarModelDetailId { get; set; }


        public SelectList CarYearSL { get; } = new  SelectList(Enumerable.Range(1930, (DateTime.Today.Year-1929)).Reverse());
        public SelectList CarMakeSL { get; }

        public IndexModel(ILogger<IndexModel> logger, ICarModelService carFileModelService)
        {
            _logger = logger;
            _carModelService = carFileModelService.GetDetails();
            CarMakeSL = new SelectList(_carModelService, "Id", "Model", default, "Make");
        }

        public void OnGet()
        {
            _logger.LogInformation("Got page");
        }

        public void OnPost()
        {
            var selectedMakeModel = _carModelDetails.Where(x => CarModelDetailId == x.Id).FirstOrDefault();

            CarInfo.Make = selectedMakeModel.Make;
            CarInfo.Model = selectedMakeModel.Model;

            _logger.LogInformation($"{CarInfo.Make}  | {CarInfo.Model}");
            _logger.LogInformation($"Predicted price for car {CarInfo.Year} from {CarInfo.Mileage} miles");
            ShowPrice = true;
        }        
    }
}
