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
    public Vector2 destination;
    private Vector2 movement;
    public float speed = 3;

    static bool canMove = true;

    public List<Collider2D> specialMovementColliders;
    GameObject foundCollider;
    public float playerPositionOffset;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        movement = destination - (Vector2)transform.position; //gets the vector from the current position to the destination clicked by mouse


        if (movement.magnitude < 0.1) //if within range of the destination, stop moving
        {
            movement = Vector2.zero;
        }
        playerRB.velocity = movement.normalized * speed; //set player's velocity to the movement vector
    }

    static public void AllowMovement(bool t)
    {
        canMove = t;
    }

    void Update()
    {

        //GIL's WIP
        if (Input.GetMouseButtonUp(0) && canMove)
        {
            foreach (Collider2D collider in specialMovementColliders)
            {
                Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (collider  == hitCollider)
                {
                    foundCollider = collider.gameObject;
                    Debug.Log("found collider: " + foundCollider);
                }
                //Debug.Log(collider);
            }
            if (foundCollider != null)
            {
                //move to the designated position near the collider
                Vector2 playerToCollider = transform.position - foundCollider.transform.position;
                if (playerToCollider.normalized.x > 0) //facing right
                {
                    destination = transform.position + new Vector3(playerPositionOffset, 0, 0);
                }
                else if (playerToCollider.normalized.x < 0) //facing left
                {
                    destination = transform.position - new Vector3(playerPositionOffset, 0, 0);
                }
            }
            else
            {
            destination = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y); //sets the player's destination to whereever mouse clicks
            }

        }

        if (movement.x > 0)
        {
            gameObject.transform.localScale = new Vector3 (-1, 1, 1); //character faces left
        }
        else if (movement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1); //character faces right
        }

    }

}
