using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrailController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject trailPrefab;

    private Camera mainCamera;

    private Transform _transform;

    private Transform currentTrail;

    private Queue<Transform> trailQueue = new Queue<Transform>();

    private const int poolSize = 5;

    private void Awake()
    {
        _transform = transform;
        mainCamera = Camera.main;
        PreparePool();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(trailQueue.Count > 0)
        {
            currentTrail = trailQueue.Dequeue();
            currentTrail.gameObject.SetActive(true);
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
            currentTrail.position = new Vector3(worldPosition.x, worldPosition.y, 0.0f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        currentTrail.position = new Vector3(worldPosition.x, worldPosition.y, 0.0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentTrail.gameObject.SetActive(false);
        trailQueue.Enqueue(currentTrail);
        currentTrail = null;
    }

    void PreparePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Transform trail = Instantiate(trailPrefab, _transform).transform;
            trail.gameObject.SetActive(false);
            trailQueue.Enqueue(trail);
        }
    }
}
