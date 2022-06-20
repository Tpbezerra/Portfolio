using UnityEngine;

public class LivingEntity : MonoBehaviour {

    [SerializeField] GameObject shieldEffect;
    [SerializeField] GameObject deathEffect;

    [SerializeField] int health;
    [SerializeField] int shield;

    public int GetShield()
    {
        return shield;
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            if (damage > shield)
            {
                shield = 0;
                health -= damage - shield;
                if (shieldEffect != null)
                    shieldEffect.SetActive(false);
            }
            else
            {
                shield -= damage;
                if (shieldEffect != null)
                    shieldEffect.transform.localScale -= Vector3.one * (0.1f * damage) / 10;
            }
            
        }
        else
        {
            health -= damage;
            if (shieldEffect != null)
                shieldEffect.SetActive(false);
        }

        if (health <= 0)
            Die();
    }

    void Die()
    {
        if (deathEffect != null)
        {
            GameObject temp = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(temp, 5);
        }

        Destroy(gameObject);
    }
}
