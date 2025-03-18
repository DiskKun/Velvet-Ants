using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clue : MonoBehaviour, IPointerMoveHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool mouseDown = false;
    bool mouseMove = false;

    RectTransform rt;

    float hoverSize = 1.25f;
    float clickSize = 1.125f;
    float moveSize = 1.5f;
    float animationSpeed = 5;
    public DialogueManager dialogueManager;
    public int descriptionID;
    public AnimationCurve animationCurve;
    
    float timeAlongCurve;

    Vector3 normalScale;
    Vector3 hoverScale;
    Vector3 clickScale;
    Vector3 moveScale;

    Vector3 movePoint;
    Vector3 mouseMovePoint;

    string mode = "normal";
    
    

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        normalScale = rt.localScale;
        hoverScale = normalScale * hoverSize;
        clickScale = normalScale * clickSize;
        moveScale = normalScale * moveSize;
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
            rt.position = movePoint + (Input.mousePosition - mouseMovePoint);
            mouseMove = true;
        }
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!mouseMove)
        {
            Debug.Log("Click functionality executed here");
            dialogueManager.CommenceDialogue(descriptionID);
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        timeAlongCurve = 0;
        mouseDown = true;
        mouseMove = false;
        movePoint = rt.position;
        mouseMovePoint = Input.mousePosition;
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
