namespace Neumannalex.Pipeline;

public class AsyncPipelineBuilder<TData> where TData : class
{
    private List<Func<TData, Task<TData>>> _handlers = new();
    public AsyncPipelineBuilder<TData> WithHandler<THandler>(THandler handler) where THandler : IAsyncPipelineHandler<TData>
    {
        _handlers.Add(handler.Handle);
        return this;
    }

    public AsyncPipelineBuilder<TData> WithHandler(Func<TData, Task<TData>> handler)
    {
        _handlers.Add(handler);
        return this;
    }

    public AsyncPipeline<TData> Build()
    {
        return new AsyncPipeline<TData>(_handlers);
    }


    public static AsyncPipelineBuilder<TData> CreatePipeline()
    {
        return new AsyncPipelineBuilder<TData>();
    }
}
