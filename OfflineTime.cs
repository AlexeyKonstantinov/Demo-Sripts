using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class OfflineTime : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private const string dateName = "dateQuit";

    private void Start()
    {
        
        TimeSpan timeSpan = CheckTimeBetweenSessions();

        if (timeSpan.TotalSeconds > 3)
            WelcomeBackDialogWindow.instance.Open(timeSpan);

        StartCoroutine("SaveTimeCoroutine");
    }

    IEnumerator SaveTimeCoroutine()
    {
        SaveCurrentTime();
        yield return new WaitForSeconds(10);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveCurrentTime();
        else
            CheckTimeBetweenSessions();
    }

    private void OnApplicationQuit()
    {
        SaveCurrentTime();
    }


    private TimeSpan CheckTimeBetweenSessions()
    {
        TimeSpan timeSpan = GetTimeSpanSinceLastStart();

        /*
        if(timeSpan.TotalSeconds > 10)
        {
            text.text = System.Environment.NewLine + "TimeSpan: " + timeSpan.TotalSeconds.ToString();
        }else
        {
            text.text = System.Environment.NewLine + "TimeSpan: " + timeSpan.TotalSeconds.ToString();
        }
        */

        return timeSpan;
    }


    private static TimeSpan GetTimeSpanSinceLastStart()
    {
        string dateQuitString = PlayerPrefs.GetString(dateName, "");
        if (!dateQuitString.Equals(""))
        {
            DateTime quitTime = DateTime.Parse(dateQuitString);
            DateTime currentTime = UnbiasedTime.Instance.Now();

            if (currentTime > quitTime)
            {
                TimeSpan timeSpan = currentTime - quitTime;
                return timeSpan;
            }else
            {
                TimeSpan timeSpan = currentTime - currentTime;
                return timeSpan;
            }
        }
        else
        {
            DateTime currentTime = UnbiasedTime.Instance.Now();
            TimeSpan timeSpan = currentTime - currentTime;
            return timeSpan;
        }
    }

    void SaveCurrentTime()
    {
        DateTime currentTime = UnbiasedTime.Instance.Now();
        PlayerPrefs.SetString(dateName, currentTime.ToString());
    }    

    
}
