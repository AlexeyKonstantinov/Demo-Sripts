using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogWindow : MonoBehaviour
{
    public static DialogWindow instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject dialogWindowObject;

    public Button yesButton;
    public Button noButton;
    public TextMeshProUGUI messageText;

    public void Open(string message, UnityAction yesAction, UnityAction noAction)
    {
        dialogWindowObject.SetActive(true);

        messageText.text = message;

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesAction);
        yesButton.onClick.AddListener(Close);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(noAction);
        noButton.onClick.AddListener(Close);

    }

    public void Close()
    {
        dialogWindowObject.SetActive(false);
    }
}
