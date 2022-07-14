using Assets.Scripts.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Objects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _force;
        [SerializeField] private UnityEvent _invoke;

        private Vector2 _lastFramePosition;
        private float _flewDistance;
        private float _screenWidth;
        private PoolItem _poolItem;


        private void Start()
        {
            _rigidbody= GetComponent<Rigidbody2D>();
            _poolItem = GetComponent<PoolItem>();
            _lastFramePosition = transform.position;
            _flewDistance = 0;

            _screenWidth = Camera.main.pixelWidth * 2;
        }

        public void Fire()
        {
            _rigidbody.AddForce(transform.right * _force, ForceMode2D.Impulse);
        }

        private void Update()
        {
            float deltaMove = Vector2.Distance(transform.position, _lastFramePosition);
            if (deltaMove < Camera.main.pixelHeight)
                _flewDistance += deltaMove;

            if (_flewDistance > _screenWidth)
                _invoke?.Invoke();
                
        }

    }
}
