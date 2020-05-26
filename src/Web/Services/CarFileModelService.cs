using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Web.Models;

namespace Web.Services
{
    public class CarFileModelService : ICarModelService
    {

        private readonly IEnumerable<CarModelDetails> _details;
        
        public CarFileModelService(string filePath)
        {
            var data = File.ReadAllText(filePath);
            _details = JsonSerializer.Deserialize<IEnumerable<CarModelDetails>>(data);
        }

        public IEnumerable<CarModelDetails> GetDetails()
        {
            return _details;
        }
    }
}
