using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image iconItem;
    [SerializeField] private TextMeshProUGUI levelItemText;
    [SerializeField] private TypeItem typeSlot;

    public Image IconItem { get => iconItem; set => iconItem = value; }
    public TextMeshProUGUI LevelItemText { get => levelItemText; set => levelItemText = value; }
    public TypeItem TypeSlot { get => typeSlot; }
}
