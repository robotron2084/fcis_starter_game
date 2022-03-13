using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code
{
    public class ColliderProxy2d : MonoBehaviour
    {
        public UnityEvent<Collider2D, Collision2D> OnEnter;
        public UnityEvent<Collider2D, Collision2D> OnExit;
        public UnityEvent<Collider2D, Collision2D> OnStay;
                
        protected Collider2D _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnEnter.Invoke(_collider, other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnStay.Invoke(_collider, other);
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            OnExit.Invoke(_collider, other);
        }
    }
}