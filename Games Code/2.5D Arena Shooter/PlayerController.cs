using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : LivingEntity {

    string Name;
    [SerializeField] Text nameTag;

    [SerializeField] GameObject groundPoundExplosion;
    [SerializeField] Color characterColor;

    [SerializeField] LayerMask groundMask;

    [SerializeField] float maxSpeed = 10;
    [SerializeField] float accelerationTime = 0.2f;
    [SerializeField] float decelerationTime = 0.2f;
    [SerializeField] float jumpHeight = 6;
    [Range(0, 1)] [SerializeField] float airControlPercent = 0.75f;
    [SerializeField] float groundPoundCooldown = 2;
    [SerializeField] float groundPoundRadius = 5;
    [SerializeField] float groundPoundForce = 30;
    [SerializeField] float dashRange = 7.5f;
    [SerializeField] float dashSpeed = 50;
    [SerializeField] float dashCooldown = 1.5f;

    float gravity = 9.82f * 2;
    float input;
    float groundPoundTime;
    float dashCooldownTime;

    int currentJump;

    bool jump;
    bool groundPound;
    bool isDashing;

    bool CanMove { get; set; }
    bool IsGrounded
    {
        get
        {
            if (Physics.SphereCast(transform.position, transform.localScale.x / 2 - 0.01f, -Vector3.up, out _, 0.21f, groundMask, QueryTriggerInteraction.Ignore))
                return true;

            return false;
        }
    }

    Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        CanMove = true;
        SwitchColor();

        if (GetComponent<Collider>())
        {
            PhysicMaterial mat = new PhysicMaterial
            {
                bounciness = 0,
                bounceCombine = PhysicMaterialCombine.Minimum,
                dynamicFriction = 0,
                staticFriction = 0,
                frictionCombine = PhysicMaterialCombine.Minimum
            };

            GetComponent<Collider>().material = mat;
        }

        transform.GetComponent<Renderer>().material.color = characterColor;
        if (transform.GetComponent<TrailRenderer>())
        {
            transform.GetComponent<TrailRenderer>().startColor = new Color(characterColor.r, characterColor.g, characterColor.b, 255);
            transform.GetComponent<TrailRenderer>().endColor = new Color(characterColor.r, characterColor.g, characterColor.b, 255);
        }
        Name = PlayerPrefs.GetString("PlayerName");
    }

    void SwitchColor()
    {
        switch (PlayerPrefs.GetInt("PlayerColorIndex"))
        {
            case 0:
                characterColor = Color.blue;
                break;
            case 1:
                characterColor = Color.red;
                break;
            case 2:
                characterColor = Color.green;
                break;
            case 3:
                characterColor = Color.yellow;
                break;
            case 4:
                characterColor = Color.cyan;
                break;
            case 5:
                characterColor = Color.black;
                break;
            case 6:
                characterColor = Color.white;
                break;
        }
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W) && currentJump < 2 && CanMove)
            jump = true;

        if (IsGrounded)
        {
            currentJump = 0;
            if (jump)
                currentJump = 1;
        }
        else
        {
            if (jump)
                currentJump = 2;

            if (Input.GetKeyDown(KeyCode.S) && !groundPound && CanMove)
                groundPound = true;
        }

        Dash();
    }

    void FixedUpdate () {
        Movement();

        if (nameTag != null)
        {
            nameTag.text = Name;
            nameTag.rectTransform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
        }
    }

    void Movement()
    {
        if (groundPoundTime > 0)
            groundPoundTime -= Time.fixedDeltaTime;

        Mathf.Clamp(groundPoundTime, 0, float.MaxValue);

        float smoothTime = 1;

        if (IsGrounded)
        {
            if (Physics.SphereCast(transform.position, (transform.localScale.x / 2) - 0.01f, -Vector3.up, out RaycastHit hit, 0.21f, groundMask, QueryTriggerInteraction.Ignore) && rb.velocity.y <= 0)
            {
                Vector3 direction = new Vector3(hit.point.x, 0, hit.point.z) - new Vector3(transform.position.x, 0, transform.position.z);

                float mag = direction.magnitude;
                float difference = Mathf.Sqrt(Mathf.Pow(transform.localScale.x / 2, 2) - Mathf.Pow(mag, 2));
                float fullDistance = transform.position.y - hit.point.y;

                if (fullDistance - difference > 0.0002f)
                    rb.velocity = new Vector3(rb.velocity.x, -(fullDistance - difference) / Time.fixedDeltaTime, rb.velocity.z);
            }

            if (groundPound)
            {
                if (groundPoundTime <= 0)
                    Explode();
                else
                    rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2 * gravity * jumpHeight * 0.75f), rb.velocity.z);

                groundPound = false;
            }
        }
        else
        {
            smoothTime = airControlPercent;

            if (rb.velocity.y > 0)
                rb.velocity -= new Vector3(0, gravity * Time.fixedDeltaTime, 0);
            else
                rb.velocity -= new Vector3(0, gravity * 2 * Time.fixedDeltaTime, 0);
        }

        if (CanMove)
        {
            float targetSpeed = maxSpeed * Mathf.Abs(input);
            float acceleration = ((accelerationTime > 0) ? maxSpeed / accelerationTime : float.MaxValue) * smoothTime;
            float deceleration = ((decelerationTime > 0) ? maxSpeed / decelerationTime : float.MaxValue) * smoothTime;

            if ((input > 0 && rb.velocity.x < targetSpeed) || (input < 0 && rb.velocity.x > -targetSpeed))
            {
                rb.velocity += Vector3.right * (input * Mathf.Min(acceleration * Time.fixedDeltaTime, Mathf.Clamp(targetSpeed - Mathf.Abs(rb.velocity.x), 0, targetSpeed)));
                if ((input > 0 && rb.velocity.x < 0) || (input < 0 && rb.velocity.x > 0))
                    rb.velocity += Vector3.right * (input * Mathf.Min(deceleration * Time.fixedDeltaTime, Mathf.Abs(rb.velocity.x)));
            }

            if (Mathf.Abs(rb.velocity.x) > targetSpeed)
                rb.velocity -= Vector3.right * (rb.velocity.x / Mathf.Abs(rb.velocity.x) * Mathf.Min(deceleration * Time.fixedDeltaTime, Mathf.Abs(rb.velocity.x)));
        }

        if (jump)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(2 * gravity * jumpHeight), rb.velocity.z);
            jump = false;
        }
        if (groundPound)
            rb.velocity = new Vector3(rb.velocity.x, -60, rb.velocity.z);

        rb.drag = 0;
        if (Mathf.Approximately(rb.velocity.sqrMagnitude, 0))
            rb.drag = 999;

        if (transform.position.y < -18)
            transform.position = new Vector3(transform.position.x, 18, transform.position.z);

        if (transform.position.x > 22)
            transform.position = new Vector3(-21, transform.position.y, transform.position.z);
        else if (transform.position.x < -22)
            transform.position = new Vector3(21, transform.position.y, transform.position.z);
    }

    void Dash()
    {
        if (dashCooldownTime > 0)
            dashCooldownTime -= Time.fixedDeltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !Mathf.Approximately(input, 0) && dashCooldownTime <= 0 && !isDashing)
        {
            dashCooldownTime = dashCooldown;
            StartCoroutine(Dashing(Vector3.right * input));
        }
    }
    
    void Explode()
    {
        GameObject temp = Instantiate(groundPoundExplosion, transform.position, Quaternion.identity);
        ParticleSystem.MainModule main = temp.GetComponent<ParticleSystem>().main;
        float tempLifetime = groundPoundRadius / main.startSpeedMultiplier;
        main.startLifetimeMultiplier = tempLifetime;
        Destroy(temp, 1);

        Collider[] colliders = Physics.OverlapSphere(transform.position, groundPoundRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Rigidbody>())
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized;
                collider.GetComponent<Rigidbody>().AddForce(direction * groundPoundForce, ForceMode.VelocityChange);
            }
        }

        groundPoundTime = groundPoundCooldown;
    }

    IEnumerator Dashing(Vector3 directionToDash)
    {
        isDashing = true;
        CanMove = false;

        rb.velocity = Vector3.zero;

        float originalGravity = gravity;
        gravity = 0;

        Vector3 startPosition = rb.position;
        Vector3 endPosition = rb.position + (directionToDash * dashRange);

        float range = dashRange;

        if (Physics.SphereCast(rb.position, transform.localScale.x / 2, directionToDash, out RaycastHit hit, dashRange))
        {
            range = hit.distance;
            endPosition = rb.position + (directionToDash * range);
        }

        float dashTime = range / dashSpeed;
        float timer = 0;
        float lerpValue = 0;

        while (lerpValue < 1)
        {
            lerpValue = Mathf.Clamp01(Mathf.InverseLerp(0, dashTime, timer));
            rb.position = Vector3.Lerp(startPosition, endPosition, lerpValue);

            timer += Time.deltaTime;

            yield return null;
        }

        isDashing = false;
        CanMove = true;

        rb.velocity = directionToDash * (dashSpeed / 2);

        gravity = originalGravity;
    }
}
