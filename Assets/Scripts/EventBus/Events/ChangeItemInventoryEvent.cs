
public struct ChangeItemInventoryEvent : IEvent
{
    public Item NewItem;


    public ChangeItemInventoryEvent(Item newItem)
    {
        NewItem = newItem;
    }
}
