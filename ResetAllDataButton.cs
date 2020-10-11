using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem;

public class ResetAllDataButton : MonoBehaviour
{
    public void ResetAllData()
    {
        BubbleSaveData bubbleSaveData = new BubbleSaveData();

        for (int i = 0; i < 6; i++)
        {
            DataSaver.Save(bubbleSaveData, $"{i}");
        }        

        IdleBubbleSaveData idleBubbleSaveData = new IdleBubbleSaveData();
        DataSaver.Save(idleBubbleSaveData, "idlebubble");

        BalanceSaveData balanceSaveData = new BalanceSaveData();
        DataSaver.Save(balanceSaveData, "balance");

        AutoclickerSaveData autoclickerSaveData = new AutoclickerSaveData();
        DataSaver.Save(autoclickerSaveData, "pin");
        DataSaver.Save(autoclickerSaveData, "fork");
        DataSaver.Save(autoclickerSaveData, "knife");
        DataSaver.Save(autoclickerSaveData, "dart");
        DataSaver.Save(autoclickerSaveData, "shuriken");

        TouchDamageSaveData touchDamageSaveData = new TouchDamageSaveData();
        DataSaver.Save(touchDamageSaveData, "touchdamage");

        AutoclickersControllerSaveData autoclickersControllerSaveData = new AutoclickersControllerSaveData();
        DataSaver.Save(autoclickersControllerSaveData, "autoclickerscontroller");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}
