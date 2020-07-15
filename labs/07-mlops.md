# Phase 7.0: MLOps

In this section you'll learn how to automate the model lifecycle from training to model deployment. 
We will in addition look at some additional considerations such as data and model tests.

## Phase 7.1: Create our GitHub Action CI pipeline
The first thing we want to do is to create a simple GitHub Action workflow which will be triggered to train a new model when a commit is pushed to our repository.

To do so, navigate to your forked repo and click on the **Actions** tab
--- image here

On the page that appears, go ahead and select to set up a new .NET Core workflow
--- image here

GitHub will provide you with a template workflow that is intended to restore, build and test a .NET Core app. 

Replace the workflow file with the following yaml

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


## Phase 7.2: Set up our data source

## Phase 7.3: Train our model as part of our CI pipeline


