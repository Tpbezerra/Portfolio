using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAttacking : MonoBehaviour
{
    public Projectile shootingObj;
    public Transform gun;
    public Transform muzzle;

    public float aimSpeed = 20;
    public float fireRate = 1.3f;
    public int damage = 20;

    float aimAngle;
    float currentCharge;
    float timeToAttack;

    Projectile spawnedProjectile;

    void Update()
    {
        Aming();
        Attack();
    }

    void Aming()
    {
        Plane aimPlane = new Plane(-Vector3.forward, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        Vector3 aimPos = transform.position + transform.right;
        if (aimPlane.Raycast(ray, out distance))
            aimPos = ray.GetPoint(distance);

        Vector2 aimDirection = new Vector2(aimPos.x - transform.position.x, aimPos.y - transform.position.y);

        aimAngle = Mathf.LerpAngle(aimAngle, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg, aimSpeed * Time.deltaTime);

        if (gun != null)
            gun.eulerAngles = Vector3.forward * aimAngle;
    }

    void Attack()
    {
        if (Time.time > timeToAttack && Input.GetKeyDown(KeyCode.Mouse0) && shootingObj != null)
            spawnedProjectile = Instantiate(shootingObj, muzzle.position, muzzle.rotation, muzzle);

        if (spawnedProjectile != null)
        {
            if (Input.GetKey(KeyCode.Mouse0) && currentCharge < 1)
            {
                currentCharge = Mathf.Clamp01(currentCharge + 0.4f * Time.deltaTime);
                if (spawnedProjectile.GetComponent<ParticleSystem>())
                {
                    ParticleSystem.MainModule main = spawnedProjectile.GetComponent<ParticleSystem>().main;
                    main.startLifetimeMultiplier += 1.5f * Time.deltaTime;
                    main.startSpeedMultiplier += 3f * Time.deltaTime;
                    spawnedProjectile.GetComponent<SphereCollider>().radius += 1.2f * Time.deltaTime;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (spawnedProjectile.GetComponent<ParticleSystem>())
                {
                    ParticleSystem.EmissionModule main = spawnedProjectile.GetComponent<ParticleSystem>().emission;
                    main.rateOverTimeMultiplier = 1000;
                }

                spawnedProjectile.transform.parent = null;
                spawnedProjectile.Initialize(gameObject, (int)(damage * currentCharge), currentCharge);
                currentCharge = 0;
                spawnedProjectile = null;

                timeToAttack = Time.time + 1 / fireRate;
            }
        }

    }
}
