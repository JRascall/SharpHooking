using Hooks;
using SharpHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SharpHooking
{
    internal readonly IDictionary<string, IList<HookDefinition>> hooks = new Dictionary<string, IList<HookDefinition>>();

    protected readonly HookingOptions options = new HookingOptions();
    protected readonly IList<CallLog> callLogs = new List<CallLog>();

    public IReadOnlyList<CallLog> GetLogs()
    {
        return (IReadOnlyList<CallLog>)callLogs;
    }

    public static SharpHooking Create()
    {
        return new SharpHooking();
    }

    public static SharpHooking Create(Action<HookingOptions> options)
    {
        return new SharpHooking(options);
    }

    public SharpHooking()
    {
        options.ShowCalls = false;
        options.ShowWarnings = true;
    }

    public SharpHooking(Action<HookingOptions> setup)
    {
        setup(options);
        
        options?.Logger.LogInformation("Sharp Hooking loaded");
    }

    public virtual SharpHooking Register(string name, Action callback, int weight = 0, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, weight, tags));
        return this;
    }
    public virtual SharpHooking Register(string name, Action callback, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, 0, tags));
        return this;
    }

    public virtual SharpHooking Register(string name, Action<object[]> callback, int weight = 0, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, weight, tags));
        return this;
    }
    public virtual SharpHooking Register(string name, Action<object[]> callback, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, 0, tags));
        return this;
    }

    public virtual SharpHooking Register(string name, Func<object> callback, int weight = 0, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, weight, tags));
        return this;
    }
    public virtual SharpHooking Register(string name, Func<object> callback, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, 0, tags));
        return this;
    }

    public virtual SharpHooking Register(string name, Func<object[]> callback, int weight = 0, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, weight, tags));
        return this;
    }
    public virtual SharpHooking Register(string name, Func<object[]> callback, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, 0, tags));
        return this;
    }

    public virtual SharpHooking Register(string name, Func<Task<object>> callback, int weight = 0, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, weight, tags));
        return this;
    }
    public virtual SharpHooking Register(string name, Func<Task<object>> callback, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, 0, tags));
        return this;
    }

    public virtual SharpHooking Register(string name, Func<object[], Task<object>> callback, int weight = 0, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, weight, tags));
        return this;
    }
    public virtual SharpHooking Register(string name, Func<object[], Task<object>> callback, params string[] tags)
    {
        name = name.ToLower();
        CheckAndCreateHookCollection(name);
        AddDefinition(name, new HookDefinition(callback, 0, tags));
        return this;
    }

    private void CheckAndCreateHookCollection(string name)
    {
        if (hooks.ContainsKey(name) == false)
        {
            hooks.Add(name, new List<HookDefinition>());
        }
    }
    private void AddDefinition(string name, HookDefinition def)
    {
        hooks[name].Add(def);
        hooks[name] = hooks[name].OrderBy(x => x.weight).ToList();
    }

    public virtual int Call(string name, params object[] args)
    {
        int count = 0;
        name = name.ToLower();
        options.Logger?.LogInformation(name);
        hooks.TryGetValue(name, out var hooksEntries);
        if (hooksEntries != null) count = RunThroughCallbacks(name, hooksEntries, args);
        else
        {
            if (options.ShowWarnings) options.Logger?.LogWarning($"No hooks registered for - {name}");
        }

        return count;
    }
    public virtual int Call(string name, IList<string> tags, params object[] args)
    {
        int count = 0;
        name = name.ToLower();
        options.Logger?.LogInformation(name);
        hooks.TryGetValue(name, out var hooksEntries);
        if (hooksEntries != null) count = RunThroughCallbacks(name, hooksEntries.Where(x => tags.Any(y => x.tags.Contains(y))).ToList(), args);
        else
        {
            if (options.ShowWarnings) options.Logger?.LogWarning($"No hooks registered for - {name}");
        }

        return count;
    }
    public virtual async Task<int> CallAsync(string name, params object[] args)
    {
        int count = 0;
        name = name.ToLower();
        options.Logger?.LogInformation(name);
        hooks.TryGetValue(name, out var hooksEntries);
        if (hooksEntries != null)  count = await RunThroughCallbacksAsync(name, hooksEntries, args);
        else
        {
            if (options.ShowWarnings) options.Logger?.LogWarning($"No hooks registered for - {name}");
        }

        return count;
    }
    public virtual async Task<int> CallAsync(string name, IList<string> tags, params object[] args)
    {
        int count = 0;
        name = name.ToLower();
        options.Logger?.LogInformation(name);
        hooks.TryGetValue(name, out IList<HookDefinition> hooksEntries);
        if(hooksEntries != null) count = await RunThroughCallbacksAsync(name, hooksEntries.Where(x => tags.Any(y => x.tags.Contains(y))).ToList(), args);
        else
        {
            if (options.ShowWarnings) options.Logger?.LogWarning($"No hooks registered for - {name}");
        }

        return count;
    }
    internal async virtual Task<int> RunThroughCallbacksAsync(string name, IList<HookDefinition> entries, params object[] args)
    {
        int counter = 0;
        IList<Exception> exceptions = new List<Exception>();

        foreach (var hook in entries)
        {
            try
            {
                object returned = default;
                bool shouldCancel = false;

                if (hook.callbackNoArgs != null)
                {
                    returned = hook.callbackNoArgs();
                }
                else if (hook.callbackVoidNoArgs != null)
                {
                    hook.callbackVoidNoArgs();
                }
                else if (hook.callbackVoid != null)
                {
                    hook.callbackVoid(args);
                }
                else if(hook.callback != null)
                {
                    returned = hook.callback(args);
                }
                else if(hook.asyncCallbackNoArgs != null)
                {
                    await hook.asyncCallbackNoArgs().ConfigureAwait(false);
                }
                else if(hook.asyncCallback != null)
                {
                    returned = await hook.asyncCallback(args).ConfigureAwait(false);
                }

                counter++;

                if (returned is bool)
                {
                    shouldCancel = (bool)returned;
                }

                if (shouldCancel) break;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
                if (options.ShowWarnings) options.Logger?.LogError($"Error calling hook - {name} - {ex.Message}");
            }
        }

        AddToCallLog(name, args, exceptions.ToArray());

        return counter;
    }
    internal virtual int RunThroughCallbacks(string name, IList<HookDefinition> entries, params object[] args)
    {
        int counter = 0;
        IList<Exception> exceptions = new List<Exception>();

        foreach (var hook in entries)
        {
            try
            {
                object returned = default;
                bool shouldCancel = false;

                if (hook.callbackNoArgs != null)
                {
                    returned = hook.callbackNoArgs();
                }
                else if (hook.callbackVoidNoArgs != null)
                {
                    hook.callbackVoidNoArgs();
                }
                else if (hook.callbackVoid != null)
                {
                    hook.callbackVoid(args);
                }
                else if (hook.callback != null)
                {
                    returned = hook.callback(args);
                }
                else if (hook.asyncCallbackNoArgs != null)
                {
                    hook.asyncCallbackNoArgs().ConfigureAwait(false);
                }
                else if (hook.asyncCallback != null)
                {
                    returned = hook.asyncCallback(args).ConfigureAwait(false);
                }

                counter++;

                if (returned is bool)
                {
                    shouldCancel = (bool)returned;
                }

                if (shouldCancel) break;
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);

                if (options.ShowWarnings) options.Logger?.LogError($"Error calling hook - {name} - {ex.Message}");
            }
        }


        AddToCallLog(name, args, exceptions.ToArray());

        return counter;
    }
    protected virtual void AddToCallLog(string name, object[] args, Exception[] exceptions = null)
    {
        var log = new CallLog(name, args, exceptions);
        callLogs.Add(log);
    }
}
