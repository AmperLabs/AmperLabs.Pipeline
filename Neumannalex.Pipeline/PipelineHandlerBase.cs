namespace Neumannalex.Pipeline;

public abstract class PipelineHandlerBase<TData> : IPipelineHandler<TData>
{
    private IPipelineHandler<TData> _next;

    protected abstract void Handle(TData data);

    public void Execute(TData data)
    {
        Handle(data);

        _next?.Execute(data);
    }
}