using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        private int _health;
        public int Health=> _health;
        public event Action OnChange;
        public event Action OnDie;

        public void ModifyHealth(int amount)
        {
            _health += amount;
            if (amount < 0)
            {
                OnChange?.Invoke();
            }
            if (_health <= 0)
            {
                OnDie?.Invoke();
            }
        }

        public void SetHeath(int health)
        {
            _health = health;
        }

    }
}
