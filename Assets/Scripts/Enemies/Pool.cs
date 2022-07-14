using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Pool : MonoBehaviour
    {
        private readonly Dictionary<int, Queue<PoolItem>> _poolItems = new Dictionary<int, Queue<PoolItem>>();
        private readonly Dictionary<int, Queue<PoolItem>> _items = new Dictionary<int, Queue<PoolItem>>();



        private static Pool _instance;
        public  static Pool Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("###POOL###");
                    _instance = go.AddComponent<Pool>();
                }
                return _instance;
            }
        }


        public GameObject Get(GameObject go, Vector3 position)
        {
            var id = go.GetInstanceID();
            var queue = RequireQueue(id);

            if (queue.Count > 0)
            {
                var pooledItem = queue.Dequeue();
                pooledItem.transform.position = position;
                pooledItem.gameObject.SetActive(true);
                pooledItem.Restart();
                return pooledItem.gameObject;
            }

            var instance = Instantiate(go, position, Quaternion.identity, gameObject.transform);
            var poolItem = instance.GetComponent<PoolItem>();
            poolItem.Retain(id, this);

            return instance;
        }
        public void Release(int id, PoolItem poolItem)
        {
            var queue = RequireQueue(id);
            queue.Enqueue(poolItem);

            poolItem.gameObject.SetActive(false);
        }

        public Queue<PoolItem> RequireQueue(int id)
        {
            if (!_poolItems.TryGetValue(id, out var queue))
            {
                queue = new Queue<PoolItem>();
                _poolItems.Add(id, queue);
            }
            return queue;
        }
    }
}
