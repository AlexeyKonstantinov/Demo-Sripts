using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Autoclicking
{
    public class AutoclickersController : MonoBehaviour
    {
        public static AutoclickersController instance;      

        public List<Autoclicker> autoclickers = new List<Autoclicker>();

        private int level = 0;

        private const string saveFileName = "autoclickerscontroller";

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            LoadData();
        }

        private void Start()
        {
            LoadAutoclikers();
        }

        public void Buy()
        {
            level += 1;
            SaveData();
            LoadAutoclikers();
        }

        private void LoadAutoclikers()
        {

            for (int i = 0; i < level + 1; i++)
            {
                if (i < level)
                {
                    autoclickers[i].Activate();
                }
                else if (i == level && i < autoclickers.Count)
                {
                    autoclickers[i].SetQuestionMark();
                }
            }
        }        

        private void LoadData()
        {
            AutoclickersControllerSaveData saveData = new AutoclickersControllerSaveData();

            object obj = DataSaver.Load(saveFileName);
            if (obj != null)
                saveData = obj as AutoclickersControllerSaveData;
            else 
                Debug.LogWarning("Creating new sava data");

            level = saveData.level;
        }

        private void SaveData()
        {
            AutoclickersControllerSaveData saveData = new AutoclickersControllerSaveData();

            saveData.level = level;

            DataSaver.Save(saveData, saveFileName);
        }
    }
}
