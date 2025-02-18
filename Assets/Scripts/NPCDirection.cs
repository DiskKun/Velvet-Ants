using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDirection : MonoBehaviour
{
    public static GameObject player;
    private Vector2 playerToSelf;
    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        playerToSelf = player.transform.position - transform.position;

        if (playerToSelf.normalized.x > 0)
        {
            sprite.flipX = true;
        } else if (playerToSelf.normalized.x < 0)
        {
            sprite.flipX = false;
        }
    }
}
