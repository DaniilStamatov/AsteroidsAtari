using Assets.Scripts.Components;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class EnemyConfig
    {
        public Enemy Prefab;
        [Range(0f, 5f)]
        public float Speed;
        [Range(0, 3)]
        public int Health;
        [Range(1, 5)]
        public int Damage;
        [Range(20, 200)]
        public int Reward;

        public EnemyType Type;

        public EnemyConfig(float speed, int health, int damage, int reward, EnemyType type)
        {
            Speed = speed;
            Health = health;
            Damage = damage;
            Reward = reward;
            Type = type;
        }
    }

    [Serializable]
    public enum EnemyType
    {
        AsteroidLarge,
        AsteroidMedium,
        AsteroidSmall,
        Alien
    }
}