using Neumannalex.Pipeline;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var h1 = new Handler1();



public class Handler1 : PipelineHandlerBase<string>, IPipelineHandler<string>
{

}

