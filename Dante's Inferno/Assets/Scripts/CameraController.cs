using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    public Transform player;

    // Standard methods
    void Update()
    {
        FollowingPlayer();
    }

    //My methods
    void FollowingPlayer() {
        if (player.position.x > -1.8f) {
            transform.position = new Vector3(player.position.x, -0.8f, transform.position.z);
        }
        else {
            transform.position = new Vector3(-3.31f, -0.8f, transform.position.z);
        }

        //add stuff for end
    }
}
