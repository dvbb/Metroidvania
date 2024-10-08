﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{ 
    private Entity entity;
    private CharacterStats myStats;
    private RectTransform myTransform;
    private Slider slider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        myStats = GetComponentInParent<CharacterStats>();
        slider = GetComponentInChildren<Slider>();

        entity.OnFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }


    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.maxHealth.GetValue();
        slider.value = myStats.currentHealth;
    }

    /// <summary>
    /// when character flip, also flip UI
    /// </summary>
    private void FlipUI()
    {
        myTransform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        entity.OnFlipped -= FlipUI;
        myStats.onHealthChanged -= UpdateHealthUI;
    }
}
