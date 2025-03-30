using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //variables for player movement
    Rigidbody2D playerRB;
    private Vector2 startPos;
    public Vector2 destination;
    private Vector2 movement;
    public float speed = 3;
    bool facingRight;

    static bool canMove = true;

    public float playerPositionOffset;

    public AnimationCurve lerpCurve;
    private float lerpTimer;
    public float movementDuration;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        destination = transform.position;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        //movement = destination - (Vector2)transform.position; //gets the vector from the current position to the destination clicked by mouse


        //if (movement.magnitude < 0.1) //if within range of the destination, stop moving
        //{
        //    movement = Vector2.zero;
        //}
        //playerRB.velocity = movement.normalized * speed; //set player's velocity to the movement vector

        lerpTimer += Time.deltaTime;
        float interpolation = lerpCurve.Evaluate(lerpTimer/movementDuration);
        transform.position = Vector3.Lerp(startPos, destination, interpolation);
    }


    void Update()
    {
        startPos = transform.position;

        if (Input.GetMouseButtonUp(0) && canMove)
        {
            destination = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y); //sets the player's destination to whereever mouse clicks
            lerpTimer = 0;
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
            destination = new Vector2(target.position.x + playerPositionOffset, transform.position.y);
            facingRight = true;
        } else
        {
            destination = new Vector2(target.position.x - playerPositionOffset, transform.position.y);
            facingRight = false;
        }

    }

    public void StopMoving()
    {
        destination = new Vector2(transform.position.x, transform.position.y);
    }

}
