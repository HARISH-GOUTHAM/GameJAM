using System;
using harish.Player;
using UnityEngine;

namespace harish.Boss
{
    public class ChainSaw : MonoBehaviour
    {
        [SerializeField] float damage = 10;
        [SerializeField] float attackDelay = 2;
        private BoxCollider coll;
        private void Start()
        {
            coll = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            AttackPlayer();
        }

        private float attackTime;
        void AttackPlayer()
        {
            if (!(Time.time - attackTime > attackDelay))
            {
                return;
            }
            
            if(coll.bounds.Contains(PlayerData.instance.transform.position))
            {
                PlayerData.instance.health -= damage;
                attackTime = Time.time;
            }
        }
    }
}