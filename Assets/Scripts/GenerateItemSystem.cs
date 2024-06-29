using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateItemSystem : MonoBehaviour, IEventSubscriber<SignalToGenerateNewItemEvent>
{

    private Dictionary<TypeItem, Sprite> imageStore = new Dictionary<TypeItem, Sprite>();

    [SerializeField] private List<TypeItem> typeItem;
    [SerializeField] private List<Sprite> iconItem;

    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        InitItemIconStore();
    }
    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<SignalToGenerateNewItemEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<SignalToGenerateNewItemEvent>);
    }
    public void OnEvent(SignalToGenerateNewItemEvent eventName)
    {
        GenerateItem();
    }
    private void GenerateItem()
    {
        Item item = new Item();
        item.typeItem = (TypeItem)UnityEngine.Random.Range(0, Enum.GetNames(typeof(TypeItem)).Length);

        item.health = UnityEngine.Random.Range(1, 200);
        item.armor = UnityEngine.Random.Range(1, 100);
        item.damage = UnityEngine.Random.Range(1, 150);
        item.agility = UnityEngine.Random.Range(1, 120);

        item.level = UnityEngine.Random.Range(1, 80);
        item.price = UnityEngine.Random.Range(20, 500);
        item.image = imageStore[item.typeItem];

        EventBus.RaiseEvent(new GenerateNewItemEvent(item));

    }

    private void InitItemIconStore()
    {
        if (typeItem.Count == iconItem.Count)
        {
            for (int i = 0; i < typeItem.Count; i++)
            {
                imageStore.Add(typeItem[i], iconItem[i]);
            }

        }
        else
        {
            Debug.LogError("typeItem.Count != iconItem.Count, can't make dictionary ");
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
