
public struct SpendManaEvent : IEvent
{
    public int ChangeValue;

    public SpendManaEvent(int changeValue)
    {
        ChangeValue = changeValue;

    }
}
