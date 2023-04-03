using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public void UpdateStaminaBar(float stamina)
    {

        slider.value = stamina/100f;
    }
}
