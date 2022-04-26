using System;
using System.Threading.Tasks;

namespace AmperLabs.Pipeline {
    public class PipelineItem<TData> where TData : class
    {
        public string Id { get; set; }
        public Func<TData, Task<TData>> Handler { get; set; }
    }
}