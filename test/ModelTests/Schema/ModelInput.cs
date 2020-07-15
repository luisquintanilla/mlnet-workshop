using Microsoft.ML.Data;

namespace ModelTests.Schema
{
    public class ModelInput
    {
        [ColumnName("Label"), LoadColumn(0)]
        public float Price { get; set; }

        [LoadColumn(1)]
        public float Year { get; set; }

        [LoadColumn(2)]
        public float Mileage { get; set; }

        [LoadColumn(6)]
        public string Make { get; set; }

        [LoadColumn(7)]
        public string Model { get; set; }
    }
}