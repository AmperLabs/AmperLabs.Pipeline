namespace Neumannalex.Pipeline;

public class AsyncPipeline<TData> where TData : class
{
    private List<Func<TData, Task<TData>>> _handlers = new List<Func<TData, Task<TData>>>();
    
    public void AddHandler(Func<TData, Task<TData>> handler)
    {
        _handlers.Add(handler);
    }

    public void AddHandler(IAsyncPipelineHandler<TData> handler)
    {
        _handlers.Add(handler.Handle);
    }

    public AsyncPipeline()
    {
        _handlers = new List<Func<TData, Task<TData>>>();
    }

    public AsyncPipeline(IEnumerable<Func<TData, Task<TData>>> handlers)
    {
        _handlers = new List<Func<TData, Task<TData>>>();

        foreach(var handler in handlers)
            AddHandler(handler);
    }

    public async Task<TData> Execute(TData data)
    {
        var handledData = data;

        foreach(var handler in _handlers)
        {
            handledData = await handler(handledData);
        }

        return handledData;
    }
}
