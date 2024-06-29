using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBarUIView : MonoBehaviour, IEventSubscriber<ChangeCoinEvent>
{
    [SerializeField] private TextMeshProUGUI currentCoinText;




    private void Awake()
    {
        Subscribe();
    }


    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<ChangeCoinEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<ChangeCoinEvent>);
    }
    public void OnEvent(ChangeCoinEvent eventName)
    {
        UpdateCoinbar(eventName.CurrentValue);
    }

    public void UpdateCoinbar(int currentCoin)
    {
        currentCoinText.text = currentCoin.ToString();
    }



    private void OnDestroy()
    {
        Unsubscribe();
    }
}
