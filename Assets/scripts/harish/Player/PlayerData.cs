using System;
using UnityEngine;

namespace harish.Player
{
   
    
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance;

        

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