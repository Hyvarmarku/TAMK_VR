using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class ExitPathManager : MonoBehaviour
    {

        public Transform Player;
        public List<Transform> SpawnPoints = new List<Transform>();
        public List<PathMover> Paths = new List<PathMover>();
        public List<FireExtinguisher> Extinguishers = new List<FireExtinguisher>();

        private int _currentPath = 0;

        void Start()
        {
            RequestSpawn();
        }

        public void RequestSpawn()
        {
            if(_currentPath < SpawnPoints.Count && SpawnPoints[_currentPath])
            {
                SpawnPlayer();
            }
            else
            {
                _currentPath = 0;
                SpawnPlayer();
                return;
            }

            _currentPath++;
        }

        private void SpawnPlayer()
        {
            Player.position = SpawnPoints[_currentPath].position;
            Paths[_currentPath].gameObject.SetActive(true);

            if (_currentPath > 0)
            {
                Paths[_currentPath - 1].gameObject.SetActive(false);
            }
        }
    }
}
