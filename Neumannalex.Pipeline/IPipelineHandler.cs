namespace Neumannalex.Pipeline;

public interface IPipelineHandler<TData>
{
    TData Execute(TData data);    
    void SetNext(IPipelineHandler<TData> next);
}