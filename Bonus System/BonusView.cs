using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Autoclicking;

namespace BonusSystem
{
    public class BonusView : MonoBehaviour
    {
        public Image icon;

        public BuyButton buyButton;

        UnityAction<Autoclicker, BonusData> buyAction;

        public BonusData bonusData;

        Autoclicker autoclicker;

        public void Init(BonusData bonusData, UnityAction<Autoclicker, BonusData> buyAction, Autoclicker autoclicker)
        {
            icon.sprite = bonusData.sprite;
            this.buyAction = buyAction;
            this.bonusData = bonusData;
            this.autoclicker = autoclicker;
            buyButton.SetUpButton(bonusData.price,
                () => DialogWindow.instance.Open($"Buy {bonusData._name} for {bonusData.price}?", Buy, () => { }));
        }

        private void Buy()
        {
            buyAction.Invoke(autoclicker, bonusData);
            Destroy(gameObject);
        }
    }
}
