namespace Neumannalex.Pipeline;

public abstract class PipelineHandlerBase<TData> : IPipelineHandler<TData>
{
    private IPipelineHandler<TData> _next;

    protected abstract TData Handle(TData data);

    public TData Execute(TData data)
    {
        var handledData = Handle(data);

        if(_next is null)
            return handledData;
        else
            return _next.Execute(handledData);
    }

    public void SetNext(IPipelineHandler<TData> next)
    {
        _next = next;
    }
}