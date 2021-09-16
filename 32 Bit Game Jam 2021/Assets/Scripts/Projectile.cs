using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            //play hit something sound effect here

            collided = true;
            Destroy(gameObject);
        }

    }


    private void Start()
    {
        //play shoot sound effect here
    }
}
