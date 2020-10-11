using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStateButton : MonoBehaviour
{
    public delegate void OnChangeStateButton(bool isInUpgradeState);
    public static event OnChangeStateButton onChangeStateButton;

    public bool isInUpgradeState = false;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        isInUpgradeState = !isInUpgradeState;
        onChangeStateButton?.Invoke(isInUpgradeState);
    }
}
