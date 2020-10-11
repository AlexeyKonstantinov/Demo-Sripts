using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public enum ShopScreenType {Closed, Tap, Autoclickers, Bonus }

    [SerializeField] private Animator animator = default;

    [Header("Screens")]
    [SerializeField] public GameObject tapScreen = default;
    [SerializeField] public GameObject autoclikersScreen = default;
    [SerializeField] public GameObject bonusScreen = default;

    private ShopScreenType currentScreenType;

    public void ClickShop(string screenName)
    {
        switch (screenName)
        {
            case "Tap":
                OpenShop(ShopScreenType.Tap);
                break;
            case "Autoclickers":
                OpenShop(ShopScreenType.Autoclickers);
                break;
            case "Bonus":
                OpenShop(ShopScreenType.Bonus);
                break;
        }
    }

    public void OpenShop(ShopScreenType shopScreenType)
    {
        if(currentScreenType == shopScreenType)
        {
            CloseShop();
            return;
        }

        CloseAllScreens();

        switch (shopScreenType)
        {
            case ShopScreenType.Tap:
                tapScreen.SetActive(true);
                PlayOpenShopAnimation();
                currentScreenType = ShopScreenType.Tap;
                break;

            case ShopScreenType.Autoclickers:
                autoclikersScreen.SetActive(true);
                PlayOpenShopAnimation();
                currentScreenType = ShopScreenType.Autoclickers;
                break;

            case ShopScreenType.Bonus:
                bonusScreen.SetActive(true);
                PlayOpenShopAnimation();
                currentScreenType = ShopScreenType.Bonus;
                break;

        }
    }

    public void CloseShop()
    {
        currentScreenType = ShopScreenType.Closed;
        PlayCloseShopAnimation();
    }


    void PlayOpenShopAnimation()
    {
        animator.SetBool("opened", true);
    }

    void PlayCloseShopAnimation()
    {
        animator.SetBool("opened", false);
    }

    private void CloseAllScreens()
    {
        autoclikersScreen.SetActive(false);
        bonusScreen.SetActive(false);
        tapScreen.SetActive(false);
    }
}
