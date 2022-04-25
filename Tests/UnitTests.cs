using Xunit;
using FluentAssertions;
using Neumannalex.Pipeline;
using System;
using System.Threading.Tasks;

namespace Tests;

public class UnitTests
{
    [Fact]
    public void Can_create_empty_Pipeline()
    {
        var pipeline = new Pipeline<string>();

        pipeline.HasHandlers.Should().BeFalse();
    }

    [Fact]
    public void Empty_Pipeline_has_default_Configuration()
    {
        var pipeline = new Pipeline<string>();

        PipelineConfiguration expectedConfiguration = new PipelineConfiguration();

        pipeline.Configuration.Should().BeEquivalentTo(expectedConfiguration);
    }

    [Fact]
    public void Custom_Pipeline_Configuration_is_set()
    {
        var pipeline = new Pipeline<string>()
            .Configure(x => x.ContinueOnException = true);

        PipelineConfiguration expectedConfiguration = new PipelineConfiguration
        {
            ContinueOnException = true
        };

        pipeline.Configuration.Should().BeEquivalentTo(expectedConfiguration);
    }

    [Fact]
    public void Can_add_sync_func_handler()
    {
        Action act = () => new Pipeline<string>().AddHandler(x => x.ToLower());
        act.Should().NotThrow();
    }

    [Fact]
    public void Can_add_async_func_handler()
    {
        Action act = () => new Pipeline<string>().AddHandler(async x => { await Task.Delay(10); return x; });
        act.Should().NotThrow();
    }

    [Fact]
    public void Can_add_handlers_from_sync_interface()
    {
        Action act = () => new Pipeline<string>()
            .AddHandler(new SyncHandler());
        act.Should().NotThrow();
    }

    [Fact]
    public void Can_add_handlers_from_async_interface()
    {
        Action act = () => new Pipeline<string>()
            .AddHandler(new AsyncHandler());
        act.Should().NotThrow();
    }

    [Fact]
    public void Can_add_mixed_handlers()
    {
        Action act = () => new Pipeline<string>()
            .AddHandler(new AsyncHandler())
            .AddHandler(x => x.ToLower())
            .AddHandler(new SyncHandler())
            .AddHandler(async x => { await Task.Delay(10); return x; } );
        act.Should().NotThrow();
    }

    [Fact]
    public async void Does_throw_if_continueOnException_is_not_configured()
    {
        var pipeline = new Pipeline<string>()
            .Configure(x => x.ContinueOnException = false)
            .AddHandler(x => x.ToLower())
            .AddHandler(x => { throw new Exception(); return x; })
            .AddHandler(x => x.ToUpper());

        
        Func<Task> act = pipeline.Awaiting(x => x.ExecuteAsync("test"));
        await act.Should().ThrowAsync<PipelineException>();
    }

    [Fact]
    public async void Does_not_throw_if_continueOnException_is_configured()
    {
        var pipeline = new Pipeline<string>()
            .Configure(x => x.ContinueOnException = true)
            .AddHandler(x => x.ToLower())
            .AddHandler(x => { throw new Exception(); return x; })
            .AddHandler(x => x.ToUpper());

        Func<Task> act = pipeline.Awaiting(x => x.ExecuteAsync("test"));
        await act.Should().NotThrowAsync<PipelineException>();
    }

    [Fact]
    public async void Does_apply_handlers()
    {
        var pipeline = new Pipeline<string>()
            .Configure(x => x.ContinueOnException = true)
            .AddHandler(x => x.ToLower())
            .AddHandler(x => x.ToUpper())
            .AddHandler(x => string.Join(' ', x.ToCharArray()) );
        
        var result = await pipeline.ExecuteAsync("Test");

        result.Should().Be("T E S T");
    }
}