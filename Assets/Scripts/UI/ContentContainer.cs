using Assets.Scripts.Game;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ContentContainer : MonoBehaviour
    {
        [SerializeField] private Text _health;
        [SerializeField] private Text _score;
        [SerializeField] private GameObject _menu;
        [SerializeField] private Button _continueButton;
        private Scorer _scorer;
        private Ship _ship;


        private void Start()
        {
            _scorer = GetComponent<Scorer>();
            _ship = FindObjectOfType<Ship>();
            //_ship.HealthComponent.OnChange += UpdateHealth;
            _scorer.OnScoreChanged += UpdateScore;
            _ship._loadMenu += LoadMenu;
            Time.timeScale = 0;
            UpdateScore();
            _menu.SetActive(true);
            _continueButton.interactable = false;

            //UpdateHealth();
        }
        public void UpdateScore()
        {
            _score.text = _scorer.Current.ToString();
        }


        public void Continue()
        {
            _menu.SetActive(false);
            Time.timeScale = 1;
        }

        public void LoadMenu()
        {
            _menu.SetActive(true);
            _continueButton.interactable = true;
            Time.timeScale = 0;
        }

        public void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            Time.timeScale = 1;
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void BeginNewGame()
        {
            _menu.SetActive(false);
            Time.timeScale = 1;
        }

        public void UpdateHealth()
        {
            _health.text = _ship.Health.ToString();
        }

        private void OnDestroy()
        {
            _scorer.OnScoreChanged -= UpdateScore;
        }
    }
}
