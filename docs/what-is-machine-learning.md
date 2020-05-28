# What is Machine Learning

## Artificial Intelligence vs. Machine Learning

Artificial intelligence is "the ability of a digital computer… to perform tasks commonly associated with intelligent beings… such as the ability to reason, discover meaning, generalize, or learn from past experience."
  
In other words, artificial intelligence is machines imitating human abilities and behavior.
 
Typically, artificial intelligence started as rule-based systems; however, using traditional AI techniques are difficult to scale because you can't model behavior with a bunch of "if statements".
  
Examples of artificial intelligence include self-driving cars and robo-readers for grading essays.

![futuristic car](https://user-images.githubusercontent.com/782127/83189340-135a2180-a0ff-11ea-8d79-f36151922f0c.png)

Within AI, there are a variety of subsets, including machine learning. Machine learning is getting computers to make predictions without being explicitly programmed. Instead, computers find patterns in data and learn from experience in order to act on new data.

Machine learning is used to solve problems that are difficult (or impossible) to solve with rules-based programming. For instance, if you were asked to write a function that predicts the price of a shirt based on the description of the shirt, you might start by looking at keywords such as "long sleeves" and "business casual." However, as the list of shirt features grows, you might not know how to build a function to scale that.

![rules-data-answers](https://user-images.githubusercontent.com/10437687/83196461-cc652f80-a0f0-11ea-9930-30cbe4b35b3f.png)

To go even further, deep learning is a subset of machine learning based on artificial neural networks which imitates the way the human brain learns, thinks, and processes data. It’s called deep learning because the neural networks form many layers; for instance, Deep Neural Networks (DNNs) are comprised of a network of layers of nodes in which each layer performs a complex operation. Deep learning is used for a variety of scenarios, including image classification, object detection, speech recognition, and natural language processing.

![identifying-dogs-in-photos](https://user-images.githubusercontent.com/782127/83189469-1ead4d00-a0ff-11ea-82e4-456515358cd8.png)

Here’s how it all fits together:

![ai-ml-deep-learning](https://user-images.githubusercontent.com/10437687/83196387-a9d31680-a0f0-11ea-9870-ffc1b2c8b215.png)

## What problems can you solve with Machine Learning?

Machine learning can be used to solve a ton of different types of problems. Machine learning can be split up into tasks (the type of prediction being made based on the problem or question that is being asked and the available data), each of which has a variety of scenarios. 

### Classification

Classification is used to predict the class (category) of an instance of data. Often times this is split up into binary classification (2 classes) and multi-class classification (more than 2 classes), and it can be used to classify text, images, and more.

**TODO**: [add image of text classification and image classification]

Examples include:

- Classifying law documents
- Predicting if an image is of a cat or a dog
- Categorizing e-mail as spam or not spam
- Automatically adding labels to GitHub issues
- Predicting if a comment is positive or negative
- Diagnosing whether a patient has a certain disease or not
- Classifying movie reviews as "positive", "neutral", or "negative"
- Categorizing hotel reviews as "location", "price", "cleanliness", etc.

## Regression

Regression is used to predict a numeric value based on a set of characteristics.

![graph-price-to-size](https://user-images.githubusercontent.com/782127/83190070-de9a9a00-a0ff-11ea-9a21-2b25f45e5ffd.png)

Examples include:

- Predicting house prices based on house attributes such as number of bedrooms, location, or size
- Predicting sales of a product based on advertising budgets

## Recommendation

Recommendation enables producing a list of recommended items to users. 

![netflix-recommendations](https://user-images.githubusercontent.com/782127/83190285-31745180-a100-11ea-95e8-6c0c4ea61ba9.png)

Examples include:

- Recommending products to shoppers
- Recommending movies, songs, books, etc.

## Forecasting

Forecasting uses past time-series data to make predictions about future behavior. Time series data is a series of data points indexed in time order. 

![sales-forecast-chart](https://user-images.githubusercontent.com/782127/83190377-5668c480-a100-11ea-9651-29dd3d86bb0c.png)

Examples include:

- Predicting future sales based on past sales
- Predictive maintenance
- Weather forecasting

## Anomaly Detection

Anomaly detection detects spikes or change points in data over time. 

![a graph with spikes](https://user-images.githubusercontent.com/782127/83190491-7d26fb00-a100-11ea-9b9f-ac4af0bcd9e2.png)

Examples include:

- Identifying transactions that are potentially fraudulent.
- Learning patterns that indicate that a network intrusion has occurred.
- Finding abnormal clusters of patients.
- Checking values entered into a system.

## Clustering

Clustering is used to group instances of data into clusters that contain similar characteristics.

![clusters of colored squares on a grid](https://user-images.githubusercontent.com/782127/83190562-9c258d00-a100-11ea-87ed-6a63927cc7e2.png)

Examples include:

- Customer segmentation

## Machine Learning Terminology

When talking about machine learning, there are some key concepts and terms to know.

### Model

A model is a mathematical representation of a real-world process. A machine learning model specifies the steps needed to transform your input data into a prediction. To get a machine learning model, you have to provide data to an algorithm, which learns from the data and outputs a trained model.  

### Algorithm

An algorithm is a set of functions which, given a scenario or objective, helps a model adapt to the given data. In other words, algorithms turn a dataset into a model. Each machine learning task has its own set of algorithms.

### Training

Training is learning process for a model. It is when the algorithm finds patterns in the given dataset and outputs a model which can then be used to make predictions on new data.

### Consuming a model (or inferencing/scoring)

Consuming a model simply means using the model in your end-user application to make a prediction. Inference is also another way to say consuming a model / making predictions with the model.

### Feature

Features are the independent variables in your training data which are the inputs to the algorithm. For instance, if you are trying to predict the cost/amount of taxi fare, then your Features might be things like distance travelled, payment type, and number of passengers, since all these factors would contribute to the final price.

### Label

The Label is the output that you'd like to predict. In the taxi fare example, the amount of the taxi fare is the Label.

### Supervised vs. Unsupervised

In supervised learning, you use data which has Labels in order to predict the outcome of unforeseen data. Classification, regression, and recommendation are all supervised machine learning tasks.

Unsupervised learning uses unlabeled data to find new, unknown patterns in data and find features that can be useful for classification. Clustering is an unsupervised machine learning task.

### Training data

Training data is the data you use as input to the algorithm in order to get a model.

### Test data

Test data is used to evaluate the trained model by making predictions on the test data and comparing the predicted values to actual values. A common technique is to split a dataset into a train dataset (80% of original dataset used to train the model) and a test dataset (20% used to assess the model's performance).

### Hyperparameters

Hyperparameters are algorithm options that you can change and tune in order to improve the model based on the data given.

### Data Transformations

Machine learning algorithms generally can’t directly use the data you have available for training; you need to use data transformations to pre-process the raw data and convert it into a format that algorithms can accept.

### Pre-built vs. Custom Models

Pre-built models are models that are already trained on data and ready to consume in your app. In this case, you just need a way to consume the model in your app.

Custom models are models that are trained on your own data.

## Machine learning workflow

Now that you've got the basic terminology down, here's the general steps and workflow for machine learning:

![prepare build train run](https://user-images.githubusercontent.com/10437687/83197121-d20f4500-a0f1-11ea-8c63-80c9baa54d31.png)

### Prepare your data

Without data there is no Machine Learning, so the first step is to collect and prepare your data. Preparing your data can include labelling, merging different data sources, and cleaning up the data (removing missing values, etc.).

### Build & train your model

The process of building and training a model is normally done in a separate application (for instance, in a console app) than where the model will be eventually consumed.

As mentioned above, machine learning algorithms accept data in a very specific format, so normally you must use data transformations to pre-process the raw data and convert it into an acceptable format to input to the algorithm.

After applying the data transformations, you must also choose an algorithm that best fits your use case and scenario. Many algorithms also have numerous hyperparameters (or options) which you can tune to improve the performance of the model.

Before using a trained model in production, you want to make sure it achieves the desired quality when making predictions. There are multiple evaluation metrics used to determine model performance, such as accuracy, AUC, and RMSE.

The process of building a machine learning model is cyclical. If you evaluate your model and determine that the performance is not high enough, you can improve your model in several ways, such as re-training the data with more data and or re-training for a longer time. If you continuously receive new data, you may want to incorporate that into your model training.  

### Consume your model

When your model has achieved the desired quality and performance, you can consume the model in your end-user application. This can involve loading the model, setting the input data, and using the loaded model to make predictions on the input data.

Depending on the ML framework / technology that you use, there are several ways to deploy the model, which we will get into later on.

## Automated Machine Learning (AutoML)

Choosing the correct machine learning data transformations, algorithms, and algorithm options can be a daunting task, especially if you don’t have a data science or ML background. Automated Machine Learning, or AutoML, makes this task a lot easier by automating model selection.  

AutoML will take your ML scenario and dataset and will iterate through different combinations of data transformations and algorithms to give you the best performing model.

## Machine learning landscape

There are a ton of different frameworks, libraries, tools, and technologies for machine learning. Some have the ability to train custom models, some are just for consuming models, and some have the ability to do both those tasks and more. Even within Microsoft, there are a variety of different machine learning offerings. Below is a break-down of a few of the most popular ML technologies.

**TODO:** [maybe have code snippets for sentiment analysis for each fx???]

### External

#### Scikit-learn

Scikit-learn is a free, open source machine learning library for Python. Scikit-learn is built on NumPy, SciPy, and matplotlib, and it's used for classical machine learning tasks like classification, regression, and clustering.

#### TensorFlow

TensorFlow is a free, open source library for machine learning created by Google. It uses Python to provide a front-end API for building applications with the framework while executing those applications in C++. It's used for large scale machine learning and complex workflows, and it is popular for deep learning training.

#### Keras

Keras is a free, open-source neural-network library written in Python. Specifically, it's a high-level, user-friendly API for building neural network models. TensorFlow (as of its 2.0 release) has adopted Keras as its high-level API.

#### PyTorch

PyTorch is a free Python-based scientific computing package for deep learning based on the Torch library created by Facebook. It's known for being flexible and fast.

#### H2O

H2O is an open source, distributed in-memory machine learning platform created by a company called H2O.ai. H2O is written in Java and offers interfaces for R, Python, Scala, Java, JSON and CoffeeScript/JavaScript, along with a built-in web interface. H2O.ai offers a variety of machine learning tools at a cost.  

### Machine learning at Microsoft

#### Azure Cognitive Services

Azure Cognitive Services are APIs, SDKs, and services available to help developers build intelligent applications without having direct AI or data science skills or knowledge. The catalog of services within Azure Cognitive Services are categorized into five main pillars - Vision, Speech, Language, Web Search, and Decision, all of which can be accessed via REST API or SDK calls. A majority of Cognitive Services use pre-trained models which don't require any training data.

Custom Vision is a cognitive service which uses a machine learning algorithm to apply labels to images (image classification and object detection). In this case, you provide the training images either via the web portal or an SDK, and in the end you can export the model to a format for making predictions offline.

You can learn more about Cognitive Services and machine learning [here](https://docs.microsoft.com/azure/cognitive-services/cognitive-services-and-machine-learning).

#### Azure Machine Learning (service & studio)

Azure Machine Learning is a fully managed cloud service used to train, deploy, and manage machine learning models at scale. Developers and data scientists can use Azure Machine Learning through various ways.

Azure Machine Learning studio – A browser interface that helps you create and manage assets and resources as part of the end-to-end machine learning workflow. Users can use studio to build workflows with code as well as no-code with the designer.

SDK – Users who prefer managing the resources and building workflows with code, can leverage the SDK available for both Python and R. Using the SDK gives creators of machine learning applications the ability to version control their workflows as well as interact with preview features of the product.

CLI – The Azure Machine Learning CLI is a cross-platform Azure CLI extension that provides commands for Azure Machine Learning. Users can use it to train and deploy machine learning models. The CLI can be useful in automation scenarios and as part of training and deployment pipelines done through CI/CD workflows.

Visual Studio Code Extension – Users who use Visual Studio Code as their editor for building machine learning models can leverage the Azure Machine Learning extension which provides a graphical user interface to manage their end-to-end machine learning workflows and assets.

Learn more about Azure Machine Learning [here](https://docs.microsoft.com/azure/machine-learning/).

#### WinML

Windows Machine Learning (WinML) allows developers to use trained machine learning models in Windows apps written in C#, C++, JavaScript, or Python, either locally on a Windows 10 device or on a Windows Server 2019 machine. WinML supports model consumption only (e.g. you can't train a model with WinML).

#### ML.NET

**TODO:** [quick description of ML.NET here]

Comparison chart of Microsoft offerings:
[//]: # (Table Creator: www.tablesgenerator.com/markdown_tables)

|                          | Training custom models   | Model consumption                                | Requires ML knowledge |
|--------------------------|--------------------------|--------------------------------------------------|-----------------------|
| ML.NET                   | Yes                      | Yes - ML.NET, TensorFlow, ONNX                   | No                    |
| Azure Cognitive Services | Limited to some services | Yes - consume via API/SDK                        | No                    |
| Azure ML                 | Yes                      | Yes - register model and consume via web service | Somewhat              |
| WinML                    | No                       | Yes - ONNX                                       | No                    |

### ONNX

Open Neural Network Exchange (ONNX) is an open format built to represent machine learning models and make them portable. Specifically, it's a standard for exchanging deep learning models. ONNX allows developers and data scientists to use machine learning models with a variety of frameworks, tools, runtimes, & compilers. This means that you can create a model using one programming language or ML library and then execute in an entirely different run time environment.

For instance, if you work with data scientists who use PyTorch to train a model, but you want to use that model in your .NET application, you could export the PyTorch model as an ONNX model and then consume that model using ML.NET.

## Additional Resources

- [Definition of AI](https://www.britannica.com/technology/artificial-intelligence)
- [Differences between ML and software engineering](https://www.futurice.com/blog/differences-between-machine-learning-and-software-engineering/)
- [Algorithm Cheat Sheet](https://docs.microsoft.com/azure/machine-learning/algorithm-cheat-sheet)
- [AI vs ML vs DL](https://docs.microsoft.com/en-us/azure/machine-learning/concept-deep-learning-vs-machine-learning)
