using System;
using System.Threading.Tasks;

namespace Hooks
{
    internal class HookDefinition
    {
        public Action callbackVoidNoArgs;
        public Action<object[]> callbackVoid;

        public Func<object> callbackNoArgs;
        public Func<object[], object> callback;

        public Func<Task<object>> asyncCallbackNoArgs;
        public Func<object[], Task<object>> asyncCallback;

        public int weight;
        public string[] tags;

        private HookDefinition(int weight, string[] tags)
        {
            this.weight = weight;
            this.tags = tags;
        }
        public HookDefinition(Action callback, int weight, string[] tags) : this(weight, tags)
        {
            callbackVoidNoArgs = callback;
        }
        public HookDefinition(Action<object[]> callback, int weight, string[] tags) : this(weight, tags)
        {
            callbackVoid = callback;
        }
        public HookDefinition(Func<object> callback, int weight, string[] tags) : this(weight, tags)
        {
            callbackNoArgs = callback;
        }
        public HookDefinition(Func<object[], object> callback, int weight, string[] tags) : this(weight, tags)
        {
            this.callback = callback;
        }
        public HookDefinition(Func<Task<object>> callback, int weight, string[] tags) : this(weight, tags)
        {
            asyncCallbackNoArgs = callback;
        }
        public HookDefinition(Func<object[], Task<object>> callback, int weight, string[] tags) : this(weight, tags)
        {
            asyncCallback = callback;
        }

    }
}
