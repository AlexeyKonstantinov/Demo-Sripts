using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextPool : MonoBehaviour
{

    public static PopUpTextPool instance;

    [SerializeField] private GameObject popupTextPrefab = default;

    private const int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>(poolSize);

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PreparePool();
    }

    public void SpawnText(string text, Vector3 position)
    {
        if (pool.Count > 0)
        {
            GameObject popup = pool.Dequeue();
            popup.transform.position = position;
            popup.SetActive(true);

            PopUpText popupText = popup.GetComponent<PopUpText>();

            popupText.PlayAnimation(text, () => { pool.Enqueue(popup); });

        }
    }

    void PreparePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject popup = Instantiate(popupTextPrefab, transform);
            popup.SetActive(false);
            pool.Enqueue(popup);
        }
    }

}
