
public struct PressButtonPopUpLootEvent : IEvent
{
    public string TypeOperation;


    public PressButtonPopUpLootEvent(string typeOperation)
    {
        TypeOperation = typeOperation;
    }
}
