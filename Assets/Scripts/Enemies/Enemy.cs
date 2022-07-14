using Assets.Scripts.Components;
using Assets.Scripts.Game;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private ParticleSystem _particle;
        public Transform[] Points => _points;

        public EnemyFactory OriginFectory { get; set; }

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private EnemyType _type;
        public EnemyType Type => _type;
        private int _health;
        private float _speed;
        private int _reward;

        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent => _healthComponent;
        private EnemyCollection _collection = new EnemyCollection();
        public EnemyCollection Collection => _collection;

        private Scorer _scorer;


        private void Start()
        {
            _particle = GetComponent<ParticleSystem>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthComponent = GetComponent<HealthComponent>();
            _scorer  = FindObjectOfType<Scorer>();
            _healthComponent.SetHeath(_health);
            _healthComponent.OnDie += OnDie;

        }

        private void Update()
        {
            _rigidbody.velocity = _direction * _speed;
            _collection.GameUpdate();
        }

        public void Initialize(float speed, int damage, int reward, int health, EnemyType type)
        {
            _speed = speed;
            _health = health;
            _type = type;
            _reward = reward;
        }

        public bool GameUpdate()
        {
            if (_healthComponent.Health <= 0)
            {
                return false;
            }
            return true;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void OnDie()
        {
            switch (_type)
            {
                case EnemyType.AsteroidLarge:
                    GetNextAsteroid(EnemyType.AsteroidMedium);
                    break;
                case EnemyType.AsteroidMedium:
                    GetNextAsteroid(EnemyType.AsteroidSmall);
                    break;
                case EnemyType.AsteroidSmall:
                    OriginFectory.Reclaim(this);
                    _scorer.AddScore(_reward);
                    break;
                case EnemyType.Alien:
                    OriginFectory.Reclaim(this);
                    _scorer.AddScore(_reward);
                    break;
            }
        }

        private void GetNextAsteroid(EnemyType type)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                var enemy = OriginFectory.Get(type);
                enemy.gameObject.transform.position = _points[i].position;
                enemy.SetDirection(_points[i].position);
                _collection.AddEnemy(enemy);

            }

            Recycle();
            _scorer.AddScore(_reward);

        }

        public void Recycle()
        {
            OriginFectory.Reclaim(this);
        }

        private void OnDestroy()
        {
            _healthComponent.OnDie -= OnDie;
        }
    }
}


