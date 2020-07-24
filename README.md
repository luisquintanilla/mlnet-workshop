# ML.NET Workshop

Welcome to the ML.NET Workshop!

[ML.NET](https://dot.net/ml) is an open source, cross-platform, machine learning framework for .NET developers. You can use ML.NET to create custom machine learning models without having prior machine learning experience and without leaving the .NET ecosystem.

In this workshop, we will build, train, and consume a machine learning model that predicts the price of used cars based on factors such as car make, model, and mileage. We will also learn about the basics of machine learning, the various ML.NET framework features and tooling, and how to easily get started with ML.NET.

![Used Car Price](labs/media/consume-model.png)

## Project Structure

This app is made up of seven projects:

1. **Shared**: C# .NET Standard library that has shared code
1. **TrainConsole**: C# .NET Core console appplication for training a regression model to predict the price of a car.
1. **Web**: ASP.NET Core Razor Pages app that uses the model to predict car prices
1. **ImageTrainConsole**: C# .NET Core console application for training a deep learning model to classify images of car damage.
1. **ONNXConsole**: C# .NET Core console application to build a pipeline that uses an ONNX model from Azure Custom Vision to detect car damage.
1. **DataTests**: .NET Core MSTest application to run data validation tests.
1. **ModelTests**: .NET Core MSTest application to run ML.NET model validation tests.

## Getting Started

Go ahead and clone this repo to your machine, then dive in and [get started](/labs/00-get-started.md)!

## Phases

| Phase | Topics |
| ----- | ---- |
| [Phase #0](/labs/00-get-started.md) | Get bits installed and set up your environment |
| [Phase #1](/labs/01-add-ml-context.md) | Install the ML.NET NuGet package, initialize an ML.NET environment |
| [Phase #2](/labs/02-loading-data.md) | Load data for training  |
| [Phase #3](/labs/03-training.md) | Choose data transforms and algorithms, train the model |
| [Phase #4](/labs/04-evaluate.md) | Evaluate model performance |
| [Phase #5](/labs/05-save-model.md) | Save the ML.NET model |
| [Phase #6](/labs/06-consume-model.md) | Consume the model in a web app |
| [Phase #7](/labs/07-mlops.md) | Automate the ML Lifecycle with MLOps |
| [Phase #8](/labs/08-jupyter.md) | Set up .NET Interactive kernel for Jupuyter notebooks |
| [Phase #9](/labs/09-dataframe.md) | Explore data with the .NET DataFrame API |
| [Phase #10](/labs/10-deep-learning.md) | Train a deep learning image classification model |
| [Phase #11](/labs/11-onnx.md) | Use an object detection ONNX model from Azure Custom Vision |
