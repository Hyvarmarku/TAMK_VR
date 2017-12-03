using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR {
    public class FireExtinquisherHandle : Interactable
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private FireExtinguisher _fireExtinguisherCore;
        [SerializeField] private GameObject _particles;

        protected override void EndPadAction(ViveController controller)
        {
            _particles.SetActive(false);
        }

        protected override void EndTriggerAction(ViveController controller)
        {
            //if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            //{
            //    controller.ReleaseObject();
            //    _particles.SetActive(false);
            //}
            controller.ReleaseObject();
            _particles.SetActive(false);
        }

        protected override void StartPadAction(ViveController controller)
        {
            _particles.SetActive(true);
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
            GameObject target = RaycasterTool.CheckCollision(transform.position, transform.forward, 1, _layerMask);

            if (target)
            {
                var fire = target.GetComponent<Fire>();

            } 
        }
    }
}
