using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BubblesEffectSpawner : MonoBehaviour
{
    public static BubblesEffectSpawner instance;

    [Header("Префаб обычного пузыря")]
    public GameObject simpleBubbleEffectPrefab;
    [Header("Префаб супер пузыря")]
    public GameObject superBubbleEffectPrefab;

    [Space]
    [Header("Настройки анимации")]
    [SerializeField] [Range(0.35f, 0.9f)] float minAnimDuration = default;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration = default;
    [Header("Тип анимации")]
    [SerializeField] Ease easeType = Ease.InOutBack;

    [Space]
    [Header("Количество simple bubble в пуле")]
    [SerializeField] float simpleBubbleInPoolCount = 21;

    private Queue<GameObject> simpleBubblesQueue = new Queue<GameObject>();
    private Queue<GameObject> superBubblesQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PrepareSimpleBubbles();
        PrepareSuperBubbles();
    }

    public void SpawnBubbles(int amount, double sum, Vector3 position, Vector3 target, float randomness)
    {
        if (simpleBubblesQueue.Count > amount)
        {
            for (int i = 0; i < amount; i++)
            {

                GameObject bubble = simpleBubblesQueue.Dequeue();
                bubble.transform.position = position + new Vector3(Random.Range(-randomness,randomness),Random.Range(-randomness,randomness));
                bubble.SetActive(true);

                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                
                bubble.transform.DOMove(new Vector3(target.x, target.y,0.0f), duration)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        Balance.addSimpleBubble(sum / amount);
                        bubble.SetActive(false);
                        simpleBubblesQueue.Enqueue(bubble);
                    });
            }
        } 
        else
        {
            Balance.addSimpleBubble(sum);
        }
    }

    public void SpawnSuperBubbles(int amount, double sum, Vector3 position, Vector3 target, float randomness)
    {
        if (superBubblesQueue.Count > amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject bubble = superBubblesQueue.Dequeue();
                bubble.transform.position = position + new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness));
                bubble.SetActive(true);

                float duration = Random.Range(minAnimDuration, maxAnimDuration);

                bubble.transform.DOMove(new Vector3(target.x, target.y, 0.0f), duration + 1)
                    .SetEase(easeType)
                    .OnComplete(() =>
                    {
                        Balance.addSuperBubble(sum / amount);
                        bubble.SetActive(false);
                        superBubblesQueue.Enqueue(bubble);
                    });
            }
        }
        else
        {
            Balance.addSuperBubble(sum);
        }
    }

    void PrepareSimpleBubbles()
    {
        for (int i = 0; i < simpleBubbleInPoolCount; i++)
        {
            var bubble = Instantiate(simpleBubbleEffectPrefab, transform);
            bubble.SetActive(false);
            simpleBubblesQueue.Enqueue(bubble);
        }
    }
    void PrepareSuperBubbles()
    {
        for (int i = 0; i < 3; i++)
        {
            var bubble = Instantiate(superBubbleEffectPrefab, transform);
            bubble.SetActive(false);
            superBubblesQueue.Enqueue(bubble);
        }
    }
}
