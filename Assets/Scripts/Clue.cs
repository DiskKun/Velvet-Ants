using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clue : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool mouseDown = false;
    bool mouseMove = false;

    RectTransform rt;

    public float hoverSize;
    public float clickSize;
    public float moveSize;
    public float animationSpeed = 1;
    public AnimationCurve animationCurve;
    
    float timeAlongCurve;

    Vector3 normalScale;
    Vector3 hoverScale;
    Vector3 clickScale;
    Vector3 moveScale;

    string mode = "normal";
    
    

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        normalScale = rt.localScale;
        hoverScale = Vector3.one * hoverSize;
        clickScale = Vector3.one * clickSize;
        moveScale = Vector3.one * moveSize;
    }

    // Update is called once per frame
    void Update()
    {

        if (mode == "enter")
        {
            timeAlongCurve += Time.deltaTime * animationSpeed;
            rt.localScale = Vector3.Lerp(normalScale, hoverScale, animationCurve.Evaluate(timeAlongCurve));
        } else if (mode == "exit")
        {
            timeAlongCurve -= Time.deltaTime * animationSpeed;
            rt.localScale = Vector3.Lerp(normalScale, hoverScale, animationCurve.Evaluate(timeAlongCurve));
        } else if (mode == "down")
        {
            timeAlongCurve += Time.deltaTime * animationSpeed;
            rt.localScale = Vector3.Lerp(hoverScale, clickScale, animationCurve.Evaluate(timeAlongCurve));
        } else if (mode == "fromMove")
        {
            timeAlongCurve += Time.deltaTime * animationSpeed;
            rt.localScale = Vector3.Lerp(moveScale, hoverScale, animationCurve.Evaluate(timeAlongCurve));
        } else if (mode == "move")
        {
            timeAlongCurve -= Time.deltaTime * animationSpeed;
            rt.localScale = Vector3.Lerp(moveScale, clickScale, animationCurve.Evaluate(timeAlongCurve));
        } else if (mode == "fromClick")
        {
            timeAlongCurve -= Time.deltaTime * animationSpeed;
            rt.localScale = Vector3.Lerp(hoverScale, clickScale, animationCurve.Evaluate(timeAlongCurve));
            if (timeAlongCurve <= 0)
            {
                timeAlongCurve = 1;
                mode = "enter";
            }
        }

        timeAlongCurve = Mathf.Clamp01(timeAlongCurve);

    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        if (mouseDown)
        {
            mode = "move";
            rt.position = Input.mousePosition;
            mouseMove = true;
        }
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!mouseMove)
        {
            Debug.Log("Click functionality executed here");
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Debug.Log("Down!");
        timeAlongCurve = 0;
        mouseDown = true;
        mouseMove = false;
        mode = "down";
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        //Debug.Log("Up!");
        mouseDown = false;
        if (mode == "down")
        {
            mode = "fromClick";
        } else if (mode == "move")
        {
            mode = "fromMove";
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mode = "enter";
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        mode = "exit";
    }

}
