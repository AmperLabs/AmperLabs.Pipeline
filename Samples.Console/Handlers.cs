using Neumannalex.Pipeline;
using System;

namespace Samples.Console;

public class Handler1 : IPipelineHandler<string>
{
    public string Handle(string data)
    {
        System.Console.WriteLine($"Handler {this.GetType().Name} is handling {data}.");

        PrintSignature();

        return data += $" (modified by [{this.GetType().Name}])";
    }

    private void PrintSignature()
    {
        System.Console.WriteLine("== I am the signature of Handler 1 ==");
    }
}

public class Handler2 : IPipelineHandler<string>
{
    public string Handle(string data)
    {
        System.Console.WriteLine($"Handler {this.GetType().Name} is handling {data}.");

        return data += $" (modified by [{this.GetType().Name}])";
    }
}

public class AsyncHandler1 : IAsyncPipelineHandler<string>
{
    public async Task<string> Handle(string data)
    {
        System.Console.WriteLine($"Handler {this.GetType().Name} is handling '{data}'.");
        await Task.Delay(10);
        return data;
    }
}
