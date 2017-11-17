using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class Door : Interactable
    {
        public override void EndInteractionAction(ViveController controller)
        {
            print("INTERACTION ENDED");
            controller.ReleaseObject();
        }

        public override void StartInteractionAction(ViveController controller)
        {
            print("DOOR WAS OPENED");
            controller.SetObjectInHand(this.gameObject, false);
        }
    }
}
