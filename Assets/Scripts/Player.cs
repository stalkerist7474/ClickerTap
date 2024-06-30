using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IEventSubscriber<ChangeItemInventoryEvent>
{
    [SerializeField] private Inventory inventory;

    private int health;
    private int agility;
    private int damage;
    private int armor;

    private List<InventorySlotModel> playerInventory;

    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        playerInventory = inventory.SlotsItemsInventory;    
    }
    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<ChangeItemInventoryEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<ChangeItemInventoryEvent>);
    }
    public void OnEvent(ChangeItemInventoryEvent eventName)
    {
        UpdatePlayerStats();
    }


    private void UpdatePlayerStats()
    {
        Dictionary<string, int> playerStats = CalculateTotalBonuses(playerInventory);

        health = playerStats["health"];
        agility = playerStats["agility"];
        damage = playerStats["damage"];
        armor = playerStats["armor"];

        EventBus.RaiseEvent(new UpdatePlayerStatsEvent(health, agility, damage, armor));
    }



    public static Dictionary<string, int> CalculateTotalBonuses(List<InventorySlotModel> items)
    {
        Dictionary<string, int> totalBonuses = new Dictionary<string, int>
        {
            {"health", 0},
            {"agility", 0},
            {"damage", 0},
            {"armor", 0}
        };

        foreach (InventorySlotModel item in items)
        {
            if (item.currentItem != null)
            {
                totalBonuses["health"] += item.currentItem.health;
                totalBonuses["agility"] += item.currentItem.agility;
                totalBonuses["damage"] += item.currentItem.damage;
                totalBonuses["armor"] += item.currentItem.armor;
            }
        }

        return totalBonuses;
    }



    private void OnDestroy()
    {
        Unsubscribe();
    }
}
