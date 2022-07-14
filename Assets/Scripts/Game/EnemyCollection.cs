using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class EnemyCollection
    {
        private List<Enemy> _enemies = new List<Enemy>();
        public bool IsEmpty => _enemies.Count <= 0;

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void GameUpdate()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (!_enemies[i].GameUpdate())
                {
                    int lastIndex = _enemies.Count - 1;
                    _enemies[i] = _enemies[lastIndex];
                    _enemies.RemoveAt(lastIndex);
                    i -= 1;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].Recycle();
            }
            _enemies.Clear();
        }
    }
}
