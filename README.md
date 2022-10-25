# SharpHooking

[![NuGet](https://img.shields.io/nuget/v/SharpHooking.svg?label=SharpHooking)](https://www.nuget.org/packages/SharpHooking)

A little library to allow you to call/register hooks.

## Creating
```csharp
var hooks = SharpHooking.Create();
```
## Sync
```csharp
hooks.Register("test", (args) => {});

hooks.Call("test");
hooks.Call("test", "hello!");
```

## Async
```csharp
hooks.Register("test", async (args) => {});
await hooks.CallAsync("test");
await hooks.CallAsync("test", "hello");
```

## Weight
You can assign a weight to your hook. 0 being the lowest meaning it will be the first called.

### Registering
```csharp
hooks.Register("test", (args) => {}, 0);
```

## Tags
You can use tags to group hooks for versioning and so forth.

### Registering
```csharp
hooks.Register("test", (args) => {}, "v1");
```

### Calling
```csharp
hooks.Call("test", new List<string> { "v1" });
```
## Cancelling hooks
You can cancel a hook event with returning true.

```csharp
hooks.Register("test", (args) => {
	return true;
});
```

## Call Logs

You can grab the call logs. These will contain the hook name, arguments, when and any exceptions brought up during execution.
```csharp
hooks.GetLogs();
```