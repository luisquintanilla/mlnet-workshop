# Loading Data

In this session, you'll download the data needed to train the model and import into an `IDataView`. See [Load data from files and other sources](https://docs.microsoft.com/dotnet/machine-learning/how-to-guides/load-data-ml-net) for more information.

## Get the data

First you need to download the data:

[850,000 Used Car Listings in CSV Format](https://www.kaggle.com/jpayne/852k-used-car-listings#true_car_listings.csv)

Unzip the data. You can open it and inspect its contents. It should look something like this:

![Used car sale data in CSV format](https://user-images.githubusercontent.com/782127/82711417-575aab80-9c53-11ea-9270-459fb5e79441.png)

Note the column names - we will use them in the next step.

## Create the data model input schema

In the TrainingConsoleApp project, add a new class to the root. This class will consist solely of properties and will be mapped from the data file. Each property will reference a column using a 0-based index.

```csharp
using Microsoft.ML.Data;

namespace TrainingConsoleApp
{
    public class ModelInput
    {
        [ColumnName("Label"), LoadColumn(0)]
        public float Price { get; set; }

        [LoadColumn(1)]
        public string Year  { get; set; }

        [LoadColumn(2)]
        public float Mileage { get; set; }

        [LoadColumn(4)]
        public string Make { get; set; }

        [LoadColumn(5)]
        public string Model { get; set; }
    }
}
```

> Note that each `[LoadColumn]` attribute specifies the index of its respective column.

## Load the data from the file into an IDataView

Now open up `Program.cs` and add the following code to load the data from the file.

First, add the location of the file wherever you saved it as a static string:

```csharp
class Program
{
    // update this with your file's path where you unzipped it
    private static string TRAIN_DATA_FILEPATH = @"C:\dev\true_car_listings.csv";

// ...
```

Next, in the `Main` method, add this block of code after your creation of `mlContext`:

```csharp
    // Load training data
    Console.WriteLine("Loading data...");
    IDataView trainingData = mlContext.Data.LoadFromTextFile<ModelInput>(path:TRAIN_DATA_FILEPATH, hasHeader: true, separatorChar:',');
```

At this point, you have an `IDataView` instance holding all of the data from the text file, transformed into the `ModelInput` schema.

Next up - [03-training](03-training.md)
