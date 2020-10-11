using System;
using System.Collections;
using UnityEngine;
using Enums;
using SaveSystem;

namespace Autoclicking
{
    public class Autoclicker : MonoBehaviour
    {

        public static event Action<int, Autoclicker> OnLvlUpEvent;
        public static event Action<Autoclicker> InitializeAutocliker;

        [SerializeField] private GameObject AutoclickerUIGameObject = default;

        [SerializeField] private string saveFileName = default;
        [SerializeField] private string _name = default;
        [SerializeField] public AutoclickerType autoclickerType;
        [SerializeField] private int amount = 1;
        [SerializeField] private double baseCost = 1;

        [Space(20)]
        [SerializeField] private float baseDamage = default;
        [SerializeField] private float damageBonusMultiplier;

        [Space(20)]
        [SerializeField] private float baseLoopTime = default;
        [SerializeField] private float loopTimeBonusMultiplier;

        [Space(20)]
        [SerializeField] private Sprite icon = default;
        [SerializeField] private Sprite questionMarkIcon = default;

        public double Damage => baseDamage * baseLoopTime * level * damageBonusMultiplier;

        public float LoopTime => baseLoopTime / loopTimeBonusMultiplier;

        public double DpS => Damage / LoopTime;

        public double LvlUpCost => baseCost * Math.Pow(1.15, level);

        private AutoclickerUI autoclickerUI;

        private bool isClicking = false;

        private int level = 0;


        void Awake()
        {
            autoclickerUI = AutoclickerUIGameObject.GetComponent<AutoclickerUI>();
            DeactivateUI();
        }

        private void Start()
        {
            InitializeAutocliker?.Invoke(this);
            Debug.Log("startLevel " + level);
        }

        void StartClicking()
        {
            if (!isClicking)
            {
                StartCoroutine("Clicking");
                isClicking = true;
            }
        }

        IEnumerator Clicking()
        {
            while (true)
            {
                yield return new WaitForSeconds(LoopTime / amount);
                Click();
            }
        }

        private void Click()
        {
            AnimationController.instance.PlayAnimation(autoclickerType, Callback);
        }

        private void Callback(double damage, Vector2 position)
        {
            BubblesEffectSpawner.instance.SpawnBubbles(2, damage, position,
                Balance.instance.simpleBubblesEffectTarget.position, 0.1f);

            PopUpTextPool.instance.SpawnText(Score.ScoreToText(Damage), position);

            BubbleSpawn.instance.Damage(damage);
        }

        #region Loading and Upgrading
        public void Activate()
        {
            AutoclickerUIGameObject.SetActive(true);
            LoadData();
            InitializeUI();
            ActivateAnimators();
            StartClicking();
        }

        private void InitializeUI()
        {
            autoclickerUI.SetBought(OnLvlUp, icon, _name, level, Damage, DpS, LoopTime, LvlUpCost);
        }

        private void ActivateAnimators()
        {
            AnimationController.instance.ActivateAnimators(autoclickerType, amount);
        }

        public void Deactivate()
        {
            DeactivateUI();
        }

        public void SetQuestionMark()
        {
            AutoclickerUIGameObject.SetActive(true);
            autoclickerUI.SetQuestionMark(OnBuy, questionMarkIcon, LvlUpCost);
        }

        public void UpgradeDamageMultiplier(float multiplier)
        {
            damageBonusMultiplier *= multiplier;
            UpdateUI(); //Может не сработать!!!
        }

        public void UpgradeLoopTimeMultiplier(float multiplier)
        {
            loopTimeBonusMultiplier *= multiplier;
            UpdateUI(); //Может не сработать!!!
        }

        private void OnBuy()
        {
            Balance.buySimpleBubble(LvlUpCost);
            Debug.Log(level);
            level += 1;
            SaveData();
            AutoclickersController.instance.Buy();
        }

        private void OnLvlUp()
        {
            Balance.buySimpleBubble(LvlUpCost);
            level += 1;
            SaveData();
            UpdateUI();
            OnLvlUpEvent?.Invoke(level, this);
        }

        private void UpdateUI()
        {
            autoclickerUI.UpdateUI(OnLvlUp, level, Damage, DpS, LoopTime, LvlUpCost);
        }

        private void DeactivateUI()
        {
            AutoclickerUIGameObject.SetActive(false);
        }

        private void LoadData()
        {
            AutoclickerSaveData saveData = new AutoclickerSaveData();

            object obj = DataSaver.Load(saveFileName);
            if (obj != null)
                saveData = obj as AutoclickerSaveData;
            else 
                Debug.LogWarning("Creating new sava data");

            level = saveData.level;
            amount = saveData.amount;

            damageBonusMultiplier = saveData.damageBonusMultiplier;
            loopTimeBonusMultiplier = saveData.loopTimeBonusMultiplier;
        }

        private void SaveData()
        {
            AutoclickerSaveData saveData = new AutoclickerSaveData();

            saveData.level = level;
            saveData.amount = amount;

            saveData.damageBonusMultiplier = damageBonusMultiplier;
            saveData.loopTimeBonusMultiplier = loopTimeBonusMultiplier;

            DataSaver.Save(saveData, saveFileName);
        }

        #endregion
    }
}
