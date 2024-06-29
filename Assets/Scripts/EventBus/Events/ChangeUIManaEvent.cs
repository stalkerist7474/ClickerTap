
public struct ChangeUIManaEvent : IEvent
{
    public int ChangeValue;
    public int MaxValue;
    public int CurrentValue;

    public ChangeUIManaEvent(int changeValue, int maxValue, int currentValue)
    {
        ChangeValue = changeValue;
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
