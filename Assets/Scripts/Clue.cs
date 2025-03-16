using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clue : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    bool mouseDown = false;
    bool mouseMove = false;

    RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        if (mouseDown)
        {
            rt.position = Input.mousePosition;
            mouseMove = true;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!mouseMove)
        {
            Debug.Log("Click!");
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Debug.Log("Down!");
        mouseDown = true;
        mouseMove = false;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Debug.Log("Up!");
        mouseDown = false;
    }

}
