using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemButton : MonoBehaviour
{
    [SerializeField] private int priceTapMana = 1;
    public void OnButtonPress()
    {
        EventBus.RaiseEvent(new SpendManaEvent(priceTapMana));
        Debug.Log($"Press mana Button = {priceTapMana}");
    }
}
