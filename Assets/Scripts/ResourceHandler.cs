using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour, 
    IEventSubscriber<SpendManaEvent>,
    IEventSubscriber<SellItemEvent>
{
    [SerializeField] private int currentValueMana = 20;
    [SerializeField] private int currentMaxValueMana = 100;
    [SerializeField] private int currentValueCoins = 200;

    [SerializeField] private float delayGetMana= 1f;
    [SerializeField] private int valueManaFlow = 1;

    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        InitUIValue();

        StartCoroutine(AddManaCoroutine());
    }

    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<SpendManaEvent>);
        EventBus.RegisterTo(this as IEventSubscriber<SellItemEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<SpendManaEvent>);
        EventBus.UnregisterFrom(this as IEventSubscriber<SellItemEvent>);
    }
    private void InitUIValue()
    {
        EventBus.RaiseEvent(new ChangeUIManaEvent(valueManaFlow, currentMaxValueMana, currentValueMana));
        EventBus.RaiseEvent(new ChangeUICoinEvent(currentValueCoins, true));
    }
    //Mana
    private IEnumerator AddManaCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayGetMana);
            if (currentValueMana <= currentMaxValueMana)
            {
                currentValueMana += valueManaFlow;
                EventBus.RaiseEvent(new ChangeUIManaEvent(valueManaFlow , currentMaxValueMana, currentValueMana));
            }

        }
    }

    private void SpendMana(int valueSpend)
    {
        if (valueSpend <= currentValueMana)
        {
            currentValueMana -= valueSpend;
            EventBus.RaiseEvent(new ChangeUIManaEvent(valueManaFlow, currentMaxValueMana, currentValueMana));

            EventBus.RaiseEvent(new SignalToGenerateNewItemEvent());
        }
    }

    public void OnEvent(SpendManaEvent eventName)
    {
        SpendMana(eventName.ChangeValue);
    }



    //Coin
    private void AddCoin(int valueAdd)
    {
        currentValueCoins += valueAdd;
        EventBus.RaiseEvent(new ChangeUICoinEvent(currentValueCoins, true));
    }

    private void SpendCoin(int valueSpend)
    {
        if (valueSpend <= currentValueCoins)
        {
            currentValueCoins -= valueSpend;
            EventBus.RaiseEvent(new ChangeUICoinEvent(currentValueCoins, true));       
        }
    }

    public void OnEvent(SellItemEvent eventName)
    {
        if (eventName.Price > 0)
        {
            AddCoin(eventName.Price);
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

}
