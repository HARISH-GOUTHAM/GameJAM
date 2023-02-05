using System;
using UnityEngine;

namespace harish.BossF
{
    public class BossTrigger : MonoBehaviour
    {
        public BoxCollider coll;
        public GameObject fireParticles;
        public AudioSource theme;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Boss.instance.enabled = true;
                coll.enabled = true;
                fireParticles.SetActive(true);
                theme.Play();
            }
            
        }
    }
}