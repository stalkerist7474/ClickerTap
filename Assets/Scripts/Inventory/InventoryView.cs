using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour, IEventSubscriber<ChangeItemInventoryEvent>
{
    [SerializeField] private List<InventorySlotView> slotsItemsViewInventory;

    private void Awake()
    {
        Subscribe();
    }

    private void Subscribe() => EventBus.RegisterTo(this as IEventSubscriber<ChangeItemInventoryEvent>);

    private void Unsubscribe() => EventBus.UnregisterFrom(this as IEventSubscriber<ChangeItemInventoryEvent>);

    public void OnEvent(ChangeItemInventoryEvent eventName) => UpdateInfoSlotItem(eventName.NewItem);



    private void UpdateInfoSlotItem(Item item)
    {
        for (int i = 0; i < slotsItemsViewInventory.Count; i++)
        {
            if (slotsItemsViewInventory[i].TypeSlot == item.typeItem)
            {
                slotsItemsViewInventory[i].IconItem.sprite = item.image;
                slotsItemsViewInventory[i].LevelItemText.text = item.level.ToString();
                break;
            }
        }
    }


    private void OnDestroy()
    {
        Unsubscribe();
    }
}
