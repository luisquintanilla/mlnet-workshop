# Phase 7.0: MLOps

In this section you'll learn how to automate the model lifecycle from training to model deployment. 
We will in addition look at some additional considerations such as data and model tests.

## Phase 7.1: Create our first GitHub Action workflow
The first thing we want to do is to create a simple GitHub Action workflow which will be triggered to train a new model when a commit is pushed to our repository.

To do so, navigate to your forked repo and click on the `Actions` tab
On the page that appears, go ahead and select to set up a new .NET Core workflow
![action](https://github.com/aslotte/mlnet-workshop/blob/master/labs/media/action-dotnet-core-workflow.PNG)

GitHub will provide you with a template workflow that is intended to restore, build and test a .NET Core app. 

Replace the workflow file with the following content and commit it to your master branch.

```
name: .NET Core

on:
  push:
    branches: [ master ]
  pull_request:
    branches: [ master ]
  
jobs:
  build:

    runs-on: ubuntu-latest

    steps:        
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 3.1.101   
    - name: Install dependencies
      run: dotnet restore src/MLNETWorkshop.sln
    - name: Build
      run: dotnet build src/MLNETWorkshop.sln --configuration Release --no-restore
``` 

If all goes well, a successful build should complete in less than a minute.

## Phase 7.2: Set up our data source
Great work, we're now able to compile our training project as part of our CI pipeline to ensure the integrity of our system. The next step is to automatically kick-off the training of our machine learning model. Before we can do that do we need to address a challenge we will face, which is the location of our training data. So far in this workshop you've had your training data available on disk, or as part of the GitHub repository. However, in many cases the training data is of 1-100 Gb large which makes it non-feasible to store it in GitHub. 

One way to solve this problem is to upload our data to an Azure FileShare and mount the fileshare on our Ubuntu build agent as part of each build so that the training application can access it. An Azure FileShare can handle concurrent load meaning that multiple build agents can read the data simultaneously. 

In order to achieve this we need to do two things:
1) Change the path to our data
2) Mount the fileshare as part of our Github workflow

### Change the path to our data
Navigate to the `Program.cs` file and change the `TRAIN_DATA_PATH` variable to:
```
  private static string TRAIN_DATA_FILEPATH = @"/media/data/true_car_listings.csv";
```

In addition, we'll also need to change the path to where we store our model. To do so, change the `MODEL_FILEPATH` variable to
```
  private static string MODEL_FILEPATH = MLConfiguration.GetModelPath();
```

What this will do is to store the model on the fileshare as well, with a unique id matching the Git commit sha.
Commit the changes to your master branch and push the changes to your repo.

### Mount the fileshare as part of our GitHub workflow
To mount the fileshare as part of our workflow, add the following just before the `Install dependencies` step and commit the changes to your master branch.
```
 - name: 'Create mount points'
      run: 'sudo mkdir /media/data'
    - name: 'Map disk drive to Azure Files share folder'
      run: 'sudo mount -t cifs //ndcmelbourne.file.core.windows.net/data /media/data -o vers=3.0,username=ndcmelbourne,password=${{ secrets.STORAGEKEY }},dir_mode=0777,file_mode=0777'
```

To be able to mount the fileshare, we'll also need to add the access key to the Azure Storage Container as a secret.
To add a secret, navigate to the `Settings` tab and select `Secrets` in the left menu:

![secrets](https://github.com/aslotte/mlnet-workshop/blob/master/labs/media/secrets.PNG)

Click on `New Secret` and add a new secret with the name of `STORAGEKEY`. The value will be provided to you by the facilitators of the workshop.

## Phase 7.3: Train our model as part of our CI pipeline
In order to automatically train our model, we'll need to use the `dotnet run` command to run our console application.
To do so, go ahead and add the following to your GitHub Action's workflow file:
```
    - name: Train
      working-directory: 'src/TrainConsole'
      run: dotnet run --project TrainConsole.csproj.csproj
```

Your workflow file should now look as below. The GitHub action should take a couple of minutes to complete, and if all is setup correctly be successful. 

```
name: .NET Core

on:
  push:
    branches: [ master ]
  pull_request:
    branches: [ master ]
  
jobs:
  build:

    runs-on: ubuntu-latest

    steps:        
    - uses: actions/checkout@v2
    - name: Setup .NET Core
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 3.1.101   
    - name: 'Create mount points'
      run: 'sudo mkdir /media/data'
    - name: 'Map disk drive to Azure Files share folder'
      run: 'sudo mount -t cifs //ndcmelbourne.file.core.windows.net/data /media/data -o vers=3.0,username=ndcmelbourne,password=${{ secrets.STORAGEKEY }},dir_mode=0777,file_mode=0777'
    - name: Install dependencies
      run: dotnet restore src/MLNETWorkshop.sln
    - name: Build
      run: dotnet build src/MLNETWorkshop.sln --configuration Release --no-restore
    - name: Train
      working-directory: 'src/TrainConsole'
      run: dotnet run --project TrainConsole.csproj.csproj 
```

