using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.transform.GetComponent<PlayerBulletInventory>())
        {
            collider.transform.GetComponent<PlayerBulletInventory>().SetBulletType("ExplosiveBullet");

            Destroy(gameObject);
        }
    }
}
