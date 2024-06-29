
public struct ChangeUICoinEvent : IEvent
{
    public int CurrentValue;
    public bool IsPlusOperation;

    public ChangeUICoinEvent(int currentValue, bool isPlusOperation)
    {
        CurrentValue = currentValue;
        IsPlusOperation = isPlusOperation;
    }
}
