using System;

namespace SaveSystem
{
    [Serializable]
    public class BubbleSaveData
    {
        public int level;
        public int damageLevel;
        public BubbleSaveData()
        {
            level = 1;
            damageLevel = 1;
        }
    }

    [Serializable]
    public class BalanceSaveData
    {
        public double simpleBubbleBalance;
        public double superBubbleBalance;

        public BalanceSaveData()
        {
            simpleBubbleBalance = 5;
            superBubbleBalance = 1;
        }
    }

    [Serializable]
    public class IdleBubbleSaveData
    {
        public int tubesAmount;
        public int frequencyLevel;
        public int incomeLevel;
        public double balance;

        public IdleBubbleSaveData()
        {
            tubesAmount = 1;
            frequencyLevel = 1;
            incomeLevel = 1;
            balance = 0;
        }
    }

    [Serializable]
    public class BonusSaveData
    {
        public int availableIndex;
        public int boughtIndex;

        public BonusSaveData()
        {
            availableIndex = 2;
            boughtIndex = 0;
        }
    }

    [Serializable]
    public class TouchDamageSaveData
    {
        public int tapDamageLevel;
        public double tapDamageMultiplier;

        public int criticalHitLevel;
        public double criticalHitMultiplier;

        public int holdDamageLevel;
        public double holdDamageMultiplier;

        public int holdCriticalHitLevel;
        public double holdCriticalHitMultiplier;

        public int swipeDamageLevel;
        public double swipeDamageMultiplier;

        public int swipeSpeedMultiplierLevel;
        public double swipeSpeedMultiplierMultiplier;

        public TouchDamageSaveData()
        {
            tapDamageLevel = 0;
            tapDamageMultiplier = 1;

            criticalHitLevel = 0;
            criticalHitMultiplier = 1;

            holdDamageLevel = 0;
            holdDamageMultiplier = 1;

            holdCriticalHitLevel = 0;
            holdCriticalHitMultiplier = 1;

            swipeDamageLevel = 0;
            swipeDamageMultiplier = 1;

            swipeSpeedMultiplierLevel = 0;
            swipeSpeedMultiplierMultiplier = 1;
        }
    }

    [Serializable]
    public class AutoclickerSaveData
    {
        public int level;
        public int amount;

        public float damageBonusMultiplier;
        public float loopTimeBonusMultiplier;

        public AutoclickerSaveData()
        {
            level = 0;
            amount = 1;

            damageBonusMultiplier = 1;
            loopTimeBonusMultiplier = 1;
        }
    }

    [Serializable]
    public class AutoclickersControllerSaveData
    {
        public int level;

        public AutoclickersControllerSaveData()
        {
            level = 0;
        }
    }
}