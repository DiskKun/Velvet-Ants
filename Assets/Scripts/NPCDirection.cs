using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDirection : MonoBehaviour
{
    public static GameObject player;
    private Vector2 playerToSelf;
    private SpriteRenderer sprite;

    private Vector3 mousePos;
    private Collider2D clickArea;
    private PlayerMovement playerMovement;
    public float playerPositionOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sprite = GetComponentInParent<SpriteRenderer>();
        clickArea = GetComponent<Collider2D>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerToSelf = player.transform.position - transform.position;

        //flipping sprites
        if (playerToSelf.normalized.x > 0)
        {
            sprite.flipX = true; //facing right
        } else if (playerToSelf.normalized.x < 0)
        {
            sprite.flipX = false; //facing left
        }

    }

    private void OnMouseDown()
    {
        print("down");
        //if player clicks directly on the NPC, move them beside the NPC
        if (playerToSelf.normalized.x > 0) //facing right
        {
            playerMovement.destination = transform.position + new Vector3(playerPositionOffset, 0, 0);
        }
        else if (playerToSelf.normalized.x < 0) //facing left
        {
            playerMovement.destination = transform.position - new Vector3(playerPositionOffset, 0, 0);
        }
    }
}
