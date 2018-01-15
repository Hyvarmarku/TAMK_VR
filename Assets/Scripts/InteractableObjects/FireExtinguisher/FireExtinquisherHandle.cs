using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR {
    public class FireExtinquisherHandle : Interactable
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private FireExtinquisherHose _fireExtinguisherHose;
        [SerializeField] private PowderParticles _particles;
        [SerializeField] private float _rangeOfPowder;

        private bool _isFiring = false;
        private float _currentRange = 0;
        protected override void EndPadAction(ViveController controller)
        {
            _particles.StopParticles();
            _currentRange = 0;
        }

        protected override void EndTriggerAction(ViveController controller)
        {
            //if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            //{
            //    controller.ReleaseObject();
            //    _particles.SetActive(false);
            //}
            controller.ReleaseObject();
            controller.OtherController.ReleaseObject();
            //_particles.SetActive(false);
            _isFiring = false;
        }

        protected override void StartPadAction(ViveController controller)
        {
            _particles.StartParticles();
            _isFiring = true;
        }

        protected override void StartTriggerAction(ViveController controller)
        {
            //if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            //{
            //    //controller.OtherController.ReleaseObject();
            //    controller.SetObjectInHand(gameObject, false);
            //    _particles.SetActive(true);
            //}
            if (controller.ObjectInHand != gameObject)
            {
                controller.SetObjectInHand(gameObject, true);
            }            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.K))
            {
                _isFiring = true;
                _particles.StartParticles();
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                _isFiring = false;
                _particles.StopParticles();
                _currentRange = 0;
            }

            if (_isFiring)
            {
                _currentRange += 15 * Time.deltaTime;
                if(_currentRange > _rangeOfPowder)
                {
                    _currentRange = _rangeOfPowder;
                }

                GameObject target = RaycasterTool.CheckCollision(_fireExtinguisherHose.transform.position, _fireExtinguisherHose.transform.forward, 1000, _layerMask);
                if (target)
                {
                    var fire = target.GetComponent<Fire>();
                    fire.Shrink = true;
                }
            }
        }
    }
}
