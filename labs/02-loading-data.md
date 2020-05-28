# Loading Data

In this session, you'll download the data needed to train the model and load it into an `IDataView`. 

Data in ML.NET is represented as an `IDataView`. An IDataView is a flexible and efficient way of describing tabular data (columns and rows). The `IDataView` component provides a very efficient, compositional processing of tabular data especially made for machine learning and advanced analytics applications. It is designed to efficiently handle high dimensional data and large data sets. It is also suitable for single node processing of data partitions belonging to larger distributed data sets. Some key distinctions of the IDataView include:

- *Immutability* - Cursoring through data does not modify input data in any way. The root data is immutable, and the operations performed to materialize derived data are repeatable. Immutability and repeatability enable transparent caching. Immutability also ensures that execution of a composed data pipeline graph is safe for parallelism. The IDataView system's immutability guarantees enable flexible scheduling without the need to clone data.
- *Lazy* - IDataView enables and encourages components to be lazy in both column and row directions. When only a subset of columns or a subset of rows is requested, computation for other columns and rows can be, and generally is, avoided.
- *High dimensionality* - The type system for columns includes homogeneous vector types, so a set of related primitive values can be grouped into a single vector-valued column.

Something else that the `IDataView` component provides is a `DataViewSchema`. As the name suggest, this is the schema of an `IDataView` which defines the types, names, and other annotations for the set of columns that make up an `IDataView`. Before loading data, you must define the schema of that data. You can use classes or Plain-Old-CLR-Objects (POCO) to define a `DataViewSchema`.

ML.NET provides support for loading data into an `IDataView` from various sources:

- *Files* - Load data from sources like text, binary, and image files. You can load data from a single file or multiple files. The supported formats include
- *Databases* - Load data from relational database sources. The following databases are a few of the databases supported:
  - Azure SQL Database
  - Oracle
  - IBM DB2
  - PostgreSQL
  - SQLite
  - SQL Server
- *Other* - Load data that can be represented by an `IEnumerable`. This can include JSON, XML, and many other sources.

See [Load data from files and other sources](https://docs.microsoft.com/dotnet/machine-learning/how-to-guides/load-data-ml-net) for more information on loading data.

## Get the data

First you need to download the data:

[1.2 Million Used Car Listings](https://github.com/luisquintanilla/mlnet-workshop/raw/master/data/true_car_listings.csv) (right-click, Save As and store it in your working folder for the lab)

You can open it and inspect its contents. It should look something like this:

![Used car sale data in CSV format](https://user-images.githubusercontent.com/782127/82711417-575aab80-9c53-11ea-9270-459fb5e79441.png)

Note the column names - we will use them in the next step.

## Create the data model input schema

In the `Shared` project, add a new class called `ModelInput` to the root directory. This class will consist solely of properties and will be mapped from the data file. Each property will reference a column using a 0-based index.

Start by adding the following `using` statement:

```csharp
using Microsoft.ML.Data;
```

Then, define the class as follows:

```csharp
public class ModelInput
{
    [ColumnName("Label"), LoadColumn(0)]
    public float Price { get; set; }

    [LoadColumn(1)]
    public float Year { get; set; }

    [LoadColumn(2)]
    public float Mileage { get; set; }

    [LoadColumn(6)]
    public string Make { get; set; }

    [LoadColumn(7)]
    public string Model { get; set; }
}
```

![Define model input schema](./media/define-modelinput-schema.png)

> Note that not all columns are loaded. Each `LoadColumn` attribute specifies the index of its respective column within the file. The `ColumnName` attribute tells the `IDataView` to identify the `Price` property by the `Label` column. The ground-truth values or the value to predict is known as the **label**. Since we want to be able to predict the price, we treat that as the label. The rest of the columns or inputs are known as **features**. Learn more about [data annotations](https://docs.microsoft.com/dotnet/machine-learning/how-to-guides/load-data-ml-net#annotating-the-data-model-with-column-attributes) and [expected column types](https://docs.microsoft.com/dotnet/machine-learning/how-to-guides/train-machine-learning-model-ml-net#working-with-expected-column-types).

## Load the data from the file into an IDataView

Now open up *Program.cs* file in the `TrainConsole` project (**not** the web project!) and add the following `using` statement:

```csharp
using Shared;
```

Inside of the class definition, add the location of the file wherever you saved it as a static string:

```csharp
class Program
{
    // update this with your file's path where you saved it
    private static string TRAIN_DATA_FILEPATH = @"C:\Dev\true_car_listings.csv";

    //...
}
```

Next, in the `Main` method, add this block of code after `mlContext`:

```csharp
// Load training data
Console.WriteLine("Loading data...");
IDataView trainingData = mlContext.Data.LoadFromTextFile<ModelInput>(path: TRAIN_DATA_FILEPATH, hasHeader: true, separatorChar: ',');
```

At this point, you have defined how to load data into an `IDataView` with a `ModelInput` schema. It's important to remember though that an `IDataView` is lazy and no loading takes place at this stage. We'll explore that in the following section. You're now ready to define the set of data transformations and algorithms used to train your machine learning model.

Next up - [03-training](03-training.md)
