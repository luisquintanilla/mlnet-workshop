# Add ML Context

In this session, you'll add an ML Context to the Training Console Application. You'll need to add a using statement as well as the MLContext type.

## Modifying the Program

Open the `Program.cs` file in the TrainingConsoleApp project. On the very first line, add a using statement for `Microsoft.ML`:

```c#
using Microsoft.ML;
using System;
```

Next, scroll down to the `static void Main` method and add a new variable declaration on the first line:

```c#
static void Main(string[] args)
{
    MLContext mlContext = new MLContext();

// the rest omitted
```

At this point we're not yet ready to work with the context, but you should be able to successfully build the application once more.

Next up - [02-loading-data](02-loading-data.md)
