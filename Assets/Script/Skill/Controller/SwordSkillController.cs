using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 12;
    private Rigidbody2D rb;
    private CircleCollider2D cd;

    private bool canRotate = true;
    private bool isReturning;
    private float freezeTime;

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;

    [Header("Bounce info")]
    [SerializeField] private float bounceSpeed;
    private bool isBouncing = true;
    private int amountOfBounce = 4;
    private List<Transform> enemyTargets = new List<Transform>();
    private int targetIndex;

    [Header("Spin info")]
    private float maxTravelDistance;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSpinning;

    private float hitTimer;
    private float hitCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void SetUp(Vector2 vector2, float gravityScale, float freezeTime)
    {
        rb.velocity = vector2;
        rb.gravityScale = gravityScale;
        this.freezeTime = freezeTime;
        Invoke("DestroyMe", 10);
    }

    private void DestroyMe() => Destroy(gameObject);

    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerManager.Instance.player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, PlayerManager.Instance.player.transform.position) < 1)
                PlayerManager.Instance.player.CatchTheSword();
        }

        BounceLogic();
        SpinLogic();
    }

    private void SpinLogic()
    {
        if (isSpinning)
        {
            if (Vector2.Distance(PlayerManager.Instance.player.transform.position, transform.position) > maxTravelDistance && !wasStopped)
            {
                StopWhenSpinning();
            }
            if (wasStopped)
            {
                spinTimer -= Time.deltaTime;
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSpinning = false;
                }

                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                    hitTimer = hitCooldown;

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        SwordSkillDamage(hit.GetComponent<Enemy>());
                }
            }
        }
    }

    private void StopWhenSpinning()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTargets.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTargets[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTargets[targetIndex].position) < .1f)
            {
                SwordSkillDamage(enemyTargets[targetIndex].GetComponent<Enemy>());
                targetIndex++;
                amountOfBounce--;
                if (amountOfBounce <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }
                if (targetIndex >= enemyTargets.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning)
            return;


        if (collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            SwordSkillDamage(enemy);
        }

        SetupBounceTarget(collision);

        StuckInto(collision);
    }

    private void SwordSkillDamage(Enemy enemy)
    {
        enemy.Damage();
        enemy.StartCoroutine("FreezeTimerFor", freezeTime);
    }

    private void SetupBounceTarget(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    enemyTargets.Add(hit.transform);
                }
            }
        }
    }

    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<Enemy>() != null)
        {
            pierceAmount--;
            return;
        }

        if (isBouncing && enemyTargets.Count > 0)
            return;
        if (isSpinning)
        {
            StopWhenSpinning();
            return;
        }

        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;



        transform.parent = collision.transform;
    }

    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }
    public void SetupRegular()
    {
        isBouncing = false;
    }

    public void SetupBounce(bool isBouncing, int amountOfBounce)
    {
        isSpinning = false;
        this.isBouncing = isBouncing;
        this.amountOfBounce = amountOfBounce;
    }

    public void SetupPierce(int pierceAmount)
    {
        isBouncing = false;
        this.pierceAmount = pierceAmount;
    }

    public void SetupSpin(float maxTravelDistance, float spinDuration, float hitCooldown)
    {
        isBouncing = false;
        isSpinning = true;
        this.maxTravelDistance = maxTravelDistance;
        this.spinDuration = spinDuration;
        this.hitCooldown = hitCooldown;
    }
}
