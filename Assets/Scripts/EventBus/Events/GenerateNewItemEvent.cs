
public struct GenerateNewItemEvent : IEvent
{
    public Item NewItem;


    public GenerateNewItemEvent(Item newItem)
    {
        NewItem = newItem;

    }
}
