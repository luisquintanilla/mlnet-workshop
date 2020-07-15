using FluentAssertions;
using Microsoft.ML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTests.Schema;
using Tests.Common;

namespace ModelTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void Given_LowRangeCar_ShouldEstimatePriceWithinRange()
        {

        }

        [TestMethod]
        public void Given_MidRangeCar_ShouldEstimatePriceWithinRange()
        {

        }

        [TestMethod]
        public void Given_HighRangeCar_ShouldEstimatePriceWithinRange()
        {

        }

        private PredictionEngine<ModelInput, ModelOutput> GetPredictionEngine()
        {
            var modelPath = MLConfiguration.GetModelPath();

            var mlContext = new MLContext();

            var model = mlContext.Model.Load(modelPath, out var schema);

            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model, schema);
        }
    }
}
