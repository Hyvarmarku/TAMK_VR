using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR {
    public class FireExtinquisherHandle : Interactable
    {
        [SerializeField] private FireExtinguisher _fireExtinguisherCore;
        [SerializeField] private GameObject _particles;

        public override void EndInteractionAction(ViveController controller)
        {
            if (controller.OtherController.ObjectInHand == _fireExtinguisherCore.gameObject)
            {
                _particles.SetActive(false);
            }
        }

        public override void StartInteractionAction(ViveController controller)
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
