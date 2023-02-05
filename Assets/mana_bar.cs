using harish.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mana_bar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    private void Start()
    {
        slider.maxValue = PlayerData.instance.mana;
        slider.value = PlayerData.instance.mana;
    }
    private void Update()
    {
        slider.value = PlayerData.instance.mana;
    }
}
