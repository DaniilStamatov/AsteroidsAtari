using Assets.Scripts.Enemies;
using Assets.Scripts.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    [CreateAssetMenu]

    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private EnemyInitConfig _initConfig = new EnemyInitConfig();
        private Scene _scene;

        public Enemy Get(EnemyType type)
        {
            var config = GetConfig(type);
            Enemy instance = CreateGameObjectInstance(config.Prefab);
            instance.OriginFectory = this;
            instance.Initialize(config.Speed, config.Damage, config.Reward, config.Health, type);
            return instance;
        }

        public EnemyConfig GetConfig(EnemyType type)
        {
            return _initConfig.GetConfig(type);
        }

        public void Reclaim(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        protected T CreateGameObjectInstance<T>(T prefab) where T : MonoBehaviour
        {
            if (!_scene.isLoaded)
            {
                if (Application.isEditor)
                {
                    _scene = SceneManager.GetSceneByName(name);
                    if (!_scene.isLoaded)
                    {
                        _scene = SceneManager.CreateScene(name);
                    }
                }
                else
                {
                    _scene = SceneManager.CreateScene(name);
                }
            }

      
            T instance = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(instance.gameObject, _scene);
            return instance;
        }
    }
}
