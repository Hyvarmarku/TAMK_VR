using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class PathMover : MonoBehaviour
    {
        public float Duration = 3f;
        [SerializeField] private BezierSpline _currentPath;

        private float _progress;
        private Vector3 _currentMovingDirection;
        private TrailRenderer _trailRenderer;

        private void Start()
        {
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_progress >= 1)
            {
                _progress = 0;
                _trailRenderer.Clear();
            }

            Vector3 nextPosition = _currentPath.GetPoint(1 - _progress);
            _currentMovingDirection = nextPosition - transform.position;
            transform.position = nextPosition;
            _progress += Time.deltaTime / Duration;
            transform.rotation = Quaternion.LookRotation(_currentMovingDirection);
        }
    }
}
