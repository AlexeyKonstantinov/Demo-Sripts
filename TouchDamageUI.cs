using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class TouchDamageUI : MonoBehaviour
{
    [Header("Tap")]
    public GameObject tapDamageBuyPanel;
    public BuyButton tapDamageBuyButton;
    public TextMeshProUGUI tapDamageText;
    public TextMeshProUGUI criticalHitText;
    public BuyButton upgradeTapDamageButton;
    public BuyButton upgradeCriticalHitButton;

    [Space(15)]
    [Header("Hold")]
    public GameObject holdDamageBuyPanel;
    public BuyButton holdDamageBuyButton;
    public TextMeshProUGUI holdDamageText;
    public TextMeshProUGUI holdCriticalHitText;
    public BuyButton upgradeHoldDamageButton;
    public BuyButton upgradeHoldCriticalHitButton;

    [Space(15)]
    [Header("Swipe")]
    public GameObject swipeDamageBuyPanel;
    public BuyButton swipeDamageBuyButton;
    public TextMeshProUGUI swipeDamageText;
    public TextMeshProUGUI swipeSpeedMultiplierText;
    public BuyButton upgradeSwipeDamageButton;
    public BuyButton upgradeSpeedMultiplierButton;

    public void PrepareTapForBuying(double price, UnityAction buyAction)
    {
        tapDamageBuyPanel.SetActive(true);
        tapDamageBuyButton.SetUpButton(price, buyAction);        
    }

    public void PrepareHoldForBuying(double price, UnityAction buyAction)
    {
        holdDamageBuyPanel.SetActive(true);
        holdDamageBuyButton.SetUpButton(price, buyAction);
    }

    public void PrepareSwipeForBuying(double price, UnityAction buyAction)
    {
        swipeDamageBuyPanel.SetActive(true);
        swipeDamageBuyButton.SetUpButton(price, buyAction);
    }

    public void UpdateTap(double tapDamage, double criticalHitChance, double tapUpgradeprice, 
        double criticalHitUpgradeprice, UnityAction upgradeTapAction, UnityAction upgradeCriticalHitAction)
    {
        tapDamageBuyPanel.SetActive(false);

        tapDamageText.text = Score.ScoreToText(tapDamage);
        criticalHitText.text = Score.ScoreToText(criticalHitChance) + "%";

        upgradeTapDamageButton.SetUpButton(tapUpgradeprice, upgradeTapAction);
        upgradeCriticalHitButton.SetUpButton(criticalHitUpgradeprice, upgradeCriticalHitAction);
    }

    public void UpdateHold(double holdDamage, double holdCriticalHitChance, double holdUpgradeprice,
        double holdCriticalHitUpgradeprice, UnityAction upgradeHoldAction, UnityAction upgradeHoldCriticalHitAction)
    {
        holdDamageBuyPanel.SetActive(false);

        holdDamageText.text = Score.ScoreToText(holdDamage);
        criticalHitText.text = Score.ScoreToText(holdCriticalHitChance) + "%";

        upgradeHoldDamageButton.SetUpButton(holdUpgradeprice, upgradeHoldAction);
        upgradeHoldCriticalHitButton.SetUpButton(holdCriticalHitUpgradeprice, upgradeHoldCriticalHitAction);
    }

    public void UpdateSwipe(double swipeDamage, double swipeSpeedMultiplier, double spiweUpgradeprice,
        double swipeSpeedMultiplierUpgradePrice, UnityAction upgradeSwipeAction, UnityAction upgradeSwipeMultiplierAction)
    {
        swipeDamageBuyPanel.SetActive(false);

        swipeDamageText.text = Score.ScoreToText(swipeDamage);
        swipeSpeedMultiplierText.text = Score.ScoreToText(swipeSpeedMultiplier);

        upgradeSwipeDamageButton.SetUpButton(spiweUpgradeprice, upgradeSwipeAction);
        upgradeSpeedMultiplierButton.SetUpButton(swipeSpeedMultiplierUpgradePrice, upgradeSwipeMultiplierAction);
    }

}
