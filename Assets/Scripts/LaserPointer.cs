using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR {

    public class LaserPointer : MonoBehaviour {

        [SerializeField] public GameObject _laserPrefab;
        private GameObject _laser;
        private Transform _laserTransform;
        private Vector3 _hitPoint;

        private void Start()
        {
            _laser = Instantiate(_laserPrefab);
            _laserTransform = _laser.transform;
            _laser.SetActive(false);
        }

        public void InputDownAction(ViveController controller)
        {
            RaycastHit hit;

            if(Physics.Raycast(controller.transform.position, transform.forward, out hit, 100))
            {
                _hitPoint = hit.point;
                ShowLaser(controller, hit);
            }
        }

        public void InputUpAction(ViveController controller)
        {
            _laser.SetActive(false);
        }

        private void ShowLaser(ViveController controller, RaycastHit hit)
        {
            _laser.SetActive(true);
            _laserTransform.position = Vector3.Lerp(controller.transform.position, _hitPoint.normalized, 0.5f);
            _laserTransform.LookAt(_hitPoint);
            _laserTransform.localScale = new Vector3(_laserTransform.localScale.x, _laserTransform.localScale.y, hit.distance);
        }
    }
}
