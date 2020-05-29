# Deep learning

This section provides information and a set of resources on Deep Learning in ML.NET.

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

To train image classification models, using the ML.NET API, use the [ImageClassificationTrainer](https://docs.microsoft.com/dotnet/api/microsoft.ml.vision.imageclassificationtrainer?view=ml-dotnet).

You can also train custom deep learning models in Model Builder. The process is generally the same, but in addition to training locally, you can also leverage Azure to train models in GPU enabled compute instances.

## Additional Resources

- [Build an image classification model in under 10 minutes with Model Builder and Azure](https://www.youtube.com/watch?v=G_ZJZdKLNMc&)
- [How to train an image classification model with Model Builder](https://devblogs.microsoft.com/dotnet/train-image-classification-model-azure-mlnet-model-builder/)
- [Train a custom deep learning model to identify damaged infrastructure](https://docs.microsoft.com/dotnet/machine-learning/tutorials/image-classification-api-transfer-learning)
- [Detect objects using ONNX in ML.NET](https://docs.microsoft.com/dotnet/machine-learning/tutorials/object-detection-onnx)