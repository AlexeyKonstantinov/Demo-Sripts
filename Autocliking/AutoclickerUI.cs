using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

namespace Autoclicking
{
    public class AutoclickerUI : MonoBehaviour
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI dpsText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI timeText;

        [Space()]
        public TextMeshProUGUI upgradeText;

        [Space()]
        public Image _icon;

        public BuyButton buyButton;

        public void UpdateUI(UnityAction onLvlUpCallback, int level, double damage, double dps, double time, double lvlUpCost)
        {
            buyButton.SetUpButton(lvlUpCost, onLvlUpCallback);

            levelText.text = level.ToString();
            damageText.text = Score.ScoreToText(damage);
            dpsText.text = Score.ScoreToText(dps);
            timeText.text = Score.ScoreToText(time);
        }

        public void SetBought(UnityAction onLvlUpCallback, Sprite icon, string name, int level, double damage,
            double dps, double time, double lvlUpCost)
        {
            buyButton.SetUpButton(lvlUpCost, onLvlUpCallback);

            _icon.sprite = icon;
            upgradeText.text = "UPGRADE";

            nameText.text = name;
            levelText.text = level.ToString();
            damageText.text = Score.ScoreToText(damage);
            dpsText.text = Score.ScoreToText(dps);
            timeText.text = Score.ScoreToText(time);
        }

        public void SetQuestionMark(UnityAction onLvlUpCallback, Sprite questionMarkIcon, double price)
        {
            buyButton.SetUpButton(price, onLvlUpCallback);

            _icon.sprite = questionMarkIcon;
            upgradeText.text = "BUY";

            nameText.text = "???";
            levelText.text = "???";
            damageText.text = "???";
            dpsText.text = "???";
            timeText.text = "???";
        }
    }
}