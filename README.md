# SharpHooking
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