using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Inventory : MonoBehaviour, IEventSubscriber<GenerateNewItemEvent>, IEventSubscriber<PressButtonPopUpLootEvent>
{
    [SerializeField] private LootPopUpPanel lootPopUpPanel;


    [SerializeField] private List<InventorySlotModel> slotsItemsInventory;



    private Item currentItem;

    private string healthText;
    private string agilityText;
    private string damageText;
    private string armorText;

    [SerializeField] private Sprite defaultIcon;


    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        //GenerateStartInventory();
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

        for (int i = 0; i < slotsItemsInventory.Count; i++)
        {
            if (slotsItemsInventory[i].typeSlot == type)
            {
                //if empty slot
                if(slotsItemsInventory[i].currentItem == null)
                {
                    healthText = $" 0 + {currentItem.health}";
                    agilityText = $" 0 + {currentItem.agility}";
                    damageText = $" 0 + {currentItem.damage}";
                    armorText = $" 0 + {currentItem.armor}";
                    return;
                }

                //Health
                if (slotsItemsInventory[i].currentItem.health != currentItem.health)
                {
                    healthText = $"{slotsItemsInventory[i].currentItem.health} {GetChangeSign(slotsItemsInventory[i].currentItem.health, currentItem.health)} {currentItem.health}";
                }
                if (slotsItemsInventory[i].currentItem.health == currentItem.health)
                {
                    healthText = $"{slotsItemsInventory[i].currentItem.health})";
                }


                //agility
                if (slotsItemsInventory[i].currentItem.agility != currentItem.agility)
                {
                    agilityText = $"{slotsItemsInventory[i].currentItem.agility} {GetChangeSign(slotsItemsInventory[i].currentItem.agility, currentItem.agility)} {currentItem.agility}";
                }
                if (slotsItemsInventory[i].currentItem.agility == currentItem.agility)
                {
                    agilityText = $"{slotsItemsInventory[i].currentItem.agility})";
                }

                //damage
                if (slotsItemsInventory[i].currentItem.damage != currentItem.damage)
                {
                    damageText = $"{slotsItemsInventory[i].currentItem.damage} {GetChangeSign(slotsItemsInventory[i].currentItem.damage, currentItem.damage)} {currentItem.damage}";
                }
                if (slotsItemsInventory[i].currentItem.damage == currentItem.damage)
                {
                    damageText = $"{slotsItemsInventory[i].currentItem.damage})";
                }

                //armor
                if (slotsItemsInventory[i].currentItem.armor != currentItem.armor)
                {
                    armorText = $"{slotsItemsInventory[i].currentItem.armor} {GetChangeSign(slotsItemsInventory[i].currentItem.armor, currentItem.armor)} {currentItem.armor}";
                }
                if (slotsItemsInventory[i].currentItem.armor == currentItem.armor)
                {
                    armorText = $"{slotsItemsInventory[i].currentItem.armor})";
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
        for (int i = 0; i < slotsItemsInventory.Count; i++)
        {
            if (slotsItemsInventory[i].typeSlot == currentItem.typeItem)
            {
                slotsItemsInventory[i].currentItem = currentItem; 
                break;
            }
        }
    }

    private void RefreshItemView()
    {

    }

    //private void GenerateStartInventory()
    //{

    //    foreach (TypeItem type in System.Enum.GetValues(typeof(TypeItem)))
    //    {
    //        Item newItem = new Item();
    //        newItem.typeItem = type;

    //        switch (type)
    //        {
    //            case TypeItem.Weapon:
    //                SetRandomStat(newItem);
    //                break;
    //            case TypeItem.Armor:
    //                SetRandomStat(newItem);
    //                break;
    //            case TypeItem.Boots:
    //                SetRandomStat(newItem);
    //                break;
    //            case TypeItem.Hemlet:
    //                SetRandomStat(newItem);
    //                break;
    //            case TypeItem.Extra:
    //                SetRandomStat(newItem);
    //                break;
    //            case TypeItem.Shield:
    //                SetRandomStat(newItem);
    //                break;
    //        }
    //        slotsItemsInventory.Add(new InventorySlotModel(newItem.typeItem, newItem));
    //    }

    //}

    //private void SetRandomStat(Item item)
    //{
    //    item.health = UnityEngine.Random.Range(1, 3);
    //    item.armor = UnityEngine.Random.Range(1, 3);
    //    item.damage = UnityEngine.Random.Range(1, 10);
    //    item.agility = UnityEngine.Random.Range(1, 2);
    //    item.level = UnityEngine.Random.Range(1, 2);
    //    item.price = UnityEngine.Random.Range(20, 100);
    //    item.image = defaultIcon;
    //}

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
