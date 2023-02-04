using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void set_max_health(float health)
    { 
        slider.maxValue = health;
        slider.value = health; ;
        fill.color = gradient.Evaluate(1f);

    }
    public void set_health(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
