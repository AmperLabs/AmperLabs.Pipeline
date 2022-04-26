using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmperLabs.Pipeline {
    public class Pipeline<TData> where TData : class
    {
        private List<PipelineItem<TData>> _items = new List<PipelineItem<TData>>();

        public PipelineConfiguration Configuration { get; set; } = new PipelineConfiguration();

        public List<Exception> Exceptions { get; set; } = new List<Exception>();

        public bool HasExceptions => Exceptions.Count > 0;

        public bool HasHandlers => _items.Count > 0;
        
        public Pipeline<TData> Configure(Action<PipelineConfiguration> config)
        {
            if(config != null)
                config(Configuration);

            return this;
        }

        public Pipeline<TData> AddHandler(Func<TData, Task<TData>> handler)
        {
            AddHandler(_items.Count.ToString(), handler);
            return this;
        }

        public Pipeline<TData> AddHandler(string id, Func<TData, Task<TData>> handler)
        {
            _items.Add(new PipelineItem<TData>{
                Id = id,
                Handler = handler
            });

            return this;
        }

        public Pipeline<TData> AddHandler(IAsyncPipelineHandler<TData> handler)
        {
            return AddHandler(_items.Count.ToString(), handler);
        }

        public Pipeline<TData> AddHandler(string id, IAsyncPipelineHandler<TData> handler)
        {
            _items.Add(new PipelineItem<TData>{
                Id = id,
                Handler = handler.Handle
            });

            return this;
        }

        public Pipeline<TData> AddHandler(IPipelineHandler<TData> handler)
        {
            return AddHandler(_items.Count.ToString(), handler);
        }

        public Pipeline<TData> AddHandler(string id, IPipelineHandler<TData> handler)
        {
            return AddHandler(id, handler.Handle);
        }

        public Pipeline<TData> AddHandler(Func<TData, TData> handler)
        {
            AddHandler(_items.Count.ToString(), handler);
            return this;
        }

        public Pipeline<TData> AddHandler(string id, Func<TData, TData> handler)
        {
            AddHandler(id, x => Task.FromResult(handler(x)));
            return this;
        }

        public Pipeline(){ }

        public Pipeline(IEnumerable<Func<TData, Task<TData>>> handlers, PipelineConfiguration configuration = null)
        {
            foreach(var handler in handlers)
                AddHandler(handler);

            if(configuration != null)
                Configuration = configuration;
        }

        public async Task<TData> ExecuteAsync(TData data)
        {
            Exceptions.Clear();

            var handledData = data;

            for(int i = 0; i < _items.Count; i++)
            {
                try
                {
                    handledData = await _items[i].Handler(handledData);
                }
                catch(Exception ex)
                {
                    var pipelineEx = new PipelineException($"Exception was thrown in handler with Id '{_items[i].Id}' at position {i} in the pipeline.", ex);
                    
                    Exceptions.Add(pipelineEx);

                    if(!Configuration.ContinueOnException)
                        throw pipelineEx;
                }
            }

            return handledData;
        }
    }
}