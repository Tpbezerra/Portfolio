using UnityEngine;

public class Pickup : MonoBehaviour {

    private void OnCollisionEnter(Collision collider)
    {
        PlayerBulletInventory inventory;
        if ((inventory = collider.transform.GetComponent<PlayerBulletInventory>()) != null)
        {
            inventory.SetBulletType("ExplosiveBullet");

            Destroy(gameObject);
        }
    }
}
