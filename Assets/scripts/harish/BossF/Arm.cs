using System;
using UnityEngine;

namespace harish.BossF
{
    public class Arm : MonoBehaviour
    {
        public health_bar_Script health_bar;
        public float health = 40;

        private bool isDead = false;

        private void Start()
        {
            health_bar.set_max_health(health);
        }
        private void Update()
        {
            health_bar.set_health(health);
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