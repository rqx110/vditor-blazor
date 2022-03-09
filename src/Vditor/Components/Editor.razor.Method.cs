using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Vditor
{
    public partial class Editor
    {
        [Inject] private VditorJsInterop JsInterop { get; set; }

        public ValueTask CreateVditor()
        {
            return JsInterop.CreateVditor(DotNetObjectReference.Create(this), _ref, Value, Options);
        }

        public ValueTask<string> GetValue()
        {
            return JsInterop.GetValue(_ref);
        }

        public ValueTask<string> GetHTML()
        {
            return JsInterop.GetHTML(_ref);
        }

        public ValueTask SetValue(string value, bool clearStack = false)
        {
            return JsInterop.SetValue(_ref, value, clearStack);
        }

        public ValueTask InsertValue(string value, bool render = true)
        {
            return JsInterop.InsertValue(_ref, value, render);
        }

        public ValueTask DestroyAsync()
        {
            return JsInterop.DestroyAsync(_ref);
        }
    }
}