using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool RequireHold
        {
            get { return _requireHold; }
        }

        [SerializeField] private bool _requireHold = false;

        public void InputDownAction(ViveController controller)
        {
            // Does action trigger by toggling? 
            if(!RequireHold)
            {
                // Object is already in hand, so this is the second toggle action.
                if(controller.ObjectInHand == this.gameObject)
                {
                    EndInteractionAction(controller);
                }
                // This is the first time interacting with this object.
                else
                {
                    StartInteractionAction(controller);
                }
            }
            else
            {
                StartInteractionAction(controller);
            }
        }

        public void InputUpAction(ViveController controller)
        {
            // Does action trigger by toggling? If does, ignore this action call.
            if(!RequireHold)
            {
                return;
            }
            else
            {
                EndInteractionAction(controller);
            }
        }

        public abstract void StartInteractionAction(ViveController controller);
        public abstract void EndInteractionAction(ViveController controller);
    }
}
