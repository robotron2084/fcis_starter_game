using System;
using com.enemyhideout.fsm;
using UnityEngine;

namespace Code
{
    public class UnitCore
    {
        public static bool MoveIfKeyPressed(
            KeyCode keyCode, 
            Rigidbody2D rBod, 
            Vector2 forceToApply, 
            bool hasMoved, 
            ref Vector2 force)
        {
            if (hasMoved)
            {
                return true;
            }
            if (Input.GetKey(keyCode))
            {
                force = forceToApply;
                MoveUnit(rBod, force);
                return true;
            }
            return false;
        }

        public static void MoveUnit(Rigidbody2D rBod, Vector2 force)
        {
            rBod.AddForce(force);
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

        public static void ChangeStateIfKeyPressed<T>(KeyCode keyCode, EnemyFsm<T> fsm, T newState) where T : Enum
        {
            if(Input.GetKey(keyCode))
            {
                fsm.ChangeState(newState);
            }
        }

        public static void ChangeStateIfTrue<T>(bool condition, EnemyFsm<T> fsm, T newState) where T : Enum
        {
            if (condition)
            {
                fsm.ChangeState(newState);
            }
        }

        public static int TakeDamage(int unitHealth, int amount, EnemyFsm<UnitHealth.HealthStates> fsm)
        {
            if (unitHealth == 0)
            {
                return unitHealth;
            }
            unitHealth -= amount;
            unitHealth = Math.Min(0, unitHealth);
            if (unitHealth == 0)
            {
                fsm.ChangeState(UnitHealth.HealthStates.Dead);
            }
            return unitHealth;
        }

        public static float FlipDirection(bool hitSides, float direction)
        {
            return hitSides ? -direction : direction;
        }
    }
}