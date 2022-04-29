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
IList<string> tags = new List<string> { "v1" };
hooks.Register("test", (args) => {}, 0, tags);
```

### Calling
```csharp
IList<string> tags = new List<string> { "v1" };
hooks.Call("test", tags);
```
### Canceling hooks
You can cancel a hook event with returning true.

```csharp
hooks.Register("test", async (args) => {
	return true;
});
```