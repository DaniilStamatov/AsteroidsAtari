using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class Scorer : MonoBehaviour
    {
        public event Action OnScoreChanged;

        private int _current;

        public int Current => _current;

        public int Max
        {
            get => PlayerPrefs.GetInt("max-score");
            private set => PlayerPrefs.GetInt("max-score", value);
        }

        public void AddScore(int value)
        {
            _current+= value;

            if (_current >= Max)
                Max = _current;

            OnScoreChanged?.Invoke();
        }


    }
}
