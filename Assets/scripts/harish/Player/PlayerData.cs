using System;
using Input;
using UnityEngine;

namespace harish.Player
{
   
    
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance;
        public float health = 50;
        public InputManagerScriptable inputManger;

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