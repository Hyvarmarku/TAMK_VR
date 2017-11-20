using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR {

    public class LaserPointer : MonoBehaviour {

        public Transform cameraRigTransform;
        public GameObject teleportReticlePrefab;
        private GameObject reticle;
        private Transform teleportReticleTransform;
        public Transform headTransform;
        public Vector3 teleportReticleOffset;
        private bool shouldTeleport;

        [SerializeField] public GameObject _laserPrefab;
        [SerializeField] private LayerMask _layerMask;
        private GameObject _laser;
        private Transform _laserTransform;
        private Vector3 _hitPoint;

        private void Start()
        {
            _laser = Instantiate(_laserPrefab);
            _laserTransform = _laser.transform;
            _laser.SetActive(false);
            reticle = Instantiate(teleportReticlePrefab);
            teleportReticleTransform = reticle.transform;
        }

        public void InputDownAction(ViveController controller)
        {
            RaycastHit hit;

            if (Physics.Raycast(controller.transform.position, transform.forward, out hit, 100, _layerMask))
            {
                _hitPoint = hit.point;
                ShowLaser(controller, hit);

                reticle.SetActive(true);
                teleportReticleTransform.position = _hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
            else
            {
                _laser.SetActive(false);
                reticle.SetActive(false);
            }
        }

        public void InputUpAction(ViveController controller)
        {
            if(shouldTeleport)
                Teleport();

            _laser.SetActive(false);
            reticle.SetActive(false);
        }

        private void ShowLaser(ViveController controller, RaycastHit hit)
        {
            _laser.SetActive(true);
            _laserTransform.position = Vector3.Lerp(controller.transform.position, _hitPoint, .5f);
            _laserTransform.LookAt(_hitPoint);
            _laserTransform.localScale = new Vector3(_laserTransform.localScale.x, _laserTransform.localScale.y, hit.distance);
        }
        private void Teleport()
        {
            shouldTeleport = false;
            reticle.SetActive(false);
            Vector3 difference = cameraRigTransform.position - headTransform.position;
            difference.y = 0;
            cameraRigTransform.position = _hitPoint + difference;

            Vector3 fixedPosition = cameraRigTransform.position;
            fixedPosition.y += 0.5f;
            cameraRigTransform.position = fixedPosition;
        }

    }
}
