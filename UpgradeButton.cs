using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    public enum UpgradeButtonType
    {
        simpleBubble,
        superBubble
    }

    public BuyButtonType type;

    public TextMeshProUGUI priceText;
    public TextMeshProUGUI valueText;

    private Parameter _parameter;

    private Action _onBuyCallback;

    private Button button;


    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Buy);
    }

    public void SetUpButton(Parameter parameter)
    {
        _parameter = parameter;

        UpdateUI();

    }   

    public void SetUpButton(Parameter parameter, Action onBuyCallback)
    {
        _parameter = parameter;

        _onBuyCallback = onBuyCallback;

        UpdateUI();
    }

    private void UpdateUI()
    {

        if (_parameter.ValueInt() != -1)
            valueText.text = _parameter.ValueInt().ToString();
        else
            valueText.text = Score.ScoreToText(_parameter.ValueDouble());

        if (!_parameter.IsUpgradable)
        {
            SetMaximumReached();
            return;
        }

        priceText.text = Score.ScoreToText(_parameter.UpgradeCost());
        

        if (type == BuyButtonType.simpleBubble)
        {
            if (Balance.instance.SimpleBubbleBalance >= _parameter.UpgradeCost())
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        else if (type == BuyButtonType.superBubble)
        {
            if (Balance.instance.SuperBubbleBalance >= _parameter.UpgradeCost())
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }
    
    public void Disconnect()
    {
        _onBuyCallback = null;
        _parameter = null;
        Balance.onBalanceChange -= UpdateUI;
    }    

    void Buy()
    {
        if (type == BuyButtonType.simpleBubble)
            Balance.buySimpleBubble(_parameter.UpgradeCost());
        else
            Balance.buySuperBubble(_parameter.UpgradeCost());

        _parameter.Upgrade();
        _onBuyCallback?.Invoke();
        UpdateUI();
    }

    public void SetMaximumReached()
    {
        priceText.text = "MAX";
        button.interactable = false;
    }    

}
