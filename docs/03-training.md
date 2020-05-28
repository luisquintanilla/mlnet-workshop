# Training

In this session, you'll configure a data processing pipeline to use to prepare the data, the algorithm to use, and you'll create a machine learning model to predict used car prices.

The training process consists of several steps:

- Preparing your data
- Choose an algorithm
- Train

Preparing your data can consist of:

- Filtering data
- Preparing data
- Feature engineering
- Convert data types
- Normalize the data

Examples of data transforms include

- Normalization
  - Min-Max
  - Binning
  - Mean variance
- Missing Values
  - Indicate
  - Replace


Learn more about [data transforms](https://docs.microsoft.com/dotnet/machine-learning/resources/transforms)
Learn about the different [algorithms](https://docs.microsoft.com/dotnet/machine-learning/how-to-choose-an-ml-net-algorithm)

## Data Transformation Pipeline

Most data can't be used as-is - we need to transform it before we can work with it. This is done by performing a series of transforms in succession as a pipeline.

Go back into Program.cs and add the following code after declaring `trainingData`:

```csharp
var dataProcessPipeline =
    mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "MakeEncoded", inputColumnName: "Make")
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ModelEncoded", inputColumnName: "Model"))
        .Append(mlContext.Transforms.Concatenate("Features", "Year", "Mileage", "MakeEncoded", "ModelEncoded"))
        .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))
        .AppendCacheCheckpoint(mlContext);
```

The above code first encodes the Year, Make, and Model columns using `OneHotEncoding`. It then concatenates the encoded Year, Make, and Model, as well as Mileage, into a Features column. Finally, it normalizes the Features values using a MinMax algorithm that results in a linear range from 0 to 1, with the min value at 0 and the max at 1.

Finally, since ML.NET doesn't perform any caching automatically, the resulting values are cached in preparation for running the training.

## Add Algorithms

Next, we need to choose an algorithm and add it to the pipeline. Add the following code after the code we just added to set up the transformation pipeline.

```csharp
// Choose an algorithm and add to the pipeline
var trainer = mlContext.Regression.Trainers.LbfgsPoissonRegression();

var trainingPipeline = dataProcessPipeline.Append(trainer);
```

This code sets up an instance of the trainer using a linear regression model, `LbfgsPoissonRegression`.

## Train the model

We train the model by calling the `Fit` method on the pipeline we've set up, passing in the data as an `IDataView` instance.

Add the following code to Program.cs after setting up the `trainingPipeline`:

```csharp
// Train the model
Console.WriteLine("Training model...");
var model = trainingPipeline.Fit(trainingData);
```

Once we've set up our model, the next step is to test it and see how it performs.

Next up - [04-evaluate](04-evaluate.md)
