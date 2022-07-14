using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private int _amount;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var go = collision.gameObject;
            if (go.CompareTag(_tag))
            {
                go.GetComponent<HealthComponent>().ModifyHealth(_amount);
            }
        }
    
    }
}
