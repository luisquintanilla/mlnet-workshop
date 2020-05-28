# Training

In this session, you'll configure a data processing pipeline to use to prepare the data, the algorithm to use, and you'll create a machine learning model to predict used car prices.

The training process consists of several steps:

1. Preparing your data
2. Choose an algorithm
3. Train the model

Learn more about [data transforms](https://docs.microsoft.com/dotnet/machine-learning/resources/transforms)

## Split the data into train and test sets

The goal of a machine learning model is to identify patterns within training data. These patterns are used to make predictions using new data.

Use the `TrainTestSplit` method to split the data into train and test sets. The result will be a `TrainTestData` object which contains two `IDataView` members, one for the train set and the other for the test set. The data split percentage is determined by the `testFraction` parameter. The snippet below is holding out 20 percent of the original data for the test set.

```csharp
var trainTestSplit = mlContext.Data.TrainTestSplit(trainingData, testFraction: 0.2);
```

## Data Transformation Pipeline

Most data can't be used as-is - we need to transform it before we can work with it. This is done by performing a series of transforms in succession as a pipeline.

Go back into *Program.cs* and add the following code after declaring `trainingData`:

```csharp
var dataProcessPipeline =
    mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "MakeEncoded", inputColumnName: "Make")
        .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "ModelEncoded", inputColumnName: "Model"))
        .Append(mlContext.Transforms.Concatenate("Features", "Year", "Mileage", "MakeEncoded", "ModelEncoded"))
        .Append(mlContext.Transforms.NormalizeMinMax("Features", "Features"))
        .AppendCacheCheckpoint(mlContext);
```

The above code first encodes the Make and Model columns using `OneHotEncoding`. It then concatenates the encoded Year, Make, and Model, as well as Mileage, into a Features column. Finally, it normalizes the Features values using a `MinMax` transform that results in a linear range from 0 to 1, with the min value at 0 and the max at 1.

Finally, since ML<i>.NET doesn't perform any caching automatically, the resulting values are cached in preparation for running the training. Caching can help improve training time since data doesn't have to continuously be loaded from disk. Keep in mind though, only cache when the dataset can fit into memory.

## Add Algorithms

Next, we need to choose an algorithm and add it to the pipeline. Add the following code after the code we just added to set up the transformation pipeline.

```csharp
// Choose an algorithm and add to the pipeline
var trainer = mlContext.Regression.Trainers.LbfgsPoissonRegression();
var trainingPipeline = dataProcessPipeline.Append(trainer);
```

This code sets up an instance of the trainer using a linear regression model, `LbfgsPoissonRegression`. Learn about the different [algorithms](https://docs.microsoft.com/dotnet/machine-learning/how-to-choose-an-ml-net-algorithm) in ML</i>.NET

## Train the model

We train the model by calling the `Fit` method on the pipeline we've set up, passing in the data as an `IDataView` instance.

Add the following code to *Program.cs* after setting up the `trainingPipeline`:

```csharp
// Train the model
Console.WriteLine("Training model...");
var model = trainingPipeline.Fit(trainTestSplit.TrainSet);
```

Once we've set up our model, the next step is to test it and see how it performs.

Next up - [04-evaluate](04-evaluate.md)
