using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDirection : MonoBehaviour
{
    public static GameObject player;
    private Vector2 playerToSelf;

    void Start()
    {
        player = GameObject.Find("Player");

    }


    void Update()
    {
        playerToSelf = player.transform.position - transform.position;

        //flipping sprites
        if (playerToSelf.normalized.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1); //character faces right
        } else if (playerToSelf.normalized.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1); //character faces left
        }

    }

}
