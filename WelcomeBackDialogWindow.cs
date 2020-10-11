using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ricimi;

public class WelcomeBackDialogWindow : MonoBehaviour
{
    public Color backgroundColor = new Color(10.0f / 255.0f, 10.0f / 255.0f, 10.0f / 255.0f, 0.6f);

    [SerializeField]
    private Animator animator = default;
    [SerializeField]
    private TextMeshProUGUI timeSinceLastStartText = default;
    [SerializeField]
    private TextMeshProUGUI youEarnedText = default;
    [SerializeField]
    private GameObject panel = default;
    [SerializeField]
    private AnimatedButton okButton = default;

    private double bubblesAmount = 0;

    public static WelcomeBackDialogWindow instance;

    private GameObject m_background;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        panel.SetActive(false);
        okButton.onClick.RemoveAllListeners();

    }

    public void Open(TimeSpan timeSpan)
    {        
        int days = Mathf.FloorToInt((float)timeSpan.TotalDays);

        int hours = Mathf.FloorToInt((float)timeSpan.TotalHours) - 24 * days;

        int min = Mathf.FloorToInt((float)timeSpan.TotalMinutes) - (60 * hours + 1440 * days);

        int sec = Mathf.FloorToInt((float)timeSpan.TotalSeconds) - (60 * min + 3600 * hours + 86400 * days);        

        timeSinceLastStartText.text = days + "д " + hours + "ч " + min + " мин " + sec + "c";

        youEarnedText.text = Score.ScoreToText(IdleBubble.instance.IncomePerSecond * timeSpan.TotalSeconds);


        okButton.onClick.AddListener(() => Close(timeSpan));

        StartCoroutine(RunPopupOpen());

        AddBackground();

    }

    public void Close(TimeSpan timeSpan)
    {
        bubblesAmount = IdleBubble.instance.IncomePerSecond * timeSpan.TotalSeconds;

        BubblesEffectSpawner.instance.SpawnBubbles(15, bubblesAmount, okButton.transform.position,
            Balance.instance.simpleBubblesEffectTarget.position, 0.3f);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            animator.Play("Close");

        okButton.onClick.RemoveAllListeners();

        RemoveBackground();
        StartCoroutine(RunPopupClose());
    }

    private IEnumerator RunPopupOpen()
    {
        yield return new WaitForSeconds(0.7f);
        panel.SetActive(true);
    }
    private IEnumerator RunPopupClose()
    {
        yield return new WaitForSeconds(0.5f);
        panel.SetActive(false);
        if (m_background != null)
            Destroy(m_background);
    }

    private void AddBackground()
    {
        var bgTex = new Texture2D(1, 1);
        bgTex.SetPixel(0, 0, backgroundColor);
        bgTex.Apply();

        m_background = new GameObject("PopupBackground");
        var image = m_background.AddComponent<Image>();
        var rect = new Rect(0, 0, bgTex.width, bgTex.height);
        var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
        image.material.mainTexture = bgTex;
        image.sprite = sprite;
        var newColor = image.color;
        image.color = newColor;
        image.canvasRenderer.SetAlpha(0.0f);
        image.CrossFadeAlpha(1.0f, 0.4f, false);

        var canvas = GameObject.Find("Welcome Back Window Canvas");
        m_background.transform.localScale = new Vector3(1, 1, 1);
        m_background.GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
        m_background.transform.SetParent(canvas.transform, false);
        m_background.transform.SetSiblingIndex(0);
    }

    private void RemoveBackground()
    {
        var image = m_background.GetComponent<Image>();
        if (image != null)
            image.CrossFadeAlpha(0.0f, 0.2f, false);
    }
}
