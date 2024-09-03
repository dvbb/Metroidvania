using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class BlackHoleController : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList = new List<KeyCode>();

    // circle Lerp info
    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;

    // attack info
    private float amountOfAttacks;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackeTimer;

    // time window
    private float timer;

    private bool canGrow = true;
    private bool canShrink;
    private bool isCloneAttackRelease;
    private bool canCreateHotKey = true;
    private bool canDisappear = true;

    public bool playerCanExit { get; private set; }

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKeyList = new List<GameObject>();

    public void SetupBlackhole(float maxSize, float growSpeed, float shrinkSpeed, float amountOfAttacks, float duration)
    {
        InitHotKey();
        this.maxSize = maxSize;
        this.growSpeed = growSpeed;
        this.shrinkSpeed = shrinkSpeed;
        this.amountOfAttacks = amountOfAttacks;
        timer = duration;
    }

    private void InitHotKey()
    {
        keyCodeList.Add(KeyCode.Q);
        keyCodeList.Add(KeyCode.W);
        keyCodeList.Add(KeyCode.E);
        keyCodeList.Add(KeyCode.A);
        keyCodeList.Add(KeyCode.S);
        keyCodeList.Add(KeyCode.D);
    }

    private void Update()
    {
        cloneAttackeTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = Mathf.Infinity;
            if (targets.Count > 0)
                ReleaseCloneAttack();
            else
                FinishBlackholeAbility();
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        GrowOrShrinkLogic();
    }

    private void GrowOrShrinkLogic()
    {
        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
        if (canShrink && !canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
                Destroy(gameObject);
        }
    }

    private void ReleaseCloneAttack()
    {
        if (targets.Count <= 0)
            return;

        DestroyHotKeys();
        isCloneAttackRelease = true;
        canCreateHotKey = false;

        if (canDisappear)
        {
            canDisappear = false;
            PlayerManager.Instance.player.MakeTransparent(true);
        }
    }

    private void CloneAttackLogic()
    {
        if (targets.Count <= 0)
            return;
        if (cloneAttackeTimer < 0 && isCloneAttackRelease && amountOfAttacks > 0)
        {
            cloneAttackeTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset = Random.Range(0, 100) > 50 ? 1 : -1;
            Debug.Log(randomIndex);
            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));
            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
                Invoke("FinishBlackholeAbility", .5f);
            }
        }
    }

    private void FinishBlackholeAbility()
    {
        DestroyHotKeys();
        playerCanExit = true;
        canShrink = true;
        canGrow = false;
        isCloneAttackRelease = false;
        PlayerManager.Instance.player.MakeTransparent(false);
        // PlayerManager.Instance.player.ExitBlackHoleAbility();
    }

    private void DestroyHotKeys()
    {
        if (createdHotKeyList.Count <= 0)
            return;
        foreach (var item in createdHotKeyList)
        {
            Destroy(item);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Enemy>()?.FreezeTime(false);
    }

    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0)
        {
            Debug.LogWarning("not enough hotkeys in 'List<KeyCode> keyCodeList'");
            return;
        }
        if (!canCreateHotKey)
            return;

        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKeyList.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];
        keyCodeList.Remove(choosenKey);

        BlackholeHotKeyController hotKeyController = newHotKey.GetComponent<BlackholeHotKeyController>();

        hotKeyController.SetupHotKey(choosenKey, collision.transform, this);
    }

    public void AddEnemyToList(Transform enemyTransform) => targets.Add(enemyTransform);
}
