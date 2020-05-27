# Get started

In this session, you'll get ready to use ML.NET in an application.

## Setup

TODO: What requirements are there? Visual Studio version? VS Code? Version of .NET Core? Does it work on non-Windows machines?

### Clone the starter application

We've set up an initial project for you to start from. You just need to clone this repo to your machine and then open it up in Visual Studio or your editor of choice. A simple way to clone the application is to run this command from a command line terminal:

```powershell
git clone https://github.com/briacht/HowMuchIsMyCar
```

Once you've done this, change directories to the `HowMuchIsMyCar` folder and again to the `src` folder where you'll find the `HowMuchIsMyCar.sln` file. Open the solution. You should see two projects, similar to what's shown here:

![solution explorer](https://user-images.githubusercontent.com/782127/82521002-7e01d080-9af3-11ea-85bf-a2c5c7da7b4d.png)

### Add the ML.NET Nuget Package

Next, we need to add the ML.NET NuGet package to the `TrainingConsole` project. If you're using Visual Studio, just right click on the project name and select `Manage NuGet Dependencies`. Then click the "Browse" tab and search for `Microsoft.ML`.

![NuGet](https://user-images.githubusercontent.com/782127/82521205-fb2d4580-9af3-11ea-9cf1-3e07463fb735.png)

Alternately if you prefer working from the command line, you can run this command from the `src/TrainingConsole` folder:

```powershell
dotnet add package Microsoft.ML -v 1.5.0-preview2
```

### Build the application

At this point you should be able to build the application for the first time, which you can do from your editor or at the command line with `dotnet build` from the root of the `src` folder.

Next up - [01-add-ml-context](01-add-ml-context.md)
