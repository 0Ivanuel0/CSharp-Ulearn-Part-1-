using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace StructBenchmarking;
public class Benchmark : IBenchmark
{
    public double MeasureDurationInMs(ITask task, int repetitionCount)
    {
        GC.Collect();           
        GC.WaitForPendingFinalizers();
		task.Run();

        var watch = Stopwatch.StartNew();

        for (int i = 0; i < repetitionCount; i++)
            task.Run();

        watch.Stop();

        return watch.Elapsed.TotalMilliseconds / repetitionCount;
	}
}

[TestFixture]
public class RealBenchmarkUsageSample
{
    [Test]
    public void StringConstructorFasterThanStringBuilder()
    {
        var benchmark = new Benchmark();
        const int RepetitionCount = 100;

        var builderTask = new StringBuilderTask();
        var builderTime = benchmark.MeasureDurationInMs(builderTask, RepetitionCount);

        var constructorTask = new StringConstructorTask();
        var constructorTime = benchmark.MeasureDurationInMs(constructorTask, RepetitionCount);

        ClassicAssert.Less(constructorTime, builderTime);
    }

    public class StringBuilderTask : ITask
    {
        public void Run()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < 10000; i++) 
                builder.Append('a');

            var result = builder.ToString();
        }
    }

    public class StringConstructorTask : ITask
    {
        public void Run()
        {
            var result = new string('a', 10000); 
        }
    }
}