# MLOps

This section provides some information and resources on MLOps.

## What is MLOps

Machine Learning Operations (MLOps) is based on [DevOps](https://wikipedia.org/wiki/DevOps) principles and practices that increase the efficiency of workflows. For example, continuous integration, delivery, and deployment. MLOps applies these principles to the machine learning process.

## Automate the ML lifecycle

![MLOps Cycle](https://user-images.githubusercontent.com/46974588/83207091-5973ad00-a120-11ea-92d5-87738370ab0d.png)

Like DevOps, the MLOps lifecycle consists of the automation of several steps. This is typically done by some build system as part of a Continuous Integration (CI) and Continuous Delivery (CD) pipeline.

### Train

Train: In a typical scenario, when data changes or a Data Scientist checks a change into the Git repo for a project, the build system will start a training run.

### Evaluate

Evaluate: The results of the run can then be inspected to see the performance characteristics of the trained model. This can be done using traditional methods like unit tests.

### Deploy

Deploy: As part of the release pipeline can also create a pipeline that deploys the model. Deployment can have multiple meanings. Deployment mean publishing the validated production version of the model to a file share or blob storage to be accessed downstream from other end-user applications and services. .NET provides various deployment targets, so this could mean deploying the model inside of a desktop (WPF,UWP), web (Blazor, Razor Pages, Web API, Docker, Serverless),  Mobile*, IoT*.

*Deploying to ARM environments (IoT/Mobile) might require converting the model to ONNX.

## Additional tasks

- [Task 1: Build a project that trains, tests, and deploys a sentiment analysis model](https://github.com/luisquintanilla/MLNETCICDTest)

## Additional resources

- [MLOps for ML.NET Presentation](https://www.youtube.com/watch?v=0iwL9cfkYfY)
- [Azure DevOps documentation](https://docs.microsoft.com/azure/devops/?view=azure-devops)
- [Continous Delivery for Machine Learning](https://martinfowler.com/articles/cd4ml.html)
- [ML.NET Model Lifecycle with Azure DevOps CI/CD pipelines](https://devblogs.microsoft.com/cesardelatorre/ml-net-model-lifecycle-with-azure-devops-ci-cd-pipelines/)s