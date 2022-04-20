namespace Neumannalex.Pipeline;

public class Pipeline<TData> where TData : class
{
    private List<IPipelineHandler<TData>> _handlers = new List<IPipelineHandler<TData>>();

    private void AddHandler(IPipelineHandler<TData> handler)
    {
        if(_handlers.Count > 0)
            _handlers.Last().SetNext(handler);
        
        _handlers.Add(handler);
    }

    public Pipeline()
    {
        _handlers = new List<IPipelineHandler<TData>>();
    }

    public Pipeline(IEnumerable<IPipelineHandler<TData>> handlers)
    {
        _handlers = new List<IPipelineHandler<TData>>();
        
        foreach(var handler in handlers)
            AddHandler(handler);
    }

    public TData Execute(TData data)
    {
        if(_handlers == null || _handlers.Count == 0)
            throw new ArgumentNullException("No handler was set.");

        return _handlers.First().Execute(data);
    }
}
