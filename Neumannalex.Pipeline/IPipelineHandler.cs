namespace Neumannalex.Pipeline;

public interface IPipelineHandler<TData>
{

    void Execute(TData data);    
}

public interface IPipelineHandler<TData, TResult>
{
    
    TResult Execute(TData data);    
}
