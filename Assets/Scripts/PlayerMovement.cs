using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //variables for player movement
    public Vector3 startPos;
    public Vector3 destination;
    private Vector3 movement;
    private Vector3 toDestination;
    public float speed = 3;
    bool facingRight;

    static bool canMove = true;

    public float playerPositionOffset;

    public AnimationCurve lerpCurve;
    private float lerpTimer;
    public float interpolation;


    void Start()
    {
        destination = transform.position;
        startPos = transform.position;
        toDestination = Vector2.right;
    }

    private void FixedUpdate()
    {
        lerpTimer += Time.deltaTime;
        interpolation = lerpCurve.Evaluate(lerpTimer / toDestination.magnitude * speed);
        transform.position = Vector3.Lerp(startPos, destination, interpolation);

    }


    void Update()
    {
        
        if (Input.GetMouseButtonUp(0) && canMove)
        {
            destination = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y); //sets the player's destination to whereever mouse clicks
            lerpTimer = 0;
            startPos = transform.position;
            toDestination = destination - startPos;
            
        }

        if (movement.x < 0 && canMove)
        {
            facingRight = true;
            
        }
        else if (movement.x > 0 && canMove)
        {
            facingRight = false;

        }

        if (facingRight)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1); //character faces right
        } else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); //character faces left
        }

    }

    public static IEnumerator AllowMovement(bool t)
    {
        if (t == true)
        {
            yield return new WaitForEndOfFrame();
        }
        canMove = t;
        Debug.Log("Change");
    }

    public void DialoguePositioning(Transform target)
    {
        //GetComponentInChildren<SpriteRenderer>().sortingOrder = 100;

        if (target.parent.transform.localScale == new Vector3(1, 1, 1))
        {
            destination = new Vector3(target.position.x + playerPositionOffset, transform.position.y);
            facingRight = true;
        } else
        {
            destination = new Vector3(target.position.x - playerPositionOffset, transform.position.y);
            facingRight = false;
        }

    }

    public void StopMoving()
    {
        destination = new Vector3(transform.position.x, transform.position.y);
    }

}
