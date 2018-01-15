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
        public Transform NextAreaSpawn;
        public List<FireExtinguisher> Extinguishers = new List<FireExtinguisher>();
        public List<Door> ExitDoors = new List<Door>();
        public List<QuestTriggerArea> QuestTriggers = new List<QuestTriggerArea>();
        public bool _canContinue = true;
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
            if (Input.GetKeyDown(KeyCode.N))
            {
                SetDestination();
                if (_currentPath >= ExitDoors.Count)
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

            //_currentPath++;
        }

        private void SpawnPlayer()
        {
            Player.position = SpawnPoints[_currentPath].position;
            SetDestination();
        }

        public void SetDestination()
        {
            if (_prevDestination != null)
            {
                _prevDestination.SetHighlightActive(false);
            }

            if(_currentPath >= ExitDoors.Count)
            {
                Debug.Log("CLEARED");
                Cleared();
                return;
            }

            if(_prevDestination != null && _currentPath != 0 && Extinguishers.Count > _currentPath)
            {
                Extinguishers[_currentPath -1].GetComponent<Highlighable>().SetHighlightActive(false);
                Extinguishers[_currentPath].GetComponent<Highlighable>().SetHighlightActive(true);
            }

            Door destination = ExitDoors[_currentPath];
            QuestTriggers[_currentPath].Activate();
            Navigation.SetDestination(destination.transform.position);
            destination.SetHighlightActive(true);

            _prevDestination = destination;

            _currentPath++;
        }

        public void HideNavigation(bool hide)
        {
            this.Navigation.gameObject.SetActive(hide);
            if (_prevDestination != null)
            {
                _prevDestination.SetHighlightActive(hide);
            }
        }

        public void NextArea()
        {
            Player.position = NextAreaSpawn.position;
            SetDestination();
        }

        public void Cleared()
        {
            foreach(FireExtinguisher ex in Extinguishers)
            {
                ex.GetComponent<Highlighable>().SetHighlightActive(false);
            }

            _currentPath = 0;
            Player.position = SpawnPoints[0].position;
            SetDestination();
        }
    }
}
