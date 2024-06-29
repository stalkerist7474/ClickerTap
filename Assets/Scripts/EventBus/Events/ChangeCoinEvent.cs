
public struct ChangeCoinEvent : IEvent
{
    public int CurrentValue;
    public bool IsPlusOperation;

    public ChangeCoinEvent(int currentValue, bool isPlusOperation)
    {
        CurrentValue = currentValue;
        IsPlusOperation = isPlusOperation;
    }
}
