using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public abstract class Interactable : MonoBehaviour
    {
        public enum InputID
        {
            None,
            Trigger,
            Pad,
            Grip
        }

        public bool RequireHold
        {
            get { return _requireHold; }
        }

        [Tooltip("Does interaction support holding. Example: Ss holding a trigger required to hold an object on your hand or not")]
        [SerializeField] private bool _requireHold = false;

        public void InputDownAction(ViveController controller, InputID inputID)
        {
            // Does action trigger by toggling? 
            if(!RequireHold)
            {
                // Object is already in hand, so this is the second toggle action.
                if(controller.ObjectInHand == this.gameObject)
                {
                    EndInteractionAction(controller, inputID);
                }
                // This is the first time interacting with this object.
                else
                {
                    StartInteractionAction(controller, inputID);
                }
            }
            else
            {
                StartInteractionAction(controller, inputID);
            }
        }

        public void InputUpAction(ViveController controller, InputID inputID)
        {
            // Does action trigger by toggling? If does, ignore this action call.
            if(!RequireHold)
            {
                return;
            }
            else
            {
                EndInteractionAction(controller,inputID);
            }
        }

        public void StartInteractionAction(ViveController controller, InputID inputID)
        {
            switch(inputID)
            {
                case InputID.None:
                    return;
                case InputID.Trigger:
                    StartTriggerAction(controller);
                    break;
                case InputID.Pad:
                    StartTriggerAction(controller);
                    break;
            }
        }

        public void EndInteractionAction(ViveController controller, InputID inputID)
        {
            switch (inputID)
            {
                // Add every input action cancellor to this.
                case InputID.None:
                    EndTriggerAction(controller);
                    EndPadAction(controller);
                    break;
                case InputID.Trigger:
                    EndTriggerAction(controller);
                    break;
                case InputID.Pad:
                    EndPadAction(controller);
                    break;
            }
        }

        protected abstract void StartTriggerAction(ViveController controller);
        protected abstract void EndTriggerAction(ViveController controller);

        protected abstract void StartPadrAction(ViveController controller);
        protected abstract void EndPadAction(ViveController controller);
    }
}
