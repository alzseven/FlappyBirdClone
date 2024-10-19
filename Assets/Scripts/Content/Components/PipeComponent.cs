using System;
using UnityEngine;

namespace Content.Components
{
    public class PipeComponent : MonoBehaviour
    {   
        public event Action OnCollideWithBird;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                OnCollideWithBird?.Invoke();
            }
        }
    }
}