using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour, IEventSubscriber<GenerateNewItemEvent>, IEventSubscriber<PressButtonPopUpLootEvent>
{
    [SerializeField] private LootPopUpPanel lootPopUpPanel;
    [SerializeField] private List<InventorySlotModel> slotsItemsInventory;
    [SerializeField] private Sprite defaultIcon;

    private Item currentItem;
    private string healthText;
    private string agilityText;
    private string damageText;
    private string armorText;

    public List<InventorySlotModel> SlotsItemsInventory { get => slotsItemsInventory; }

    private void Awake()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<GenerateNewItemEvent>);
        EventBus.RegisterTo(this as IEventSubscriber<PressButtonPopUpLootEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<GenerateNewItemEvent>);
        EventBus.UnregisterFrom(this as IEventSubscriber<PressButtonPopUpLootEvent>);
    }
    public void OnEvent(GenerateNewItemEvent eventName)
    {
        currentItem = eventName.NewItem;
        ShowLootPanel();
    }
    public void OnEvent(PressButtonPopUpLootEvent eventName)
    {
        if (eventName.TypeOperation == "Sell")
        {
            EventBus.RaiseEvent(new SellItemEvent(currentItem.price));
            currentItem = null;
            return;
        }
        if (eventName.TypeOperation == "Use")
        {
            PutOnItem();
        }
    }

    private void ShowLootPanel()
    {
        CompareStatsToText(currentItem.typeItem);
        lootPopUpPanel.InitDataItem(currentItem,healthText,agilityText,damageText,armorText);
    }

    private void CompareStatsToText(TypeItem type)
    {

        for (int i = 0; i < SlotsItemsInventory.Count; i++)
        {
            if (SlotsItemsInventory[i].typeSlot == type)
            {
                //if empty slot
                if(SlotsItemsInventory[i].currentItem == null)
                {
                    healthText = $" 0 + {currentItem.health}";
                    agilityText = $" 0 + {currentItem.agility}";
                    damageText = $" 0 + {currentItem.damage}";
                    armorText = $" 0 + {currentItem.armor}";
                    return;
                }

                //Health
                if (SlotsItemsInventory[i].currentItem.health != currentItem.health)
                {
                    healthText = $"{SlotsItemsInventory[i].currentItem.health} {GetChangeSign(SlotsItemsInventory[i].currentItem.health, currentItem.health)} {Math.Abs(SlotsItemsInventory[i].currentItem.health - currentItem.health)}";
                }
                if (SlotsItemsInventory[i].currentItem.health == currentItem.health)
                {
                    healthText = $"{SlotsItemsInventory[i].currentItem.health})";
                }


                //agility
                if (SlotsItemsInventory[i].currentItem.agility != currentItem.agility)
                {
                    agilityText = $"{SlotsItemsInventory[i].currentItem.agility} {GetChangeSign(SlotsItemsInventory[i].currentItem.agility, currentItem.agility)} {Math.Abs(SlotsItemsInventory[i].currentItem.agility - currentItem.agility)}";
                }
                if (SlotsItemsInventory[i].currentItem.agility == currentItem.agility)
                {
                    agilityText = $"{SlotsItemsInventory[i].currentItem.agility})";
                }

                //damage
                if (SlotsItemsInventory[i].currentItem.damage != currentItem.damage)
                {
                    damageText = $"{SlotsItemsInventory[i].currentItem.damage} {GetChangeSign(SlotsItemsInventory[i].currentItem.damage, currentItem.damage)} {Math.Abs(SlotsItemsInventory[i].currentItem.damage - currentItem.damage)}";
                }
                if (SlotsItemsInventory[i].currentItem.damage == currentItem.damage)
                {
                    damageText = $"{SlotsItemsInventory[i].currentItem.damage})";
                }

                //armor
                if (SlotsItemsInventory[i].currentItem.armor != currentItem.armor)
                {
                    armorText = $"{SlotsItemsInventory[i].currentItem.armor} {GetChangeSign(SlotsItemsInventory[i].currentItem.armor, currentItem.armor)} {Math.Abs(SlotsItemsInventory[i].currentItem.armor - currentItem.armor)}";
                }
                if (SlotsItemsInventory[i].currentItem.armor == currentItem.armor)
                {
                    armorText = $"{SlotsItemsInventory[i].currentItem.armor})";
                }

            }
        }
    }
    string GetChangeSign(int oldValue, int newValue)
    {
        if (newValue > oldValue) return "+";
        else if (newValue < oldValue) return "-";
        else return "";
    }
    private void PutOnItem()
    {
        for (int i = 0; i < SlotsItemsInventory.Count; i++)
        {
            if (SlotsItemsInventory[i].typeSlot == currentItem.typeItem)
            {
                SlotsItemsInventory[i].currentItem = currentItem;
                EventBus.RaiseEvent(new ChangeItemInventoryEvent(SlotsItemsInventory[i].currentItem));
                break;
            }
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
