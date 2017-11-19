using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR {
    public class FireExtinquisherHandle : Interactable
    {
        [SerializeField] private FireExtinguisher _fireExtinguisherCore;
        [SerializeField] private GameObject _particles;

        protected override void EndPadAction(ViveController controller)
        {

        }

        protected override void EndTriggerAction(ViveController controller)
        {
            if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            {
                controller.ReleaseObject();
                _particles.SetActive(false);
            }
        }

        protected override void StartPadAction(ViveController controller)
        {
            
        }

        protected override void StartTriggerAction(ViveController controller)
        {
            if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            {
                //controller.OtherController.ReleaseObject();
                controller.SetObjectInHand(gameObject, false);
                _particles.SetActive(true);
            }
        }
    }
}
