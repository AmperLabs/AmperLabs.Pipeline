namespace Neumannalex.Pipeline;

public class PipelineBuilder<TData> where TData : class
{
    private List<IPipelineHandler<TData>> _handlers = new();
    public PipelineBuilder<TData> WithHandler<THandler>(THandler handler) where THandler : IPipelineHandler<TData>
    {
        _handlers.Add(handler);

        return this;
    }

    public Pipeline<TData> Build()
    {
        return new Pipeline<TData>(_handlers);
    }
}