using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void UpdateHealthBar(float health)
    {

        slider.value = health/100f;
    }
}
