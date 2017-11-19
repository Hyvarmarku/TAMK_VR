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
                _particles.SetActive(false);
            }
        }

        protected override void StartPadrAction(ViveController controller)
        {
            
        }

        protected override void StartTriggerAction(ViveController controller)
        {
            if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            {
                //controller.OtherController.ReleaseObject();
                //controller.SetObjectInHand(_fireExtinguisherCore.gameObject, true);
                _particles.SetActive(true);
            }
        }
    }
}
