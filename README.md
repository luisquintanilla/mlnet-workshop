# ML.NET Workshop

Welcome to the ML.NET Workshop!

[ML.NET](https://dot.net/ml) is an open source, cross-platform, machine learning framework for .NET developers. You can use ML.NET to create custom machine learning models without having prior machine learning experience and without leaving the .NET ecosystem.

In this workshop, we will build, train, and consume a machine learning model that predicts the price of used cars based on factors such as car make, model, and mileage. We will also learn about the basics of machine learning, the various ML.NET framework features and tooling, and how to easily get started with ML.NET.

![Used Car Price](https://user-images.githubusercontent.com/46974588/88401414-b940aa00-cd97-11ea-9388-468b024e733f.png)

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

Go ahead and clone this repo to your machine, then dive in and [get started](https://aka.ms/mlnet-workshop-content)!
