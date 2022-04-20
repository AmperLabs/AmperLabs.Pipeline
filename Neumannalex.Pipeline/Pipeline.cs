namespace Neumannalex.Pipeline;

public class Pipeline<TData> where TData : class
{
    private List<Func<TData, TData>> _handlers = new List<Func<TData, TData>>();
    
    public void AddHandler(Func<TData, TData> handler)
    {
        _handlers.Add(handler);
    }

    public void AddHandler(IPipelineHandler<TData> handler)
    {
        _handlers.Add(handler.Handle);
    }

    public Pipeline()
    {
        _handlers = new List<Func<TData, TData>>();
    }

    public Pipeline(IEnumerable<Func<TData, TData>> handlers)
    {
        _handlers = new List<Func<TData, TData>>();

        foreach(var handler in handlers)
            AddHandler(handler);
    }

    public TData Execute(TData data)
    {
        var handledData = data;

        foreach(var handler in _handlers)
        {
            handledData = handler(handledData);
        }

        return handledData;
    }
}
