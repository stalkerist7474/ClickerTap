using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBarUIView : MonoBehaviour, IEventSubscriber<ChangeUICoinEvent>
{
    [SerializeField] private TextMeshProUGUI currentCoinText;




    private void Awake()
    {
        Subscribe();
    }


    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<ChangeUICoinEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<ChangeUICoinEvent>);
    }
    public void OnEvent(ChangeUICoinEvent eventName)
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
