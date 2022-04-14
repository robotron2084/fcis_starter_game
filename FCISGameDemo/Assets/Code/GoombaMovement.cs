using com.enemyhideout.fsm;
using GameDemo;
using UnityEngine;

namespace Code
{
    /// <summary>
    /// Moves a unit in one direction until it hits something, then it turns around.
    /// </summary>
    public class GoombaMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10.0f;
        [SerializeField] private Transform visualRoot;
        
        private Rigidbody2D _rigidBody;
        private Vector2 _forceThisFrame;
        private float direction = 1.0f;
        

        private EnemyFsm<MoveStates> _fsm;
        void Start()
        {
            _fsm = new EnemyFsm<MoveStates>(this);
            _rigidBody = GetComponent<Rigidbody2D>();
        }
        
        void OnGround_Update()
        {
            _forceThisFrame = new Vector2(speed * direction, 0.0f);
        }

        void FixedUpdate()
        {
            UnitCore.MoveUnit(_rigidBody, _forceThisFrame);
            UnitCore.OrientUnitBasedOnXVelocity(visualRoot, _rigidBody);
        }

        public void OnNoseCollide(Collider2D col, Collision2D collision2D)
        {
            bool hitSides = CollisionCore.HitSides(col, collision2D);
            direction = UnitCore.FlipDirection(hitSides, direction);
        }
    }
}