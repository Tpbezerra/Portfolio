using UnityEngine;

[RequireComponent(typeof(PlayerAttacking))]
public class PlayerBulletInventory : MonoBehaviour {

    [SerializeField] Projectile NormalBullet;
    [SerializeField] Projectile ExplosiveBullet;

    PlayerAttacking attacker;

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

    void Start()
    {
        attacker = GetComponent<PlayerAttacking>();
    }

    void BulletType(string bulletName)
    {
        if (bulletName == "NormalBullet")
        {
            attacker.SetShootingObject(NormalBullet);
        }
        else if (bulletName == "ExplosiveBullet")
        {
            attacker.SetShootingObject(ExplosiveBullet);
            Invoke(nameof(ResetBullet), 10);
        }
        else
        {
            Debug.LogError("Invalid bullet name!");
        }
    }

    void ResetBullet()
    {
        attacker.SetShootingObject(NormalBullet);
    }
}
