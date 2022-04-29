using Microsoft.Extensions.Logging;

public class HookingOptions
{
    public ILogger Logger { get; set; }
    public bool ShowCalls { get; set; }
    public bool ShowWarnings { get; set; }
}
