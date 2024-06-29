
public struct SellItemEvent : IEvent
{
    public int Price;


    public SellItemEvent(int price)
    {
        Price = price;

    }
}
