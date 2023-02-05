using System;
using UnityEngine;

namespace harish.BossF
{
    public class BossTrigger : MonoBehaviour
    {
        public BoxCollider coll;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Boss.instance.enabled = true;
                coll.enabled = true;
            }
            
        }
    }
}