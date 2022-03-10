using UnityEngine;

namespace Code
{
    public class UnitCore
    {
        public static bool MoveIfKeyPressed(KeyCode keyCode, Rigidbody2D rBod, float force, bool hasMoved)
        {
            if (hasMoved)
            {
                return true;
            }
            if (Input.GetKey(keyCode))
            {
                rBod.AddForce(new Vector2(force, 0.0f));
                return true;
            }

            return false;
        }

        public static void OrientUnitBasedOnXVelocity(Transform transform, Rigidbody2D rBod)
        {
            Vector3 scale = transform.localScale;
            float x = Mathf.Abs(scale.x);
            if (rBod.velocity.x > 0.0001 || rBod.velocity.x < -0.0001)
            {
                scale.x = (rBod.velocity.x > 0) ? x : -x;
                transform.localScale = scale;
            }
        }
    }
}