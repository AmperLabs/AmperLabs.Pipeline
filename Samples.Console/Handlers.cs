using Neumannalex.Pipeline;
using System;

namespace Samples.Console;

public class Handler1 : PipelineHandlerBase<string>
{
    protected override string Handle(string data)
    {
        System.Console.WriteLine($"Handler {this.GetType().Name} is handling {data}.");

        return data += $" (modified by [{this.GetType().Name}])";
    }
}

public class Handler2 : PipelineHandlerBase<string>, IPipelineHandler<string>
{
    protected override string Handle(string data)
    {
        System.Console.WriteLine($"Handler {this.GetType().Name} is handling {data}.");

        return data += $" (modified by [{this.GetType().Name}])";
    }
}

