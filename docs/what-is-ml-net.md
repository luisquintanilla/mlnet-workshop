# What is ML.NET

ML.NET is Microsoft's open source and cross platform machine learning framework.

ML.NET provides tools and features to help .NET developers easily build, train, deploy, and consume high-quality custom machine learning models both locally and in Azure.

## History

About 10 years ago, Microsoft internally developed a machine learning framework called The Learning Code (TLC) to enable developers at Microsoft to add machine learning to the company's products & technologies. Since then, TLC has been widely used internally to add machine learning to features in major products, such as Windows Hello, Bing Ads, and PowerPoint Design Ideas.

In May 2018, Microsoft made the API publicly available, open source, and cross platform, and at Build released the first public preview of ML.NET. The next year at Build 2019, ML.NET became GA.

## Built for .NET developers

ML.NET enables developers to stay in the .NET ecosystem and use their existing .NET skills to easily integrate machine learning into almost any .NET application.

This means that if C#, F#, or VB is your programming language of choice, you no longer have to learn a new programming language, like Python or R, in order to develop your own ML models. You can leverage your existing skills to infuse custom machine learning into your .NET apps without requiring prior machine learning experience. Additionally, you can continue to use your preferred tools, whether that's Visual Studio, VS Code, or the .NET CLI.

## Runs everywhere .NET runs

You can use ML.NET in any .NET Core or .NET Framework application, including web apps and services (ASP.NET MVC, Razor Pages, Blazor, Web API), desktop apps (WPF, WinForms), and more.

![mlnet in the cloud and oses](https://user-images.githubusercontent.com/782127/83193560-430c2800-a105-11ea-8ddf-adbe6eff57e0.png)

This means that you can build and consume ML.NET models on-premises or on any cloud, such as Azure. In addition, because ML.NET is cross-platform, you can run the framework on any OS environment: Windows, Linux, or macOS.

This also applies to offline scenarios.; Yyou can train and consume ML.NET models in offline scenarios such as desktop applications (WPF and WinForms) or any other offline .NET app (excluding ARM processors, which are currently not supported - supports x64 and x86 processor architectures).

## Trusted and proven at scale

ML.NET has evolved into a significant machine learning framework that powers features in many Microsoft products, such as Microsoft Defender ATP, Bing Suggested Search, PowerPoint Design Ideas, Excel Chart Recommendations, and many Azure services.

**TODO:** [break up with photo here]

The framework has demonstrated high performance and accuracy in [experimental evaluations](https://arxiv.org/pdf/1905.05715.pdf). Using a 9GB Amazon review data set, ML.NET trained a sentiment analysis model with 95% accuracy. Other popular machine learning frameworks failed to process the dataset due to memory errors. Training on 10% of the data set, to let all the frameworks complete training, ML.NET demonstrated the highest speed and accuracy. The performance evaluation found similar results in other machine learning scenarios, including click-through rate prediction and flight delay prediction.

![accuracy vs runtime chart](https://user-images.githubusercontent.com/782127/83193742-89fa1d80-a105-11ea-8919-791e3a4ab9cf.png)

Since ML.NET’s launch, many companies have used the framework to add a variety of machine learning scenarios to their .NET apps, like Williams Mullen for law document classification, Evolution Software for hazelnut moisture level prediction, and SigParser for spam email detection.

You can read more about some of these use cases in the [ML.NET Showcase](https://dotnet.microsoft.com/apps/machinelearning-ai/ml-dotnet/customers).

## Train custom models and consume pre-trained models

**TODO:** ML.NET

- talk about difference / which ml tasks support what

## ML.NET tooling + AutoML

**TODO:** Add content

## ML.NET integration with other Microsoft ML tech

There are several ways that you can integrate ML.NET with other Microsoft ML Tech depending on your scenario.

### Azure Cognitive Services – Custom Vision

You can use Custom Vision to train an image classification or object detection model, download that model in the ONNX format, and then use ML.NET for model consumption. The advantage here is that you pay for training and not inference.

### Azure Machine Learning

You can train image classification models with Azure Machine Learning through ML.NET Model Builder. This is great when you have a lot of images in your dataset which would take up local resources for a long time.

### WinML

You can train a model with ML.NET, export to ONNX, and then consume with the WinML for your Windows Desktop apps so you can take advantage of GPU inference.

## When to use ML.NET

ML.NET is a great choice if you:

- Want to stay in the .NET ecosystem
- Don't want to worry about the low-level complexities of machine learning
- Want to train a custom model
- Want to consume an ONNX or TensorFlow model
