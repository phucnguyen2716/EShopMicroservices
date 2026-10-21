namespace Basket.API.Data;

public class CacheLockService
{
    public SemaphoreSlim Lock { get; } = new(1, 1);
}