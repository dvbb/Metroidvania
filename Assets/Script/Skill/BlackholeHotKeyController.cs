using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackholeHotKeyController : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private BlackHoleController blackHole;

    public void SetupHotKey(KeyCode newHotKey, Transform enemy, BlackHoleController controller)
    {
        sr = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = enemy;
        blackHole = controller;

        myHotKey = newHotKey;
        myText.text = newHotKey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(myEnemy);
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
