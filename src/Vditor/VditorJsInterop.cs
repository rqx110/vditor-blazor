using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Vditor
{
    public class VditorJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        public VditorJsInterop(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Vditor/vditor-blazor.js").AsTask());
        }

        public async ValueTask CreateVditor<T>(DotNetObjectReference<T> dotnetRef, ElementReference elementRef,
            string value, Dictionary<string, object> options) where T : ComponentBase
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("createVditor", elementRef, dotnetRef, value, options);
        }

        public async ValueTask<string> GetValue(ElementReference elementRef)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("getValue", elementRef);
        }

        public async ValueTask<string> GetHTML(ElementReference elementRef)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<string>("getHTML", elementRef);
        }

        public async ValueTask SetValue(ElementReference elementRef, string value, bool clearStack = false)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setValue", elementRef, value, clearStack);
        }

        public async ValueTask InsertValue(ElementReference elementRef, string value, bool render = true)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("insertValue", elementRef, value, render);
        }

        public async ValueTask SetPreview<T>(DotNetObjectReference<T> dotnetRef, ElementReference elementRef,
            string markdown,
            Dictionary<string, object> options) where T : ComponentBase
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("preview", elementRef, dotnetRef, markdown, options);
        }

        public async ValueTask DestroyAsync(ElementReference elementRef)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("destroy", elementRef);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}