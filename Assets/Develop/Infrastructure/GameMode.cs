using Develop.Characters;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Develop.Infrastructure
{
    public class GameMode
    {
        private readonly Character _player;
        private readonly Collider2D _levelBounds;
        private readonly KeyCode _restartKey = KeyCode.R;

        private MonoBehaviour _coroutineRunner;

        private bool _isRestartProcess;

        public GameMode(Character player, Collider2D levelBounds, MonoBehaviour coroutineRunner)
        {
            _player = player;
            _levelBounds = levelBounds;
            _coroutineRunner = coroutineRunner;
        }

        public void Update(float deltaTime)
        {
            if (_isRestartProcess)
                return;

            if (_player.IsDied || _levelBounds.bounds.Contains(_player.Transform.position) == false)
            {
                _isRestartProcess = true;

                _coroutineRunner.StartCoroutine(RestartProcess());
            }
        }

        private IEnumerator RestartProcess()
        {
            if (_player.IsDied == false)
                _player.Kill();

            Debug.Log("Вы проиграли!");
            Debug.Log($"Чтобы перезапустить уровень - нажмите на {_restartKey}");

            yield return new WaitUntil(() => Input.GetKeyDown(_restartKey));

            Scene currentScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(currentScene.name);
        }
    }
}