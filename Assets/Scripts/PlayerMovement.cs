using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables for player movement
    Rigidbody2D playerRB;
    public Vector2 destination;
    private Vector2 movement;
    public float speed = 3;

    //variables for dialogue prompt
    public GameObject dialoguePrompt;
    bool canMove = true;
    public InputManager inputManager;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

        dialoguePrompt.SetActive(false); //don't show dialogue prompt
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

    public void AllowMovement(bool t)
    {
        canMove = t;
    }

    void Update()
    {
        ////old way
        if (Input.GetMouseButtonUp(0) && canMove)
        {
            destination = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0); //sets the player's destination to whereever mouse clicks
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NPC") //if enter NPC trigger zone
        {
            //show dialogue prompt
            dialoguePrompt.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            if (dialoguePrompt != null)
            {
                dialoguePrompt.SetActive(false);

            }
        }
    }
}
