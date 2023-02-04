using System;
using UnityEngine;

namespace harish.Boss
{
    public class Arm : MonoBehaviour
    {
        public float health = 40;

        private void Update()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
        }
        
    }
}