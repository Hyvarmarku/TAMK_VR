using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class ExitPathManager : MonoBehaviour
    {

        public Transform Player;
        public Navigation Navigation;
        public List<Transform> SpawnPoints = new List<Transform>();
        public List<FireExtinguisher> Extinguishers = new List<FireExtinguisher>();
        public List<Door> ExitDoors = new List<Door>();
        //public List<PathMover> Paths = new List<PathMover>();

        private int _currentPath = 0;
        private Door _prevDestination;

        void Start()
        {
            RequestSpawn();
        }

        // DEBUG
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.N))
            {
                SetDestination();
                _currentPath++;
                if(_currentPath >= ExitDoors.Count)
                {
                    _currentPath = 0;
                }
            }
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
            SetDestination();
        }

        private void SetDestination()
        {
            if (_prevDestination != null)
            {
                _prevDestination.SetHighlightActive(false);
                Extinguishers[ExitDoors.IndexOf(_prevDestination)].GetComponent<Highlighable>().SetHighlightActive(false);
            }

            Door destination = ExitDoors[_currentPath];
            Navigation.SetDestination(destination.transform.position);
            destination.SetHighlightActive(true);
            Extinguishers[_currentPath].GetComponent<Highlighable>().SetHighlightActive(true);

            _prevDestination = destination;
        }
    }
}
