# Get started

In this section you'll set up your environment to build machine learning applications with ML .NET. [ML.NET](https://dot.net/ml) is a cross-platform, machine learning framework for .NET developers.

## Requirements

- [Git(Optional)](https://git-scm.com/)
- GitHub account 

### Windows

- [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) with .NET Core workload installed.
- [Model Builder enabled](https://github.com/dotnet/machinelearning-modelbuilder/issues/757#issuecomment-631665947).

### Mac / Linux

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [ML.NET CLI](https://www.nuget.org/packages/MLNet/)
- [Visual Studio Code (Optional)](https://code.visualstudio.com/Download)
- [.NET Core 2.1 SDK](https://aka.ms/download-netcore-21)

### Fork the repo

We've set up an initial project for you to start from. To get started, please complete the following steps:
- Fork the repo (top right-corner)
- Clone your forked repo locally, e.g. by running the following command (replace luisquintanilla with your GitHub username)
```powershell
git clone https://github.com/luisquintanilla/mlnet-workshop
```

Once you've done this, change directories to the *mlnet-workshop/src* folder where you'll find the *MLNETWorkshop.sln* file. Open the solution. You should see three projects, similar to what's shown here:

![](./media/project-structure.png)

<!-- ![solution explorer](https://user-images.githubusercontent.com/782127/82521002-7e01d080-9af3-11ea-85bf-a2c5c7da7b4d.png) -->

## Build the application

At this point you should be able to build the solution for the first time, which you can do from Visual Studio or the command line with `dotnet build` from the root of the `src` folder.
