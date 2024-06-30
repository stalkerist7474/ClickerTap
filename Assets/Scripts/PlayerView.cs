using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour, IEventSubscriber<UpdatePlayerStatsEvent>
{
    [SerializeField] private TextMeshProUGUI healthStatText;
    [SerializeField] private TextMeshProUGUI agilityStatText;
    [SerializeField] private TextMeshProUGUI damageStatText;
    [SerializeField] private TextMeshProUGUI armorStatText;


    private void Awake()
    {
        Subscribe();
    }

    public void OnEvent(UpdatePlayerStatsEvent eventName)
    {
        healthStatText.text = eventName.Health.ToString();
        agilityStatText.text = eventName.Agility.ToString();
        damageStatText.text = eventName.Damage.ToString();
        armorStatText.text = eventName.Armor.ToString();
    }

    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<UpdatePlayerStatsEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<UpdatePlayerStatsEvent>);
    }




    private void OnDestroy()
    {
        Unsubscribe();
    }
}
