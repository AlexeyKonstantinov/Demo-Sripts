using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using SaveSystem;

public class IdleBubble : MonoBehaviour, IPointerClickHandler
{
    public static IdleBubble instance;

    private const string saveFileName = "idlebubble";

    [SerializeField]
    private TextMeshProUGUI balanceText = default;
    [SerializeField]
    private Animator animator = default;
    

    [Space]
    [Header("Текстовые поля характеристик")]
    public TextMeshProUGUI frequencyText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI tubesAmountText;

    [Space]
    [Header("Кнопки улучшения характеристик")]
    public BuyButton upgradeTubesAmountButton;
    public BuyButton upgradeFrequencyButton;
    public BuyButton upgradeOneBubbleIncomeButton;

    public double UpgradeFrequencyCost { get { return frequencyLevel * 1000; } }
    public double UpgradeTubesAmountCost { get { return tubesAmount * 10000; } }
    public double UpgradeIncomeCost { get { return incomeLevel * 10; } }
    public double Balance_ { get; private set; }


    public float Frequency { get { return 2 / (float)frequencyLevel; } }  //Заменить 2 на что то другое
    public int TubesAmount { get { return tubesAmount; } }
    public double BareIncome { get { return incomeLevel * 2; } }
    public double TotalIncome { get { return BareIncome * TubesAmount; } }
    public double IncomePerSecond { get { return TotalIncome / Frequency; } }

    private int tubesAmount = 1;
    private int frequencyLevel = 1;
    private int incomeLevel = 1000;

    private void Awake()
    {
        if (instance != this)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        LoadData();

        SetUpUpgradeButtons();

        StartCoroutine("IdleProduction");

        UpdateUI();

        yield return null;
    }

    private void SetUpUpgradeButtons()
    {
        upgradeFrequencyButton.SetUpButton(UpgradeFrequencyCost, UpgradeFrequencyButton);

        upgradeOneBubbleIncomeButton.SetUpButton(UpgradeIncomeCost, UpgradeIncomeButton);

        if (tubesAmount < 4)
            upgradeTubesAmountButton.SetUpButton(UpgradeTubesAmountCost, UpgradeTubesAmountButton);
        else
            upgradeTubesAmountButton.SetMaximumReached();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    private void Click()
    {
        CollectBubbles();
        animator.SetTrigger("Click");
    }

    private void CollectBubbles()
    {
        if (Balance_ <= 0)
            return;
        BubblesEffectSpawner.instance.SpawnBubbles(10, Balance_, transform.position, Balance.instance.simpleBubblesEffectTarget.position, 0.4f);
        Balance_ = 0;
        UpdateUI();
    }

    IEnumerator IdleProduction()
    {
        while(true)
        {
            //TODO: Заспаунить пузыри
            Balance_ += TotalIncome;
            SaveData();
            UpdateUI();
            yield return new WaitForSeconds(Frequency);
        }        
    }

    private void UpdateUI()
    {
        balanceText.text = Score.ScoreToText(Balance_);
        frequencyText.text = Frequency.ToString("F") + "s";
        incomeText.text = Score.ScoreToText(BareIncome);
        tubesAmountText.text = TubesAmount.ToString();
    }

    void UpgradeFrequencyButton()
    {
        Balance.buySimpleBubble(UpgradeFrequencyCost);
        frequencyLevel += 1;
        upgradeFrequencyButton.SetUpButton(UpgradeFrequencyCost, UpgradeFrequencyButton);
        UpdateUI();
    }

    void UpgradeTubesAmountButton()
    {
        if (tubesAmount < 4)
        {
            Balance.buySimpleBubble(UpgradeTubesAmountCost);

            tubesAmount += 1;

            if (tubesAmount == 4)
            {
                upgradeTubesAmountButton.SetMaximumReached();
            }
            else
            {
                upgradeTubesAmountButton.SetUpButton(UpgradeTubesAmountCost, UpgradeTubesAmountButton);
            }
        }  
        UpdateUI();
    }

    void UpgradeIncomeButton()
    {
        Balance.buySimpleBubble(UpgradeIncomeCost);
        incomeLevel += 1;
        upgradeOneBubbleIncomeButton.SetUpButton(UpgradeIncomeCost, UpgradeIncomeButton);
        UpdateUI();
    }

    private void SaveData()
    {
        IdleBubbleSaveData saveData = new IdleBubbleSaveData();
        saveData.balance = Balance_;
        saveData.tubesAmount = tubesAmount;
        saveData.frequencyLevel = frequencyLevel;
        saveData.incomeLevel = incomeLevel;

        DataSaver.Save(saveData, saveFileName);
    }

    private void LoadData()
    {
        IdleBubbleSaveData saveData = new IdleBubbleSaveData();
        object obj = DataSaver.Load(saveFileName);

        if (obj != null)
            saveData = obj as IdleBubbleSaveData;
        else 
            Debug.LogWarning("obj is null");

        Balance_ = saveData.balance;
        tubesAmount = saveData.tubesAmount;
        frequencyLevel = saveData.frequencyLevel;
        incomeLevel = saveData.incomeLevel;
    }
}
