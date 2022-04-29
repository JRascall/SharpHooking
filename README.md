# SharpHooking
A little library to allow you to call/register hooks.

## Example Use
```csharp
	var hooks = SharpHooking.Create();
	hooks.Register("test", (args) => {
		
	});
	hooks.Call("test");
	hooks.Call("test", "hello!");
```