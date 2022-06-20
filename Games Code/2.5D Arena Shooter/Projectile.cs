using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] GameObject BulletExplosion;

    [SerializeField] bool explosive = false;

    [SerializeField] float ExplosiveBulletRadius = 2.5f;
    [SerializeField] float pushbackForce = 50;
    [SerializeField] float travelSpeed = 75;

    int damage;

    float charge;
    float distanceTraveled;

    GameObject owner;

    public void Initialize(GameObject owner, int damage, float charge)
    {
        this.owner = owner;
        this.damage = damage;
        this.charge = charge;
    }
	
	void FixedUpdate ()
    {
        float travelDistance = travelSpeed * charge * Time.fixedDeltaTime;
        distanceTraveled += travelDistance;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, travelDistance) && hit.transform.gameObject != owner)
            Collide(hit);

        transform.position += transform.forward * travelDistance;

        if (distanceTraveled > 100)
            Destroy(gameObject);
    }

    void Collide(RaycastHit hit)
    {
        Collider[] colliders = new Collider[] { hit.collider };
        Vector3 forceDirection = transform.forward;

        if (explosive)
            colliders = Physics.OverlapSphere(hit.point, ExplosiveBulletRadius, ~(1 << 0) | (1 << 0), QueryTriggerInteraction.Ignore);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == owner)
                continue;

            Rigidbody hitRb = collider.transform.GetComponent<Rigidbody>();
            if (hitRb == null)
                hitRb = collider.transform.parent.GetComponent<Rigidbody>();

            LivingEntity hitEntity = collider.GetComponent<LivingEntity>();
            if (hitEntity == null)
                hitEntity = collider.transform.parent.GetComponent<LivingEntity>();

            bool addForce = true;

            if (hitEntity != null)
            {
                hitEntity.TakeDamage(damage);

                addForce = hitEntity.GetShield() <= 0;
            }

            if (hitRb != null && addForce)
            {
                if (explosive)
                    forceDirection = (collider.transform.position - hit.point).normalized;

                hitRb.AddForce(forceDirection * (pushbackForce * charge), ForceMode.Impulse);
            }
        }

        if (BulletExplosion != null)
        {
            GameObject temp = Instantiate(BulletExplosion, hit.point, Quaternion.identity);
            ParticleSystem.MainModule main = temp.GetComponent<ParticleSystem>().main;
            main.startLifetimeMultiplier = ExplosiveBulletRadius / main.startSpeedMultiplier;
            Destroy(temp, 1);
        }

        Destroy(gameObject);
    }
}
