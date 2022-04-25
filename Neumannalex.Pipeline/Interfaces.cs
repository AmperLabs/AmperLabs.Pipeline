namespace Neumannalex.Pipeline;

public interface IPipelineHandler<TData>
{
    TData Handle(TData data);    
}

public interface IAsyncPipelineHandler<TData>
{
    Task<TData> Handle(TData data);    
}