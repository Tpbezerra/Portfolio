using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAttacking))]
public class PlayerBulletInventory : MonoBehaviour {

    public Projectile NormalBullet;
    public Projectile ExplosiveBullet;

    PlayerAttacking attacker;

    void Start()
    {
        attacker = GetComponent<PlayerAttacking>();
    }

    void BulletType(string BulletTypes)
    {
        if (BulletTypes == "NormalBullet")
        {
            attacker.shootingObj = NormalBullet;
        }
        else if (BulletTypes == "ExplosiveBullet")
        {
            attacker.shootingObj = ExplosiveBullet;
            Invoke("SetBulletToNormal", 10);
        }
        else
        {
            Debug.LogError("ERROR! More than one bullet selected");
        }
    }
    private void SetBulletToNormal()
    {
        attacker.shootingObj = NormalBullet;
    }

    public void SetBulletType(string BulletName)
    {
        if (BulletName == "NormalBullet")
        {
            BulletType("NormalBullet");
        }
        else if (BulletName == "ExplosiveBullet")
        {
            BulletType("ExplosiveBullet");
        }
    }
}
