using com.enemyhideout.fsm;
using UnityEngine;
using UnityEngine.Events;

namespace Code
{
    public class UnitHealth : MonoBehaviour
    {
        [SerializeField] private int health;
        private int currentHealth;
        [SerializeField] private UnityEvent OnDeath;
        
        
        public enum HealthStates
        {
            Alive,
            Dead
        }

        private EnemyFsm<HealthStates> _fsm;

        private void Awake()
        {
            currentHealth = health;
            _fsm = new EnemyFsm<HealthStates>(this);
        }

        public void TakeDamage(int amount)
        {
            currentHealth = UnitCore.TakeDamage(currentHealth, amount, _fsm);
        }
    }
}