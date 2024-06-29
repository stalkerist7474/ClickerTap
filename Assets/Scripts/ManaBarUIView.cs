using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarUIView : MonoBehaviour, IEventSubscriber<ChangeUIManaEvent>
{

    [SerializeField] private TextMeshProUGUI currentManaText;
    [SerializeField] private Slider manaSlider;



    private void Awake()
    {
        Subscribe();
    }

    private void Start()
    {
        manaSlider.value = 0f;
    }

    private void Subscribe()
    {
        EventBus.RegisterTo(this as IEventSubscriber<ChangeUIManaEvent>);
    }
    private void Unsubscribe()
    {
        EventBus.UnregisterFrom(this as IEventSubscriber<ChangeUIManaEvent>);
    }
    public void OnEvent(ChangeUIManaEvent eventName)
    {
        UpdateManabar(eventName.CurrentValue, eventName.MaxValue);
    }

    public void UpdateManabar(int currentMana, int maxMana)
    {
        currentManaText.text = currentMana.ToString() + "/" + maxMana.ToString();
        manaSlider.value = Mathf.Clamp01((float)currentMana / (float)maxMana);
    }



    private void OnDestroy()
    {
        Unsubscribe();
    }







}
