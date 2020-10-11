using UnityEngine;
using Enums;

namespace BonusSystem
{
    [CreateAssetMenu(fileName = "Bonus", menuName = "Bonus", order = 51)]
    public class BonusData : ScriptableObject
    {
        public string _name;
        //public AutoclickerType autoclickerType;
        public BonusType bonusType;
        public int index;
        public double price;
        public float multiplier;
        public Sprite sprite;
    }
}




