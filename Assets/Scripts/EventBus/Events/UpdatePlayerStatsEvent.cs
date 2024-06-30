
public struct UpdatePlayerStatsEvent : IEvent
{
    public int Health;
    public int Agility;
    public int Damage;
    public int Armor;


    public UpdatePlayerStatsEvent(int health, int agility, int damage, int armor)
    {
        Health = health;
        Agility = agility;
        Damage = damage;
        Armor = armor;
    }
}
