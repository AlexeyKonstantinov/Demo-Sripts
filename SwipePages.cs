using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipePages : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{    
    public float percentThreshold = 0.2f;
    public float speedThreshold = 0.38f;
    public float easing = 0.5f;
    public float pageWidth = 2;
    public int totalPages = 1;

    private int currentPage = 1;
    private float lastDragSpeed;
    private float previousPositionX = 0f;
    private float currentPositionX = 0f;
    private float pressPositionX;
    private Vector3 panelLocation;

    private Transform _transform;

    private Camera cam;

    private void Awake()
    {
        _transform = transform;

        cam = Camera.main;
    }
    void Start()
    {
        panelLocation = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        pressPositionX = cam.ScreenToWorldPoint(eventData.pressPosition).x;
    }

    public void OnDrag(PointerEventData data)
    {
        currentPositionX = cam.ScreenToWorldPoint(data.position).x;

        float difference = pressPositionX - currentPositionX;

        _transform.position = panelLocation - new Vector3(difference, 0, 0);

        lastDragSpeed = currentPositionX - previousPositionX;
        previousPositionX = currentPositionX;
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (pressPositionX - cam.ScreenToWorldPoint(data.position).x ) / pageWidth;
        if (Mathf.Abs(percentage) >= percentThreshold || Mathf.Abs(lastDragSpeed) > speedThreshold)
        {
            Vector3 newLocation = panelLocation;
            if ((percentage > 0 || lastDragSpeed < 0) && currentPage < totalPages) 
            {
                currentPage++;
                newLocation += new Vector3(-pageWidth, 0, 0);
            }
            else if ( (percentage < 0 || lastDragSpeed > 0) && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(pageWidth, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }

        lastDragSpeed = 0f;
        previousPositionX = 0f;
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

}
