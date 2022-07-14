using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private float _asteroidSpawnSpeed;
        [SerializeField] private float _alienSpawnSpeed;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _offset;
        [SerializeField] private Ship _player;
        private Enemy[] _enemies = new Enemy[1];
        private Enemy _enemy;
        private EnemyCollection _collection = new EnemyCollection();

        private void Awake()
        {
            StartCoroutine(SpawnAlienCoroutine());
            StartSpawn();
        }

        private void Update()
        {
            _enemy.Collection.GameUpdate();
            
            if (_enemy.Collection.IsEmpty)
            {
                StartSpawn();
                Debug.Log("Empty");
            }
        }

        public void Spawn(EnemyType type)
        {
            _enemy = _enemyFactory.Get(type);

            _enemy.transform.position = GetRandomPosition();
            _enemy.SetDirection(GetRandomPosition());
            _enemy.Collection.AddEnemy(_enemy);
        }

        [ContextMenu("Spawn")]
        private void StartSpawn()
        {
            for (var j = 0; j < _enemies.Length; j++)
            {
                Spawn(EnemyType.AsteroidLarge);
            }
            _enemies = new Enemy[_enemies.Length + 1];
            Debug.Log($"{_enemies.Length}");
        }

        private void SpawnAlien()
        {
            _enemy = _enemyFactory.Get(EnemyType.Alien);
            _enemy.transform.position = GetRandomPosition(_offset);
            Vector3 viewPos = _camera.WorldToViewportPoint(_enemy.transform.position);
            if (viewPos.x > 0.5F)
                _enemy.SetDirection(new Vector2(transform.position.x - 1, transform.position.y));
            else
                _enemy.SetDirection(new Vector2(transform.position.x + 1, transform.position.y));
        }

        private IEnumerator SpawnAlienCoroutine()
        {
            while (true)
            {
                SpawnAlien();
                yield return new WaitForSeconds(_alienSpawnSpeed);
            }
        }

        private Vector3 GetRandomPosition() => _camera.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0, 2), 1f));
        private Vector3 GetRandomPosition(float offset) => _camera.ViewportToWorldPoint(new Vector3(Random.Range(0, 2), Random.Range(0f + offset, 1f - offset), 1f));
    }
}

