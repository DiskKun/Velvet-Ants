using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //variables for player movement
    public Vector3 startPos;
    public Vector3 destination;
    private Vector3 totalDistance;
    private Vector3 toDestination;
    public float speed = 3;
    bool facingRight;

    static bool canMove = true;

    public float playerPositionOffset;

    public AnimationCurve lerpCurve;
    private float lerpTimer;
    public float interpolation;

    //changing order in layer when in dialogue
    public InputManager inputManager;
    private SpriteRenderer sprite;

    void Start()
    {
        destination = transform.position;
        startPos = transform.position;
        toDestination = Vector3.right;
        totalDistance = Vector3.right;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        lerpTimer += Time.deltaTime;
        interpolation = lerpCurve.Evaluate(lerpTimer / totalDistance.magnitude * speed);
        transform.position = Vector3.Lerp(startPos, destination, interpolation);

    }


    void Update()
    {
        
        if (Input.GetMouseButtonUp(0) && canMove)
        {
            destination = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y); //sets the player's destination to whereever mouse clicks
            lerpTimer = 0;
            startPos = transform.position;
            totalDistance = destination - startPos;
        }
        toDestination = destination - transform.position;


        if (toDestination.x < 0 && canMove)
        {
            facingRight = true;
            
        }
        else if (toDestination.x > 0 && canMove)
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

        if (inputManager.currentGameMode == "dialogue")
        {
            sprite.sortingOrder = 21; //above pillars
        } else
        {
            sprite.sortingOrder = 5; //default sorting order
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
        lerpTimer = 0;
        startPos = transform.position;

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

    public void MovementPause()
    {
        StartCoroutine("PauseMovementCoroutine");
    }

    public IEnumerator PauseMovementCoroutine()
    {
        canMove = false;
        yield return new WaitForEndOfFrame();
        canMove = true;
    }

}
