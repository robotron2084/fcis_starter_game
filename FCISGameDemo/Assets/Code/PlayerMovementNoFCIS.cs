using System;
using UnityEngine;

namespace Code
{
    public class PlayerMovementNoFcis : MonoBehaviour
    {

        public Rigidbody2D rBod;
        public float force = 10.0f;
        
        private bool _onGround;
        private const int GroundLayer = 10;
        
        void Update()
        {
            if (Input.GetKey(KeyCode.Space) && _onGround)
            {
                rBod.AddForce(new Vector2(0.0f, force));
                _onGround = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == GroundLayer)
            {
                _onGround = true;
            }
        }
    }
}