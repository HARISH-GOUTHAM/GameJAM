using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using harish.Player;

public class player_health : MonoBehaviour
{
    public health_bar_Script health_bar;
    // Start is called before the first frame update
    void Start()
    {
        health_bar.set_max_health(PlayerData.instance.health);
    }

    // Update is called once per frame
    void Update()
    {
        health_bar.set_health(PlayerData.instance.health);
    }
}
