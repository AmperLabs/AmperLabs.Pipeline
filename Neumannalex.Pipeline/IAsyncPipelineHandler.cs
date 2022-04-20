namespace Neumannalex.Pipeline;

public interface IAsyncPipelineHandler<TData>
{
    Task<TData> Handle(TData data);    
}