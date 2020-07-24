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

![Install Microsoft.ML.Vision NuGet package](https://user-images.githubusercontent.com/46974588/88373334-a9a86d80-cd65-11ea-80ac-8d297fbe924f.png)

Repeat these steps for the `Microsoft.ML.ImageAnalytics` version `1.5.1` and `SciSharp.TensorFlow.Redist` version `2.2.0.2`.

Alternately if you prefer working from the command line, you can run this command from the *src/ImageTrainConsole* folder:

```dotnetcli
dotnet add package Microsoft.ML.Vision -v 1.5.1
dotnet add package Microsoft.ML.ImageAnalytics -v 1.5.1
dotnet add package SciSharp.TensorFlow.Redist -v 2.2.0.2
```

## Phase 10.2: Load the data

In this session, you'll download the data needed to train the model and load it into an `IDataView`.

### Download the data

[Download the data](https://storage.googleapis.com/bucket-8732/car_damage/preprocessed.zip) and unzip it.

> NOTE: This dataset is licensed from a third-party (Peltarion AB), not Microsoft.

The contents of the directory are the following:

- *image*: Directory containing all of the image files for training and testing.
- *index.csv*: File containing the following column names:
  - *image*: The relative path of the image (i.e. `image/1.jpg`).
  - *class*: The category of damage in the image.
  - *subset*: Whether the image belongs to the training(T) or validation (V) dataset.

You can open and inspect the contents of *index.csv* in a tool like Excel

![Car Damage Image Data Annotations](https://user-images.githubusercontent.com/46974588/88373950-c5604380-cd66-11ea-9835-62aa395e8db4.png)

### Define input schema

Create a new class in the `Shared` project called `ImageModelInput`. This class will consist solely of properties and will be mapped from the data file. Each property will reference a column using a 0-based index.

Start by adding the following `using` statement:

Then, define the class as follows:

```csharp
public class ImageModelInput
{
    [LoadColumn(0)]
    public string ImagePath { get; set; }

    [LoadColumn(1)]
    public string DamageClass { get; set; }

    [LoadColumn(2)]
    public string Subset { get; set; }
}
```

![Define image model input schema](https://user-images.githubusercontent.com/46974588/88374342-9c8c7e00-cd67-11ea-87eb-4e9758ecafeb.png)

### Load data into an IDataView

Open the `Program.cs` file in the `ImageTrainConsole` project and add the following `using` statements:

```csharp
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Vision;
using Shared;
```

Inside of the class definition, add the location of the *index.csv* file and the *image* directory, which will depend on where you saved them to:

```csharp
// update this with your file's path where you saved it
private static string TRAIN_DATA_FILEPATH = @"C:\Dev\mlnet-workshop\data\preprocessed\index.csv";
private static string IMAGE_DATA_DIRECTORY = @"C:\Dev\mlnet-workshop\data\preprocessed\image";
```

Inside the `Main` method, initialize the `MLContext`. `MLContext` is the starting point for all ML.NET operations.

```csharp
MLContext mlContext = new MLContext();
```

Below the `Main` method, create a new method called `LoadImageData` to load the data from the `index.csv` file. This method will load your data and do some parsing. Note that although you can use ML.NET's `LoadFromTextFile` method to load the contents of the file directly into an `IDataView`, instead we use `LoadFromEnumerable` because there are a few manipulations we want to make to the image path before loading it into the `IDataView`.

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

Below `mlContext`, use the `LoadImageData` method to load the data by providing the path to the `index.csv` file and the `image` directory containing the image files.

```csharp
// Load data in index.csv file
var imageData = LoadImageData(TRAIN_DATA_FILEPATH, IMAGE_DATA_DIRECTORY);
```

Once the data is loaded, split it into training and validation sets. Doing so will help us evaluate how well the trained model performs on unseen data.

```csharp
// Split data into training and validation sets
var trainImages = imageData.Where(image => image.Subset == "T");
var validationImages = imageData.Where(image => image.Subset == "V");
```

Load the datasets into an `IDataView`.

```csharp
// Load data into IDataViews
var trainImagesDV = mlContext.Data.LoadFromEnumerable(trainImages);
var validationImagesDV = mlContext.Data.LoadFromEnumerable(validationImages);
```

![Load image data into IDataVIew](https://user-images.githubusercontent.com/46974588/88375426-8e3f6180-cd69-11ea-9d79-d4ea228f3b56.png)

## Phase 10.3: Define pipeline

In this session, you will define a training pipeline for a deep learning image classification model using the ML.NET [ImageClassification API](https://docs.microsoft.com/dotnet/api/microsoft.ml.visioncatalog.imageclassification?view=ml-dotnet#Microsoft_ML_VisionCatalog_ImageClassification_Microsoft_ML_MulticlassClassificationCatalog_MulticlassClassificationTrainers_System_String_System_String_System_String_System_String_Microsoft_ML_IDataView_)

Once your data is loaded, it's time to define the set of transforms to train your model.

Start off by creating a data loading pipeline which takes the image path and creates a `Bitmap`. Below the `validationImagesDV` variable, add the following code:

```csharp
var dataLoadPipeline = mlContext.Transforms.LoadRawImageBytes(outputColumnName:"ImageBytes", imageFolder:null, inputColumnName:"ImagePath");
```

To train an image classification model, there are several parameters that you may want to set. To make it easier to manage, create an instance of [`ImageClassificationTrainer.Options`](https://docs.microsoft.com/dotnet/api/microsoft.ml.vision.imageclassificationtrainer.options?view=ml-dotnet).

```csharp
// Set the options for the image classification trainer
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
- *Arch*: The model architecture to use. In this case, the architecture used is [Inception v3](https://en.wikipedia.org/wiki/Inceptionv3). ML.NET supports several [image classification model architectures](https://docs.microsoft.com/dotnet/api/microsoft.ml.vision.imageclassificationtrainer.architecture?view=ml-dotnet).
- *ReuseTrainSetBottleneckCachedValues*: When set to `true`, cached bottleneck values are used if already available in the workspace folder.
- *MetricsCallback*: Callback to report statistics on accuracy/cross entropy during training phase.

Once you have defined your options, use them to create your training pipeline. Note that the DamageClass is encoded as a [`KeyDataViewType`](https://docs.microsoft.com/dotnet/microsoft.ml.data.keydataviewtype?view=ml-dotnet) before being passed into the model by using the [`MapValueToKey`](https://docs.microsoft.com/dotnet/api/microsoft.ml.conversionsextensionscatalog.mapvaluetokey?view=ml-dotnet) transform. Once the model classifies the image, the predicted category is decoded back to the original `DamageClass` value using the [`MapKeyToValue`](https://docs.microsoft.com/dotnet/api/microsoft.ml.conversionsextensionscatalog.mapkeytovalue?view=ml-dotnet#Microsoft_ML_ConversionsExtensionsCatalog_MapKeyToValue_Microsoft_ML_TransformsCatalog_ConversionTransforms_System_String_System_String_) transform.

```csharp
// Define training pipeline
var trainingPipeline = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "EncodedLabel", inputColumnName: "DamageClass")
    .Append(mlContext.MulticlassClassification.Trainers.ImageClassification(trainerOptions))
    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel", inputColumnName: "PredictedLabel"));
```

Finally, combine the data loading pipeline and training pipeline.

```csharp
var trainer = dataLoadPipeline.Append(trainingPipeline);
```

![Define image classification training pipeline](https://user-images.githubusercontent.com/46974588/88376609-bfb92c80-cd6b-11ea-8123-0f309e384413.png)

## Phase 10.4: Train the model

In this section you will train your image classification model.

Once you have your pipeline defined, use `Fit` to train the model.

```csharp
// Train the model
var model = trainer.Fit(trainImagesDV);
```

![Train the image classification model](https://user-images.githubusercontent.com/46974588/88376552-a4e6b800-cd6b-11ea-97b8-bf13cf9e1b7d.png)

The Image Classification API starts the training process by loading a pretrained TensorFlow model. The training process consists of two steps:

1. Bottleneck phase
2. Training phase

![ML.NET Image Classification API Training steps](https://user-images.githubusercontent.com/46974588/88136053-7b386000-cbb6-11ea-8fce-ada371ffe20f.png)

### Bottleneck phase

During the bottleneck phase, the set of training images is loaded and the pixel values are used as input, or features, for the frozen layers of the pretrained model. The frozen layers include all of the layers in the neural network up to the penultimate layer, informally known as the bottleneck layer. These layers are referred to as frozen because no training will occur on these layers and operations are pass-through. It's at these frozen layers where the lower-level patterns that help a model differentiate between the different classes are computed. The larger the number of layers, the more computationally intensive this step is. Fortunately, since this is a one-time calculation, the results can be cached and used in later runs when experimenting with different parameters.

### Training phase

Once the output values from the bottleneck phase are computed, they are used as input to retrain the final layer of the model. This process is iterative and runs for the number of times specified by model parameters. During each run, the loss and accuracy are evaluated. Then, the appropriate adjustments are made to improve the model with the goal of minimizing the loss and maximizing the accuracy. Once training is finished, two model formats are output. One of them is the `.pb` version of the model and the other is the `.zip` ML.NET serialized version of the model. When working in environments supported by ML.NET, it is recommended to use the `.zip` version of the model. However, in environments where ML.NET is not supported, you have the option of using the `.pb` version.

The output of this phase should look something like the following:

```text
2020-07-24 05:26:24.836060: I tensorflow/core/platform/cpu_feature_guard.cc:143] Your CPU supports instructions that this TensorFlow binary was not compiled to use: AVX2
2020-07-24 05:26:24.861675: I tensorflow/compiler/xla/service/service.cc:168] XLA service 0x1e7a01d1e90 initialized for platform Host (this does not guarantee that XLA will be used). Devices:
2020-07-24 05:26:24.873356: I tensorflow/compiler/xla/service/service.cc:176]   StreamExecutor device (0): Host, Default Version
Saver not created because there are no variables in the graph to restore
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   1
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   2
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   3
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   4
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   5
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   6
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   7
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   8
Phase: Bottleneck Computation, Dataset used:      Train, Image Index:   9
...
Phase: Training, Dataset used:      Train, Batch Processed Count: 115, Epoch:  31, Accuracy:  0.9869566, Cross-Entropy: 0.122196294, Learning Rate: 0.0037157428
Phase: Training, Dataset used: Validation, Batch Processed Count:  13, Epoch:  31, Accuracy: 0.76263744, Cross-Entropy: 0.63454604
Phase: Training, Dataset used:      Train, Batch Processed Count: 115, Epoch:  32, Accuracy:  0.9869566, Cross-Entropy: 0.12009087, Learning Rate: 0.0037157428
Phase: Training, Dataset used: Validation, Batch Processed Count:  13, Epoch:  32, Accuracy: 0.75494516, Cross-Entropy: 0.63655174
Saver not created because there are no variables in the graph to restore
Restoring parameters from workspace\custom_retrained_model_based_on_inception_v3.meta
Froze 2 variables.
Converted 2 variables to const ops.
```

## Phase 10.4: Evaluate the model

In this section, you will evaluate how well your model performs on the validation dataset.

The evaluation metric used to measure model performance depends on the task. The task in this application is multiclass classification. Some common regression metrics are:

- Micro-accuracy (Closer to 1 is better)
- Macro-accuracy (Close to 1 is better)
- Log-loss (Closer to 0 is better)
- Log-loss reduction (Greater than 0 is better)

Learn more about [multiclass classification evaluation metrics](https://docs.microsoft.com/dotnet/machine-learning/resources/metrics#evaluation-metrics-for-multi-class-classification).

To evaluate your model, start off by using the model to make predictions on the validation images. In the `Main` method of the *Program.cs* file inside the `ImageTrainConsole` project, use the `Transform` method.

```csharp
// Use the model to make predictions on the validation images
var predictionsDV = model.Transform(validationImagesDV);
```

Evaluate the model and output the macro-accuracy to the console.

```csharp
// Evaluate the model
var evaluationMetrics = mlContext.MulticlassClassification.Evaluate(predictionsDV, labelColumnName: "EncodedLabel");
Console.WriteLine($"Train Set Macro Accuracy: {evaluationMetrics.MacroAccuracy}");
```

The output should look something like the following:

```text
Train Set Macro Accuracy: 0.6810376171767301
```

### Compare actual against predicted values

Start off by defining the model output schema. In the `Shared` project, create a new class called `ImageModelOutput` and define it as follows:

```csharp
public class ImageModelOutput
{
    public string ImagePath { get; set; }
    public string DamageClass { get; set; }
    public string PredictedLabel { get; set; }
}
```

![Define image classification model output schema](https://user-images.githubusercontent.com/46974588/88377444-2ee35080-cd6d-11ea-893e-947665806787.png)

To make it easier to inspect the predicted values, convert the `predictionsDV` `IDataView` to an `IEnumerable`. Because we plan on lazily iterating over the collection, we set the `reuseRowObject` parameter to `true`.

```csharp
// Convert IDataView to IEnumerable
var predictions = mlContext.Data.CreateEnumerable<ImageModelOutput>(predictionsDV, reuseRowObject: true);
```

Iterate over the predictions and compare the actual values to the predicted values by displaying them in the console.

```csharp
//Iterate over predictions and display actual vs predicted values
foreach (var prediction in predictions)
{
    var fileName = Path.GetFileName(prediction.ImagePath);
    Console.WriteLine($"Image: {fileName} | Actual: {prediction.DamageClass} | Predicted: {prediction.PredictedLabel}");
}
```

![Evaluate the image classification model](https://user-images.githubusercontent.com/46974588/88378883-e11c1780-cd6f-11ea-827d-96b0baea3df1.png)

The output should look something like the following:

```text
Image: 1366.jpeg | Actual: bumper_scratch | Predicted: bumper_scratch
Image: 1392.jpeg | Actual: bumper_scratch | Predicted: bumper_scratch
Image: 1399.jpeg | Actual: bumper_scratch | Predicted: door_dent
Image: 1431.jpeg | Actual: bumper_scratch | Predicted: bumper_scratch
Image: 1437.jpeg | Actual: bumper_scratch | Predicted: tail_lamp
Image: 1497.jpeg | Actual: bumper_scratch | Predicted: bumper_scratch
Image: 1500.jpeg | Actual: bumper_scratch | Predicted: bumper_scratch
```

## Phase 10.5: Save the model

In this section, you will save your image classification model for later use in other applications (desktop, mobile, web).

Once you're satisfied with your model, save it to your PC.

Inside of the *Program* class definition of the `ImageTrainConsole` project, add the path where you want to save your model to. The model is serialized and stored as a `.zip` file. In this case, the model will be saved to a file called *ImageModel.zip*.

```csharp
private static string MODEL_SAVE_PATH = @"C:\Dev\ImageModel.zip";
```

Then, inside the `Main` method, add the following code at the bottom:

```csharp
//Save the model
mlContext.Model.Save(model, trainImagesDV.Schema, MODEL_SAVE_PATH);
Console.WriteLine("Saved image classification model");
```

![Save image classification model](https://user-images.githubusercontent.com/46974588/88378946-fc872280-cd6f-11ea-8b1a-e5f3204c06aa.png)

Congratulations! You have now trained a custom image classification model with ML.NET.

## Additional resources

- [Build an image classification model in under 10 minutes with Model Builder and Azure](https://www.youtube.com/watch?v=G_ZJZdKLNMc&)
- [How to train an image classification model with Model Builder](https://devblogs.microsoft.com/dotnet/train-image-classification-model-azure-mlnet-model-builder/)
- [Train a custom deep learning model to identify damaged infrastructure](https://docs.microsoft.com/dotnet/machine-learning/tutorials/image-classification-api-transfer-learning)
- [Detect objects using ONNX in ML.NET](https://docs.microsoft.com/dotnet/machine-learning/tutorials/object-detection-onnx)
- [Classify sentiment using TensorFlow](https://docs.microsoft.com/dotnet/machine-learning/tutorials/text-classification-tf)