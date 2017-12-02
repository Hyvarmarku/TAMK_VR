using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class FireExtinquisherHose : Interactable
    {
        private ViveController _controllerHolding;
        private Vector3 _positionOnPrevFrame = Vector3.zero;

        protected override void EndPadAction(ViveController controller)
        {
            throw new System.NotImplementedException();
        }

        protected override void EndTriggerAction(ViveController controller)
        {
            //controller.ReleaseObject();
            //_controllerHolding = null;
        }

        protected override void StartPadAction(ViveController controller)
        {
            throw new System.NotImplementedException();
        }

        protected override void StartTriggerAction(ViveController controller)
        {
            if(!controller.ObjectInHand)
            {
                controller.SetObjectInHand(gameObject, false);
                _controllerHolding = controller;
                _positionOnPrevFrame = _controllerHolding.transform.position;
            }
        }

        private void Update()
        {

            if (_controllerHolding != null)
            {
                var rotation = transform.localEulerAngles;

                rotation.y += (_controllerHolding.transform.position.y - _positionOnPrevFrame.y) * 1000f;

                transform.localEulerAngles = rotation;
                _positionOnPrevFrame = _controllerHolding.transform.position;
            }
        }
    }
}
