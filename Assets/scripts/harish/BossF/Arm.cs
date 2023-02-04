using System;
using UnityEngine;

namespace harish.BossF
{
    public class Arm : MonoBehaviour
    {
        public float health = 40;

        private bool isDead = false;
        
        private void Update()
        {
            if (health <= 0 && !isDead)
            {
                Boss.instance.armDeadCount += 1;
                Destroy(gameObject);
                isDead = true;
            }
        }

        public void TakeDamage(float damage)
        {
            if(Boss.instance.state == BossState.Overheat)
                health -= damage;
        }
        
    }
}