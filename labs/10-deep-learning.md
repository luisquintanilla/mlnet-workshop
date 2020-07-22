# Phase 10: Train a deep learning model

In this session, you'll train a custom deep learning model to classify images of car damage.

## What is deep learning

!["ai-ml-dl"](https://user-images.githubusercontent.com/10437687/83196387-a9d31680-a0f0-11ea-9870-ffc1b2c8b215.png)

Deep Learning is a subset of machine learning. The most common algorithms used to train deep learning models are [neural networks](https://en.wikipedia.org/wiki/Deep_learning#Neural_networks).

## Deep learning in ML.NET

In ML.NET you can train and consume deep learning models.

### Consume models

In ML.NET you can use pretrained [TensorFlow](https://en.wikipedia.org/wiki/Deep_learning#Neural_networks) and [Open Neural Network Exchange (ONNX)](https://onnx.ai/) models to make predictions.

TensorFlow is an open-source platform for machine learning.

ONNX is an open format built to represent machine learning models. ONNX enables you to use your preferred framework with your chosen inference engine. Additionally, it makes it easier to access hardware optimizations.

### Train models

![ML.NET Tensorflow](https://user-images.githubusercontent.com/46974588/83211607-89747d80-a12b-11ea-8dce-555ec828773a.png)

ML.NET uses TensorFlow through the low-level bindings provided by the [TensorFlow.NET](https://github.com/SciSharp/TensorFlow.NET) library. The TensorFlow.NET library is an open source and low-level API library that provides the .NET Standard bindings for TensorFlow. That library is part of the open source [SciSharp](https://github.com/SciSharp) stack libraries.

To train image classification models, using the ML.NET API, use the [ImageClassification API](https://docs.microsoft.com/dotnet/api/microsoft.ml.visioncatalog.imageclassification?view=ml-dotnet#Microsoft_ML_VisionCatalog_ImageClassification_Microsoft_ML_MulticlassClassificationCatalog_MulticlassClassificationTrainers_System_String_System_String_System_String_System_String_Microsoft_ML_IDataView_).

You can also train custom deep learning models in Model Builder. The process is generally the same, but in addition to training locally, you can also leverage Azure to train models in GPU enabled compute instances.

## Phase 10.1: Install Nuget packages

First, we need to add a few NuGet packages to the `ImageTrainConsole` project.

If you're using Visual Studio, right click on the project name and select **Manage NuGet Dependencies**. Then click the "Browse" tab and search for `Microsoft.ML.Vision`. Make sure to install version **1.5.1**.

Alternately if you prefer working from the command line, you can run this command from the *src/ImageTrainConsole* folder:

```dotnetcli
dotnet add package Microsoft.ML.Vision -v 1.5.1
```

Repeat these steps for the `Microsoft.ML.ImageAnalytics` version `1.5.1` and `SciSharp.TensorFlow.Redist` version `2.2.0.2`.

## Phase 10.2: Load the data

[Download the data](https://storage.googleapis.com/bucket-8732/car_damage/preprocessed.zip) and unzip it.

> NOTE: This dataset is licensed from a third-party (Peltarion AB), not Microsoft.

The contents of the directory are the following:

- *image*: Directory containing all of the image files for training and testing.
- *index.csv*: File containing the following column names:
  - *image*: The relative path of the image (i.e. `image/1.jpg`).
  - *class*: The category of damage in the image.
  - *subset*: Whether the image belongs to the training(T) or validation (V) dataset.

Create a new class in the `Shared` project called `ImageModelInput`.

Replace the existing class definition with the following code:

```csharp
public class ImageModelInput
{
    public string ImagePath { get; set; }

    public string DamageClass { get; set; }

    public string Subset { get; set; }
}
```

Open the `Program.cs` file in the `ImageTrainConsole` project and add the following `using` statements.

```csharp
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Vision;
using Shared;
```

Inside the `Main` project, initialize the `MLContext`.

```csharp
MLContext mlContext = new MLContext();
```

Below the `Main` method, create a new method called `LoadImageData` to load the data from the `index.csv` file.

```csharp
static IEnumerable<ImageModelInput> LoadImageData(string path, string imageFilePath, char separator=',',bool hasHeader=true)
{
    // Choose how many rows to skip
    var skipRows = hasHeader ? 1 : 0;

    // Local function to get full image path
    Func<string, string> getFilePath = imagePath => Path.Combine(imageFilePath, imagePath.Split('/')[1]);

    // Load file and create IEnumerable<ImageModelInput>
    var imageData =
        File.ReadAllLines(path)
            .Skip(skipRows)
            .Select(line =>
            {
                var columns = line.Split(separator);
                return new ImageModelInput
                {
                    ImagePath = getFilePath(columns[0]),
                    DamageClass = columns[1],
                    Subset = columns[2]
                };
            });

    return imageData;
}
```

Below the `mlContext`, use the `LoadImageData` method to load the data and provide the path to the `index.csv` file and the `image` directory containing the image files.

```csharp
var imageData = LoadImageData(@"C:/Dev/index.csv", @"C:/Dev/preprocessed/preprocessed/image");
```

Once the data is loaded, split it into training and validation sets.

```csharp
var trainImages = imageData.Where(image => image.Subset == "T");
var validationImages = imageData.Where(image => image.Subset == "V");
```

Load the datasets into an `IDataView`.

```csharp
var trainImagesDV = mlContext.Data.LoadFromEnumerable(trainImages);
var validationImagesDV = mlContext.Data.LoadFromEnumerable(validationImages);
```

## Phase 10.3: Define pipeline

Once your data is loaded, it's time to define the set of transforms to train your model.

Start off by creating a data loading pipeline which takes the image path and creates a Bitmap. Below the `validationIMagesDV`, add the following code:

```csharp
var dataLoadPipeline = mlContext.Transforms.LoadRawImageBytes(outputColumnName:"ImageBytes", imageFolder:null, inputColumnName:"ImagePath");
```

To train an image classification model, there are several parameters that you may want to set. To make it easier to manage, create an instance of [`ImageClassificationTrainer.Options`](https://docs.microsoft.com/dotnet/api/microsoft.ml.vision.imageclassificationtrainer.options?view=ml-dotnet) below the `dataLoadPipeline`.

```csharp
var trainerOptions = new ImageClassificationTrainer.Options
{
    FeatureColumnName = "ImageBytes",
    LabelColumnName = "EncodedLabel",
    WorkspacePath = "workspace",
    Arch = ImageClassificationTrainer.Architecture.InceptionV3,
    ReuseTrainSetBottleneckCachedValues=true,
    MetricsCallback = (metrics) => Console.WriteLine(metrics.ToString())
};
```

Some parameters to note:

- *WorkspacePath*: The path where image bottleneck cache values and trained model are saved.
- *Arch*: The model architecture to use. In this case, the architecture used is [Inception v3](https://en.wikipedia.org/wiki/Inceptionv3). ML.NET supports several [model architectures](https://docs.microsoft.com/dotnet/api/microsoft.ml.vision.imageclassificationtrainer.architecture?view=ml-dotnet).
- *ReuseTrainSetBottleneckCachedValues*: When set to `true`, cached bottleneck values are used if already available in the workspace folder.
- *MetricsCallback*: Callback to report statistics on accuracy/cross entropy during training phase.

Once you have defined your settings, create the model training pipeline. Define the pipeline below the `trainerOptions`.

```csharp
var trainingPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "EncodedLabel", inputColumnName: "DamageClass")
    .Append(mlContext.MulticlassClassification.Trainers.ImageClassification(trainerOptions))
    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel", inputColumnName: "PredictedLabel"));

```

Finally, combine the data loading pipeline and training pipeline.

```csharp
var trainer = dataLoadPipeline.Append(trainingPipeline);
```

## Phase 10.4: Train the model

Once you have your pipeline defined, use `Fit` to train the model.

```csharp
var model = trainer.Fit(trainImagesDV);
```

The Image Classification API starts the training process by loading a pretrained TensorFlow model. The training process consists of two steps:

1. Bottleneck phase
2. Training phase

![ML.NET Image Classification API Training steps](https://user-images.githubusercontent.com/46974588/88136053-7b386000-cbb6-11ea-8fce-ada371ffe20f.png)

### Bottleneck phase

During the bottleneck phase, the set of training images is loaded and the pixel values are used as input, or features, for the frozen layers of the pretrained model. The frozen layers include all of the layers in the neural network up to the penultimate layer, informally known as the bottleneck layer. These layers are referred to as frozen because no training will occur on these layers and operations are pass-through. It's at these frozen layers where the lower-level patterns that help a model differentiate between the different classes are computed. The larger the number of layers, the more computationally intensive this step is. Fortunately, since this is a one-time calculation, the results can be cached and used in later runs when experimenting with different parameters.

### Training phase

Once the output values from the bottleneck phase are computed, they are used as input to retrain the final layer of the model. This process is iterative and runs for the number of times specified by model parameters. During each run, the loss and accuracy are evaluated. Then, the appropriate adjustments are made to improve the model with the goal of minimizing the loss and maximizing the accuracy. Once training is finished, two model formats are output. One of them is the `.pb` version of the model and the other is the `.zip` ML.NET serialized version of the model. When working in environments supported by ML.NET, it is recommended to use the `.zip` version of the model. However, in environments where ML.NET is not supported, you have the option of using the `.pb` version.

## Phase 10.4: Evaluate the model

Use the `Transform` method to make predictions on the validation dataset.

```csharp
var predictionsDV = model.Transform(validationImagesDV);
```

Evalute the model and output the macro accuracy to the console. Visit the ML.NET documentation to learn more about [multiclass classification metrics](https://docs.microsoft.com/dotnet/machine-learning/resources/metrics#evaluation-metrics-for-multi-class-classification).

```csharp
var evaluationMetrics = mlContext.MulticlassClassification.Evaluate(predictionsDV, labelColumnName: "EncodedLabel");
```

To make it easier to inspect the predicted values, convert the `predictionsDV` `IDataView` to an `IEnumerable`.

```csharp
var predictions = mlContext.Data.CreateEnumerable<ImageModelOutput>(predictionsDV, reuseRowObject: false);
```

Iterate over the predictions and compare the actual values to the predicted values.

## Phase 10.5: Save the model

Once you're satisfied with your model, save it to your PC.

```csharp
mlContext.Model.Save(model, trainImagesDV.Schema, @"C:/Dev/ImageModel.zip");
Console.WriteLine("Saved image classification model");
```

Congratulations! You have now trained a custom image classification model with ML.NET.

## Additional resources

- [Build an image classification model in under 10 minutes with Model Builder and Azure](https://www.youtube.com/watch?v=G_ZJZdKLNMc&)
- [How to train an image classification model with Model Builder](https://devblogs.microsoft.com/dotnet/train-image-classification-model-azure-mlnet-model-builder/)
- [Train a custom deep learning model to identify damaged infrastructure](https://docs.microsoft.com/dotnet/machine-learning/tutorials/image-classification-api-transfer-learning)
- [Detect objects using ONNX in ML.NET](https://docs.microsoft.com/dotnet/machine-learning/tutorials/object-detection-onnx)
- [Classify sentiment using TensorFlow](https://docs.microsoft.com/dotnet/machine-learning/tutorials/text-classification-tf)