using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Inventory : MonoBehaviour, IEventSubscriber<GenerateNewItemEvent>, IEventSubscriber<PressButtonPopUpLootEvent>
{
    [SerializeField] private LootPopUpPanel lootPopUpPanel;


    private Item[] itemsInventory;



    private Item currentItem;

    private string healthText;
    private string agilityText;
    private string damageText;
    private string armorText;

    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        
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

        for (int i = 0; i < itemsInventory.Length; i++)
        {
            if (itemsInventory[i].typeItem == type)
            {

                //Health
                if (itemsInventory[i].health != currentItem.health)
                {
                    healthText = $"{itemsInventory[i].health} {GetChangeSign(itemsInventory[i].health, currentItem.health)} {currentItem.health})";
                }
                if (itemsInventory[i].health == currentItem.health)
                {
                    healthText = $"{itemsInventory[i].health})";
                }


                //agility
                if (itemsInventory[i].agility != currentItem.agility)
                {
                    agilityText = $"{itemsInventory[i].agility} {GetChangeSign(itemsInventory[i].agility, currentItem.agility)} {currentItem.agility})";
                }
                if (itemsInventory[i].agility == currentItem.agility)
                {
                    agilityText = $"{itemsInventory[i].agility})";
                }

                //damage
                if (itemsInventory[i].damage != currentItem.damage)
                {
                    damageText = $"{itemsInventory[i].damage} {GetChangeSign(itemsInventory[i].damage, currentItem.damage)} {currentItem.damage})";
                }
                if (itemsInventory[i].damage == currentItem.damage)
                {
                    damageText = $"{itemsInventory[i].damage})";
                }

                //armor
                if (itemsInventory[i].armor != currentItem.armor)
                {
                    armorText = $"{itemsInventory[i].armor} {GetChangeSign(itemsInventory[i].armor, currentItem.armor)} {currentItem.armor})";
                }
                if (itemsInventory[i].armor == currentItem.armor)
                {
                    armorText = $"{itemsInventory[i].armor})";
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
        for (int i = 0; i < itemsInventory.Length; i++)
        {
            if (itemsInventory[i].typeItem == currentItem.typeItem)
            {
                itemsInventory[i] = currentItem; 
                break;
            }
        }
    }

    private void RefreshItemView()
    {

    }


    private void OnDestroy()
    {
        Unsubscribe();
    }

}
