using Neumannalex.Pipeline;
using Samples.Console;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var pipeline = new PipelineBuilder<string>()
    .WithHandler(new Handler1())
    .WithHandler(new Handler2())
    .Build();

var result = pipeline.Execute("test");

Console.WriteLine($"Result: {result}");