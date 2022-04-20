namespace Neumannalex.Pipeline;

public class PipelineBuilder<TData> where TData : class
{
    private List<Func<TData, TData>> _handlers = new();
    public PipelineBuilder<TData> WithHandler<THandler>(THandler handler) where THandler : IPipelineHandler<TData>
    {
        _handlers.Add(handler.Handle);
        return this;
    }

    public PipelineBuilder<TData> WithHandler(Func<TData, TData> handler)
    {
        _handlers.Add(handler);
        return this;
    }

    public Pipeline<TData> Build()
    {
        return new Pipeline<TData>(_handlers);
    }


    public static PipelineBuilder<TData> CreatePipeline()
    {
        return new PipelineBuilder<TData>();
    }
}