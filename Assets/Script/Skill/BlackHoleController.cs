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

    private bool canGrow = true;
    private bool canShrink;
    private bool isCloneAttackRelease;
    private bool canCreateHotKey = true;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createdHotKeyList = new List<GameObject>();

    public void SetupBlackhole(float maxSize, float growSpeed, float shrinkSpeed, float amountOfAttacks)
    {
        InitHotKey();
        this.maxSize = maxSize;
        this.growSpeed = growSpeed;
        this.shrinkSpeed = shrinkSpeed;
        this.amountOfAttacks = amountOfAttacks;
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
        DestroyHotKeys();
        isCloneAttackRelease = true;
        canCreateHotKey = false;
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackeTimer < 0 && isCloneAttackRelease)
        {
            cloneAttackeTimer = cloneAttackCooldown;

            int randomIndex = Random.Range(0, targets.Count);

            float xOffset = Random.Range(0, 100) > 50 ? 2 : -2;

            SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));
            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
                canShrink = true;
                canGrow = false;
                isCloneAttackRelease = false;
            }
        }
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
