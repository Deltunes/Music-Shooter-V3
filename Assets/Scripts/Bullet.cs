using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int collisionNum = 0;
    public int maxCollisions = 1;
    private void OnCollisionEnter(Collision collision)
    {
        print("hit " + collision.gameObject.name + " !");
        if (collision.gameObject.tag == "Destructible")
        {
            Destroy(collision.gameObject);
        }
        if (collisionNum == maxCollisions)
        {
            Destroy(gameObject);
        }
        collisionNum++;
    }
}
