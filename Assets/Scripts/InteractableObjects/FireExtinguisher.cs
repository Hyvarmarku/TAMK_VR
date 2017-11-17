using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class FireExtinguisher : Interactable
    {
        public override void EndInteractionAction(ViveController controller)
        {
            controller.ReleaseObject();
        }

        public override void StartInteractionAction(ViveController controller)
        {
            controller.SetObjectInHand(this.gameObject, true);
        }
    }
}