using System;
using System.Collections;
using UnityEngine;
using SaveSystem;

public class TouchDamage : MonoBehaviour
{
    public TouchDamageUI ui;

    public double tapPrice = 1;
    public double holdPrice = 100000;
    public double swipePrice = 100000000;

    //-----TAP-----

    private int tapDamageLevel;
    private double tapDamageMultiplier;
    public double TapDamage => tapDamageMultiplier * tapDamageLevel * 0.05f;
    public double TapDamageUpgradePrice => Math.Pow(1.2, tapDamageLevel);

    private int tapCriticalHitLevel;
    private double tapCriticalHitMultiplier;
    public double TapCriticalHit => tapCriticalHitLevel * tapCriticalHitMultiplier * 0.01f;
    public double TapCriticalHitUpgradePrice => Math.Pow(2, tapCriticalHitLevel);
    
    //------HOLD------

    private int holdDamageLevel;
    private double holdDamageMultiplier;
    public double HoldDamage => holdDamageLevel * holdDamageMultiplier;
    public double HoldDamageUpgradePrice => holdDamageLevel * 10;

    private int holdCriticalHitLevel;
    private double holdCriticalHitMultiplier;
    public double HoldCriticalHit => holdCriticalHitLevel * holdCriticalHitMultiplier * 0.01f;
    public double HoldCriticalHitUpgradePrice => holdCriticalHitLevel * 10;

    //-----SWIPE-------

    private int swipeDamageLevel;
    private double swipeDamageMultiplier;
    public double SwipeDamage => swipeDamageLevel * swipeDamageMultiplier;
    public double SwipeDamageUpgradePrice => swipeDamageLevel * 10;

    private int swipeSpeedMultiplierLevel;
    private double swipeSpeedMultiplierMultiplier;
    public double SwipeSpeedMultiplier => swipeSpeedMultiplierLevel * swipeSpeedMultiplierMultiplier * 0.01f;
    public double SwipeSpeedMultiplierUpgradePrice => swipeSpeedMultiplierLevel * 10;

    private const string saveFileName = "touchdamage";

    private void Awake()
    {
        LoadData();

        TouchInput.ClickEvent += HandleClickDamage;
        TouchInput.DragEvent += HandleSwipeDamage;
        TouchInput.BeginHoldEvent += HandleBeginHoldDamage;
        TouchInput.EndHoldEvent += HandleEndHoldDamage;
    }

    private void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        if (tapDamageLevel == 0)
            ui.PrepareTapForBuying(tapPrice, BuyTapHandler);
        else
            UpdateTapPanelUI();

        if (holdDamageLevel == 0)
            ui.PrepareHoldForBuying(holdPrice, BuyHoldHandler);
        else
            UpdateHoldPanelUI();

        if (swipeDamageLevel == 0)
            ui.PrepareSwipeForBuying(swipePrice, BuySwipeHandler);
        else
            UpdateSwipePanelUI();
    }

    #region Tap Actions
    void BuyTapHandler()
    {
        Balance.buySuperBubble(tapPrice);
        tapDamageLevel += 1;
        SaveData();
        UpdateTapPanelUI();
    }

    
    void UpgradeTapDamageHandler()
    {
        Balance.buySimpleBubble(TapDamageUpgradePrice);
        tapDamageLevel += 1;
        SaveData();
        UpdateTapPanelUI();
    }

    void UpgradeTapCriticalHitHandler()
    {
        tapCriticalHitLevel += 1;
        SaveData();
        UpdateTapPanelUI();
    }

    private void UpdateTapPanelUI()
    {
        ui.UpdateTap(TapDamage, TapCriticalHit, TapDamageUpgradePrice, TapCriticalHitUpgradePrice,
                        UpgradeTapDamageHandler, UpgradeTapCriticalHitHandler);
    }

    #endregion

    #region Hold Actions
    void BuyHoldHandler()
    {
        Balance.buySuperBubble(holdPrice);
        holdDamageLevel += 1;
        SaveData();
        UpdateHoldPanelUI();
    }


    void UpgradeHoldDamageHandler()
    {
        Balance.buySimpleBubble(HoldDamageUpgradePrice);
        holdDamageLevel += 1;
        SaveData();
        UpdateHoldPanelUI();
    }

    void UpgradeHoldCriticalHitHandler()
    {
        holdCriticalHitLevel += 1;
        SaveData();
        UpdateHoldPanelUI();
    }

    private void UpdateHoldPanelUI()
    {
        ui.UpdateHold(HoldDamage, TapCriticalHit, TapDamageUpgradePrice, HoldCriticalHitUpgradePrice,
                        UpgradeHoldDamageHandler, UpgradeHoldCriticalHitHandler);
    }

    #endregion

    #region Swipe Actions
    void BuySwipeHandler()
    {
        Balance.buySuperBubble(swipePrice);
        swipeDamageLevel += 1;
        SaveData();
        UpdateSwipePanelUI();
    }


    void UpgradeSwipeDamageHandler()
    {
        Balance.buySimpleBubble(SwipeDamageUpgradePrice);
        swipeDamageLevel += 1;
        SaveData();
        UpdateSwipePanelUI();
    }

    void UpgradeSwipeSpeedMultiplierHandler()
    {
        swipeSpeedMultiplierLevel += 1;
        SaveData();
        UpdateSwipePanelUI();
    }

    private void UpdateSwipePanelUI()
    {
        ui.UpdateSwipe(SwipeDamage,SwipeSpeedMultiplier, SwipeDamageUpgradePrice, SwipeSpeedMultiplierUpgradePrice,
                        UpgradeSwipeDamageHandler, UpgradeSwipeSpeedMultiplierHandler);
    }

    #endregion


    #region Making Damage
    void HandleClickDamage(Vector2 position)
    {
        int random = UnityEngine.Random.Range(0, 100);
        if (random < TapCriticalHit)
        {
            BubblesEffectSpawner.instance.SpawnBubbles(3, TapDamage * 10, position,
                Balance.instance.simpleBubblesEffectTarget.position, 0.1f);

            PopUpTextPool.instance.SpawnText(Score.ScoreToText(TapDamage * 10), position);

            BubbleSpawn.instance.Damage(TapDamage * 10);
        }
        else
        {
            BubblesEffectSpawner.instance.SpawnBubbles(3, TapDamage, position,
                Balance.instance.simpleBubblesEffectTarget.position, 0.1f);

            PopUpTextPool.instance.SpawnText(Score.ScoreToText(TapDamage), position);

            BubbleSpawn.instance.Damage(TapDamage);
        }
    }

    void HandleSwipeDamage(float speed)
    {
        BubbleSpawn.instance.Damage(speed * SwipeDamage);
    }

    void HandleBeginHoldDamage()
    {
        StartCoroutine("HoldDamageCoroutine");
    }

    void HandleEndHoldDamage()
    {
        StopCoroutine("HoldDamageCoroutine");
    }

    IEnumerator HoldDamageCoroutine()
    {
        while(true)
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random < HoldCriticalHit)
                BubbleSpawn.instance.Damage(HoldDamage * 10);
            else
                BubbleSpawn.instance.Damage(HoldDamage);
            yield return new WaitForSeconds(0.5f);
        }
    }
    #endregion

    private void LoadData()
    {
        TouchDamageSaveData saveData = new TouchDamageSaveData();

        object obj = DataSaver.Load(saveFileName);
        if (obj != null)
            saveData = obj as TouchDamageSaveData;
        else 
            Debug.LogWarning("Creating new save data");

        this.tapDamageLevel = saveData.tapDamageLevel;
        this.tapDamageMultiplier = saveData.tapDamageMultiplier;
        this.tapCriticalHitLevel = saveData.criticalHitLevel;
        this.tapCriticalHitMultiplier = saveData.criticalHitMultiplier;

        this.holdDamageLevel = saveData.holdDamageLevel;
        this.holdDamageMultiplier = saveData.holdDamageMultiplier;
        this.holdCriticalHitLevel = saveData.holdCriticalHitLevel;
        this.holdCriticalHitMultiplier = saveData.holdCriticalHitMultiplier;

        this.swipeDamageLevel = saveData.swipeDamageLevel;
        this.swipeDamageMultiplier = saveData.swipeDamageMultiplier;
        this.swipeSpeedMultiplierLevel = saveData.swipeSpeedMultiplierLevel;
        this.swipeSpeedMultiplierMultiplier = saveData.swipeSpeedMultiplierMultiplier;
    }

    private void SaveData()
    {
        TouchDamageSaveData saveData = new TouchDamageSaveData();

        saveData.tapDamageLevel = this.tapDamageLevel;
        saveData.tapDamageMultiplier = this.tapDamageMultiplier;
        saveData.criticalHitLevel = this.tapCriticalHitLevel;
        saveData.criticalHitMultiplier = this.tapCriticalHitMultiplier;

        saveData.holdDamageLevel = this.holdDamageLevel;
        saveData.holdDamageMultiplier = this.holdDamageMultiplier;
        saveData.holdCriticalHitLevel = this.holdCriticalHitLevel;
        saveData.holdCriticalHitMultiplier = this.holdCriticalHitMultiplier;

        saveData.swipeDamageLevel = this.swipeDamageLevel;
        saveData.swipeDamageMultiplier = this.swipeDamageMultiplier;
        saveData.swipeSpeedMultiplierLevel = this.swipeSpeedMultiplierLevel;
        saveData.swipeSpeedMultiplierMultiplier = this.swipeSpeedMultiplierMultiplier;

        DataSaver.Save(saveData, saveFileName);
    }

}
