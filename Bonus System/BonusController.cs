using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using Autoclicking;
using SaveSystem;

namespace BonusSystem
{
    public class BonusController : MonoBehaviour
    {
        public List<Bonus> bonuses;

        public GameObject bonusViewPrefab;

        public RectTransform bonusViewPanel;

        public BonusScreenSorting sorting;

        private void Awake()
        {
            Autoclicker.OnLvlUpEvent += LevelUpHandler;
            Autoclicker.InitializeAutocliker += LoadData;
        }

        private void OnDisable()
        {
            Autoclicker.OnLvlUpEvent -= LevelUpHandler;
            Autoclicker.InitializeAutocliker -= LoadData;
        }

        private void LoadData(Autoclicker autoclicker)
        {
            Bonus bonus = bonuses.Find(x => x.autoclickerType == autoclicker.autoclickerType);
            if (bonus != null)
                bonus.LoadData();

            var bonusDataList = bonus.GetAvailibleBonusDataToSpawn();

            for (int i = 0; i < bonusDataList.Count; i++)
            {
                BonusView bonusView = Instantiate(bonusViewPrefab, bonusViewPanel).GetComponent<BonusView>();

                bonusView.Init(bonusDataList[i], BuyBonus, autoclicker);
            }

            sorting.Sort();

        }

        void LevelUpHandler(int level, Autoclicker autoclicker)
        {
            if (level % 5 == 0)
            {
                int index = level / 5;
                SpawnBonus(index, autoclicker);
            }
        }

        void SpawnBonus(int index, Autoclicker autoclicker)
        {
            Bonus bonus = bonuses.Find(x => x.autoclickerType == autoclicker.autoclickerType);

            BonusData bonusData = bonus.GetBonusData(index);

            BonusView bonusView = Instantiate(bonusViewPrefab, bonusViewPanel).GetComponent<BonusView>();

            bonusView.Init(bonusData, BuyBonus, autoclicker);

            sorting.Sort();
        }

        void BuyBonus(Autoclicker autoclicker, BonusData bonusData)
        {
            Balance.buySimpleBubble(bonusData.price);

            bonuses.Find(x => x.autoclickerType == autoclicker.autoclickerType).BuyBonus(bonusData.index);

            ApplyBonus(bonusData, autoclicker);

            sorting.Sort();
        }

        private void ApplyBonus(BonusData bonusData, Autoclicker autoclicker)
        {
            switch (bonusData.bonusType)
            {
                case BonusType.DamageMultiplier:
                    autoclicker.UpgradeDamageMultiplier(bonusData.multiplier);
                    break;
                case BonusType.LoopTimeMultiplier:
                    autoclicker.UpgradeLoopTimeMultiplier(bonusData.multiplier);
                    break;
            }
        }
    }

    [Serializable]
    public class Bonus
    {
        public string _name;

        public AutoclickerType autoclickerType;
        public List<BonusData> bonuses;

        public int AvailableIndex { get; private set; }
        public int BoughtIndex { get; private set; }

        public void UpdateBonuses(int index)
        {
            AvailableIndex = index;
        }

        public void BuyBonus(int index)
        {
            BoughtIndex = index;
        }

        public BonusData GetBonusData(int index)
        {
            AvailableIndex = index;
            if (bonuses.Count >= index)
            {
                return bonuses[index - 1];
            }
            else
                return null;
        }

        public List<BonusData> GetAvailibleBonusDataToSpawn()
        {
            List<BonusData> bonusDataList = new List<BonusData>();
            for (int i = BoughtIndex; i <= AvailableIndex; i++)
            {
                bonusDataList.Add(bonuses[i]);
            }

            return bonusDataList;
        }

        public List<int> GetAvailableBonusesIndex()
        {
            List<int> bonusesIndex = new List<int>();

            for (int i = BoughtIndex + 1; i <= AvailableIndex; i++)
            {
                bonusesIndex.Add(i);
            }
            return bonusesIndex;
        }

        public void LoadData()
        {
            BonusSaveData saveData = new BonusSaveData();

            AvailableIndex = saveData.availableIndex;
            BoughtIndex = saveData.boughtIndex;
        }
    }
}



