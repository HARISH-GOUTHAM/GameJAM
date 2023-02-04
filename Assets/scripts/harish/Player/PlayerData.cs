using System;
using Input;
using UnityEngine;

namespace harish.Player
{


    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance;
        public float health = 50;
        public player_healthbar healthbar;
        public InputManagerScriptable inputManger;
        private void Start()
        {
            healthbar.set_max_health(health);  
        }
        private void Update()
        {
            healthbar.set_health(health);
        }

        private void Awake()
        {

            if (instance == null)
            {
                instance = this;

            }
            else
            {
                Destroy(this);
            }
        }
    }
}
        
