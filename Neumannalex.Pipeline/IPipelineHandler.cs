namespace Neumannalex.Pipeline;

public interface IPipelineHandler<TData>
{
    TData Handle(TData data);    
}