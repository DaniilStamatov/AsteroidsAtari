using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Factory
{
    [Serializable]
    public class EnemyInitConfig
    {
        [SerializeField] private EnemyConfig _large, _medium, _small, _alien;

        public EnemyConfig GetConfig(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.AsteroidLarge:
                    return _large;
                case EnemyType.AsteroidMedium:
                    return _medium;
                case EnemyType.AsteroidSmall:
                    return _small;
                case EnemyType.Alien:
                    return _alien;
            }
            Debug.LogError($"No config found :{type}");
            return _large;
        }
    }
}
