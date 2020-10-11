using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IPointerClickHandler,IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static event Action BeginDragEvent;
    public static event Action<float> DragEvent;
    public static event Action EndDragEvent;
    public static event Action BeginHoldEvent;
    public static event Action EndHoldEvent;
    public static event Action<Vector2> ClickEvent;

    private bool isDragging = false;
    private bool isHolding = false;

    private float screenHeight;
    private float screenWidth;

    private Camera mainCamera;

    private void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isHolding)
        {
            StopCoroutine("HoldCoroutine");
            BeginDrag();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
            Drag(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isDragging)
            EndDrag();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging && !isHolding)
        {
            Click(eventData);
        }
        StopCoroutine("HoldCoroutine");
        if (isHolding)
            EndHold();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("HoldCoroutine");
    }

    IEnumerator HoldCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        BeginHold();
    }


    void BeginDrag()
    {
        isDragging = true;
        BeginDragEvent?.Invoke();
    }

    void Drag(Vector2 delta)
    {
        Vector2 speed = new Vector2(delta.x / screenWidth, delta.y / screenHeight);
        DragEvent?.Invoke(speed.magnitude);
    }

    void EndDrag()
    {        
        isDragging = false;
        EndDragEvent?.Invoke();
    }

    void BeginHold()
    {
        BeginHoldEvent?.Invoke();
        isHolding = true;
    }

    void EndHold()
    {
        EndHoldEvent?.Invoke();
        isHolding = false;
    }

    void Click(PointerEventData eventData)
    {
        Vector2 pos = mainCamera.ScreenToWorldPoint(eventData.position);

        ClickEvent?.Invoke(pos);
    }

    
}
