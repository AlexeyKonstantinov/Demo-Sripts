using SaveSystem;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{
    public static Balance instance;    

    [Header("Текст баланса обычных пузырей")]
    [SerializeField] private TextMeshProUGUI simpleBubbleBalanceText = default;

    [Header("Текст баланса суперпузырей")]
    [SerializeField] private TextMeshProUGUI superBubbleBalanceText = default;

    [Header("Target Transform для эффекта simple Bubbles")]
    public Transform simpleBubblesEffectTarget;
    [Header("Target Transform для эффекта super Bubbles")]
    public Transform superBubblesEffectTarget;

    public double SimpleBubbleBalance { get; set; }
    public double SuperBubbleBalance { get; set; }

    public delegate void AddSimpleBubble(double amount);
    public static AddSimpleBubble addSimpleBubble;

    public delegate void BuySimpleBubble(double amount);
    public static BuySimpleBubble buySimpleBubble;

    public delegate void AddSuperBubble(double amount);
    public static AddSuperBubble addSuperBubble;

    public delegate void BuySuperBubble(double amount);
    public static BuySuperBubble buySuperBubble;

    public delegate void OnBalanceChange();
    public static OnBalanceChange onBalanceChange;

    private const string saveFileName = "balance";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadBalance();
    }

    private void Start()
    {
        addSimpleBubble += EarnSimpleBubble;
        buySimpleBubble += SpendSimpleBubble;

        addSuperBubble += EarnSuperBubble;
        buySuperBubble += SpendSuperBubble;

        onBalanceChange += SaveData;
        
        UpdateBalanceUI();
    }

    private void OnDisable()
    {
        addSimpleBubble -= EarnSimpleBubble;
        buySimpleBubble -= SpendSimpleBubble;

        addSuperBubble -= EarnSuperBubble;
        buySuperBubble -= SpendSuperBubble;

        onBalanceChange -= SaveData;
    }

    private void EarnSimpleBubble(double amount)
    {
        SimpleBubbleBalance += amount;
        onBalanceChange?.Invoke();
        UpdateBalanceUI();
    }

    private void EarnSuperBubble(double amount)
    {
        SuperBubbleBalance += amount;
        onBalanceChange?.Invoke();
        UpdateBalanceUI();
    }

    private void SpendSimpleBubble(double amount)
    {
        SimpleBubbleBalance -= amount;
        onBalanceChange?.Invoke();
        UpdateBalanceUI();
    }

    private void SpendSuperBubble(double amount)
    {
        SuperBubbleBalance -= amount;
        onBalanceChange?.Invoke();
        UpdateBalanceUI();
    }

    private void LoadBalance()
    {
        SimpleBubbleBalance = 0;
        SuperBubbleBalance = 0;
        LoadData();
    }
    private void UpdateBalanceUI()
    {
        simpleBubbleBalanceText.text = Score.ScoreToText(SimpleBubbleBalance);
        superBubbleBalanceText.text = Score.ScoreToText(SuperBubbleBalance);
    }

    private void SaveData()
    {
        BalanceSaveData balanceSaveData = new BalanceSaveData();

        balanceSaveData.simpleBubbleBalance = SimpleBubbleBalance;
        balanceSaveData.superBubbleBalance = SuperBubbleBalance;

        DataSaver.Save(balanceSaveData, saveFileName);
    }

    private void LoadData()
    {
        BalanceSaveData balanceSaveData = new BalanceSaveData();

        object obj = DataSaver.Load(saveFileName);
        if (obj != null)
            balanceSaveData = obj as BalanceSaveData;
        else 
            Debug.LogWarning("Creating new save data");

        SimpleBubbleBalance = balanceSaveData.simpleBubbleBalance;
        SuperBubbleBalance = balanceSaveData.superBubbleBalance;

        UpdateBalanceUI();
    }
}
