using Assets.Scripts.Components;
using Assets.Scripts.Objects;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Alien : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _cooldown;
        private Ship _target;

        private void Start()
        {
            _target = FindObjectOfType<Ship>();
            _cooldown = Random.Range(2, 5);
            StartCoroutine(Fire());
        }


        private void Update()
        {
            CalculateDirectionOfBullet();
        }
        private Vector3 CalculateDirectionOfBullet()
        {
            return _target.gameObject.transform.position - transform.position;
        }

        private IEnumerator Fire()
        {
            var bullet = Pool.Instance.Get(_bulletPrefab, transform.position);
            bullet.transform.TransformDirection(CalculateDirectionOfBullet());
            bullet.GetComponent<Bullet>().Fire();
           yield return new WaitForSeconds(2);
        }
    }
}
