using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //variables for player movement
    Rigidbody2D playerRB;
    private Vector2 destination;
    private Vector2 movement;
    public float speed = 3;

    public GameObject dialoguePrompt;
    bool canMove = true;

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

    public void ToggleMovement()
    {
        canMove = !canMove;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && canMove)
        {
            destination = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0); //sets the player's destination to whereever mouse clicks
        }

        //makes player face the direction they are moving
        Vector3 scale = transform.localScale;
        if (movement.x > 0)
        {
            scale.x = 1;
            transform.localScale = scale;
        }
        else if (movement.x < 0)
        {
            scale.x = -1;
            transform.localScale = scale;
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
            dialoguePrompt.SetActive(false);
        }
    }
}
