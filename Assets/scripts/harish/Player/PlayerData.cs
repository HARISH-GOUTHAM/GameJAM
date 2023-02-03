using System;
using Input;
using UnityEngine;

namespace harish.Player
{
   
    
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance;

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