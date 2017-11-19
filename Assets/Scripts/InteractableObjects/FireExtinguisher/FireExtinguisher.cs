using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class FireExtinguisher : Interactable
    {
        protected override void EndPadAction(ViveController controller)
        {

        }

        protected override void StartPadAction(ViveController controller)
        {

        }

        protected override void StartTriggerAction(ViveController controller)
        {
            controller.SetObjectInHand(gameObject, true);
        }

        protected override void EndTriggerAction(ViveController controller)
        {
            controller.ReleaseObject();
        }

    }
}