using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    #region SingleTon

    public static SkillTree Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [HideInInspector] public int currentPoints;
    [HideInInspector] public int stillAvailablePoints;
    [HideInInspector] public int currentCost;
    public TextMeshProUGUI textTooltip;
    [HideInInspector] public List<SkillTreeItem> checkout;

    public Button confirmButton;

    private void Start()
    {
        stillAvailablePoints = currentPoints;
        confirmButton.onClick.AddListener(Checkout);
    }

    private void Checkout()
    {
        if (currentCost > currentPoints) return;
        foreach (SkillTreeItem item in checkout)
        {
            item.Unlock();
        }

        currentPoints -= currentCost;
        currentCost = 0;
        stillAvailablePoints = currentPoints;
    }
}
