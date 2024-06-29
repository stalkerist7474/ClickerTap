using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class LootPopUpPanel : MonoBehaviour
{
    [SerializeField] private Image iconItem;
    [SerializeField] private TextMeshProUGUI healthStatText;
    [SerializeField] private TextMeshProUGUI agilityStatText;
    [SerializeField] private TextMeshProUGUI damageStatText;
    [SerializeField] private TextMeshProUGUI armorStatText;

    [SerializeField] private Button buttonUse;
    [SerializeField] private Button buttonSell;
    [SerializeField] private TextMeshProUGUI buttonSellText;

    [SerializeField] private GameObject panelObject;


    public void InitDataItem(Item item, string healthText, string agilityText, string damageText, string armorText)
    {
        iconItem.sprite = item.image;
        healthStatText.text = healthText;
        agilityStatText.text = agilityText;
        damageStatText.text = damageText;
        armorStatText.text = armorText;

        buttonSellText.text = "Sell by +" + item.price.ToString();

        showPopUp();
    }

    public void PressButton(string type)
    {
        EventBus.RaiseEvent(new PressButtonPopUpLootEvent(type));
    }


    private void showPopUp()
    {
        panelObject.SetActive(true);
    }

    public void ClosePopUp()
    {
        panelObject.SetActive(false);
    }
}
