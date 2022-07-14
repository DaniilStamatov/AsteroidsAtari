using Assets.Scripts.Components;
using Assets.Scripts.Enemies;
using Assets.Scripts.Objects;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private float _mouseIntencity;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _fireTransform;
        [SerializeField] private Cooldown _fireCooldown;
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        public int Health => _health;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private Vector3 mousePosition;
        private HealthComponent _healthComponent;
        public HealthComponent HealthComponent => _healthComponent;

        public event Action _loadMenu;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.SetHeath(_health);
            _healthComponent.OnDie += OnDie;
        }

        private void Update()
        {
            mousePosition = Input.mousePosition;
            Vector3 lookPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            _direction = lookPosition - transform.position;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,0f, angle);
            LoadMenu();
            Fire();
            Move();
        }


        private void Fire()
        {
            if (Input.GetMouseButton(0) && _fireCooldown.IsReady)
            {
                var bullet = Pool.Instance.Get(_bulletPrefab, transform.position);
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponent<Bullet>().Fire();
                _fireCooldown.Reset();
            }
        }

        private void LoadMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _loadMenu?.Invoke();
            }
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void Move()
        {
            if (Input.GetMouseButton(1))
            {
                _rigidbody.AddForce(_direction * _speed, ForceMode2D.Impulse);
            }
        }

        private void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
