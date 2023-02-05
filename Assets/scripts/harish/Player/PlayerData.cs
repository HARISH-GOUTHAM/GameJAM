using System;
using Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace harish.Player
{


    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance;
        public float health = 50;
        public float mana = 100;
        public InputManagerScriptable inputManger;
        public Transform checkpoint;

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

        private void Update()
        {
            if (health > 100)
            {
                health = 100;
            }
            if(mana> 100)
            {
                mana = 100;
            }
            if(health<=0)
            {
               transform.position = checkpoint.position;
                health = 100;
                mana = 100;
            }
        }
        
    }
}
        
