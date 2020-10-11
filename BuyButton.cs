using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public enum BuyButtonType
{ 
    simpleBubble,
    superBubble
}

[RequireComponent(typeof(Button))]
public class BuyButton : MonoBehaviour
{
    public BuyButtonType type;

    public UnityAction buyAction;

    public TextMeshProUGUI priceText;

    public Button button;

    double _price;

    bool maximumReached = false;

    bool initialized = false;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Buy);
        
        Balance.onBalanceChange += OnBalanceChange;
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
        Balance.onBalanceChange -= OnBalanceChange;
    }

    private void OnEnable()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Buy);
        Balance.onBalanceChange += OnBalanceChange;

        if (initialized)
            OnBalanceChange();
        else
            Invoke("OnBalanceChange", 0.5f);
    }


    void Buy() 
    {
        buyAction?.Invoke();
    }

    public void SetUpButton(double price, UnityAction buyAction)
    {
        this._price = price;
        this.buyAction = buyAction;
        
        if(priceText != null)
            priceText.text = Score.ScoreToText(price);

        OnBalanceChange();
    }

    public void SetUpButton(double price, UnityAction buyAction, bool maximumReached)
    {
        if (!maximumReached)
        {
            this._price = price;
            this.buyAction = buyAction;


            if(priceText != null)
                priceText.text = Score.ScoreToText(price);
        }
        else
            SetMaximumReached();

        OnBalanceChange();
    }

    public void SetMaximumReached()
    {
        if (priceText != null)
            priceText.text = "MAX";
        button.interactable = false;
        buyAction = null;
        maximumReached = true;
    }

    private void OnBalanceChange()
    {
        if (Balance.instance == null)
            Debug.Log("Balance.instance == null");

        if (maximumReached)
            return;
        if (type == BuyButtonType.simpleBubble)
        {
            if (Balance.instance.SimpleBubbleBalance >= _price)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        else if(type == BuyButtonType.superBubble)
        {
            if (Balance.instance.SuperBubbleBalance >= _price)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

}
