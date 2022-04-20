using Neumannalex.Pipeline;
using Samples.Console;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var pipeline = PipelineBuilder<string>
    .CreatePipeline()
    .WithHandler(new Handler1())
    .WithHandler(x => $"Handled by handler 1.5 {x}")
    .WithHandler(new Handler2())
    .WithHandler(x => {
        return $"Another handler handled '{x}'";
    })
    .Build();

var result = pipeline.Execute("test");
Console.WriteLine($"Result: {result}");

Console.WriteLine("---------------------------------------------------------");

var asyncPipeline = AsyncPipelineBuilder<string>
    .CreatePipeline()
    .WithHandler(async x => {
        Console.WriteLine($"Received '{x}' at {DateTime.Now} in step 1");
        return await Task.FromResult(x);
    })
    .WithHandler(async x => {
        Console.WriteLine($"Received '{x}' at {DateTime.Now} in step 2");
        await Task.Delay(2000);
        return x;
    })
    .WithHandler(async x => {
        Console.WriteLine($"Received '{x}' at {DateTime.Now} in step 3");
        return await Task.FromResult(x);
    })
    .WithHandler(new AsyncHandler1())
    .Build();

var asyncResult = await asyncPipeline.Execute("test");
Console.WriteLine($"Result: {asyncResult}");
