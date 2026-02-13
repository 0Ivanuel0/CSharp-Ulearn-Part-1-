using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StructBenchmarking;

public class Experiments
{
    public static ChartData BuildChartDataForArrayCreation(
        IBenchmark benchmark, int repetitionsCount)
    {
        return GetNewChartData("Create array", new ArrayCreationFactory(), benchmark, repetitionsCount);
    }

    public static ChartData BuildChartDataForMethodCall(
        IBenchmark benchmark, int repetitionsCount)
    {
        return GetNewChartData("Call method with argument", new MethodCallFactory(), benchmark, repetitionsCount);
    }

    public static ChartData GetNewChartData(
        string title, ICreationFactory factory, IBenchmark benchmark, int repetitionsCount)
    {
        var sizeParams = Constants.FieldCounts;

        var ClassesTimes = new List<ExperimentResult>();
        var StructuresTimes = new List<ExperimentResult>();

        foreach (int size in sizeParams)
        {
            var classTask = factory.GetClassCreation(size);
            var classTime = benchmark.MeasureDurationInMs(classTask, repetitionsCount);
            ClassesTimes.Add(new ExperimentResult(size, classTime));

            var structureTask = factory.GetStructureCreation(size);
            var structureTime = benchmark.MeasureDurationInMs(structureTask, repetitionsCount);
            StructuresTimes.Add(new ExperimentResult(size, structureTime));
        }

        return new ChartData
        {
            Title = title,
            ClassPoints = ClassesTimes,
            StructPoints = StructuresTimes,
        };
    }
}

public class ArrayCreationFactory : ICreationFactory
{
    public ITask GetClassCreation(int size)
    {
        return new ClassArrayCreationTask(size);
    }

    public ITask GetStructureCreation(int size)
    {
        return new StructArrayCreationTask(size);
    }
}

public class MethodCallFactory : ICreationFactory
{
    public ITask GetClassCreation(int size)
    {
        return new MethodCallWithClassArgumentTask(size);
    }

    public ITask GetStructureCreation(int size)
    {
        return new MethodCallWithStructArgumentTask(size);
    }
}

public interface ICreationFactory
{
    ITask GetClassCreation(int size);
    ITask GetStructureCreation(int size);
}