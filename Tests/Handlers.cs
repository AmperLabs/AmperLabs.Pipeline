using System.Threading.Tasks;
using AmperLabs.Pipeline;

namespace Tests;

public class SyncHandler : IPipelineHandler<string>
{
    public string Handle(string data)
    {
        return data;
    }
}

public class AsyncHandler : IAsyncPipelineHandler<string>
{
    public async Task<string> Handle(string data)
    {
        await Task.Delay(10);
        return data;
    }
}
