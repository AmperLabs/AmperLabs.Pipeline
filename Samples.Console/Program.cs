using Neumannalex.Pipeline;
using Samples.Console;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var pipeline = new Pipeline<string>()
    .Configure(x => {
        x.ContinueOnException = true;
    })
    .AddHandler(x => x.ToUpper())
    .AddHandler("Faulty step", x => {
        throw new Exception("test");
        return x;
    })
    .AddHandler(x => x.ToLower());


try
{
    var result = await pipeline.ExecuteAsync("test");
    Console.WriteLine($"Result: {result}");

    if(pipeline.Configuration.ContinueOnException && pipeline.HasExceptions)
    {
        Console.WriteLine(
            pipeline.Exceptions.Count == 1 ?
            "There was one exceptions thrown while executing the pipeline." :
            $"There where {pipeline.Exceptions.Count} exceptions thrown while executing the pipeline.");

        pipeline.Exceptions.ForEach(x => {
            Console.WriteLine(x.Message);
            if(x.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {x.InnerException.Message}");
            }
        });
    }
}
catch
{
    Console.WriteLine("Caught exception");
}
