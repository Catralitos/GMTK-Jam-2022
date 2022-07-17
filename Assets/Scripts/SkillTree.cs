using System;
using System.Collections.Generic;
using Player;
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
    //public TextMeshProUGUI currentPointsText;
    //public TextMeshProUGUI currentCostText;
    public TextMeshProUGUI finalPointsText;
    public TextMeshProUGUI finalPointsNumber;

    public List<SkillTreeItem> items;
    [HideInInspector] public List<SkillTreeItem> checkout;

    public Button confirmButton;
    public Button exitButton;

    private void Start()
    {
        stillAvailablePoints = currentPoints;
        confirmButton.onClick.AddListener(Checkout);
        exitButton.onClick.AddListener(CloseScreen);
    }

    private void OnEnable()
    {
        checkout = new List<SkillTreeItem>();
        foreach (SkillTreeItem item in items)
        {
            item.EnableChild();
        }
        currentPoints = PlayerEntity.Instance.progression.currentWavePoints;
    }

    private void Update()
    {
        //currentPointsText.text = "Current Points - " + currentPoints;
        //currentCostText.text = "Current Cost - " + currentCost;
        finalPointsText.text = "Final Points - ";
        int finalPoints = currentPoints - currentCost;
        finalPointsNumber.text = finalPoints.ToString();
        finalPointsNumber.color = finalPoints == currentPoints ? Color.black : Color.red;
    }

    private void Checkout()
    {
        if (currentCost > currentPoints) return;
        foreach (SkillTreeItem item in checkout)
        {
            item.Unlock();
        }

        currentPoints -= currentCost;
        PlayerEntity.Instance.progression.currentWavePoints -= currentCost;
        currentCost = 0;
        stillAvailablePoints = currentPoints;
        CloseScreen();
    }

    private void CloseScreen()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}