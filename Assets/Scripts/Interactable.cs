using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TAMKVR
{
    public abstract class Interactable : MonoBehaviour
    {
        private List<InputCommand> _inputCommands;

        // Always use this as a base.
        protected virtual void Awake()
        {
            _inputCommands = GetComponents<InputCommand>().ToList();
            foreach(var command in _inputCommands)
            {
                command.Init(this);
            }
        }

        public void InputDownAction(ViveController controller, ViveController.InputID inputID)
        {
            InputCommand command = _inputCommands.FirstOrDefault(a => a.InputID == inputID);
            if(command == null)
            {
                Debug.Log("Command: " + inputID + " has not been set for " + gameObject);
                return;
            }
            // Does action trigger by toggling? 
            if(!command.RequireHold)
            {
                // Object is already in hand, so this is the second toggle action.
                if(controller.ObjectInHand == this.gameObject)
                {
                    command.EndAction(controller);
                }
                // This is the first time interacting with this object.
                else
                {
                    command.StartAction(controller);
                }
            }
            else
            {
                command.StartAction(controller);
            }
        }

        public void InputUpAction(ViveController controller, ViveController.InputID inputID)
        {

            if(inputID == ViveController.InputID.None)
            {
                CancelAllActions(controller);
                return;
            }

            InputCommand command = _inputCommands.FirstOrDefault(a => a.InputID == inputID);
            if (command == null)
            {
                return;
            }

            // Does action trigger by toggling? If does, ignore this action call.
            if (!command.RequireHold)
            {
                return;
            }
            else
            {
                command.EndAction(controller);
            }
        }

#region CommandAction Delegate Getters
        public InputCommand.CommandAction GetStartAction(ViveController.InputID inputId)
        {
            InputCommand.CommandAction action = null;
            switch(inputId)
            {
                case ViveController.InputID.Trigger:
                    action = StartTriggerAction;
                    break;
                case ViveController.InputID.Pad:
                    action = StartPadAction;
                    break;
                case ViveController.InputID.Grip:
                    //action = StartGripAction;
                    break;
            }

            return action;
        }

        public InputCommand.CommandAction GetEndAction(ViveController.InputID inputId)
        {
            InputCommand.CommandAction action = null;
            switch (inputId)
            {
                case ViveController.InputID.Trigger:
                    action = EndTriggerAction;
                    break;
                case ViveController.InputID.Pad:
                    action = EndPadAction;
                    break;
                case ViveController.InputID.Grip:
                    //action = StartGripAction;
                    break;
            }

            return action;
        }
#endregion

        private void CancelAllActions(ViveController controller)
        {
            foreach(var command in _inputCommands)
            {
                command.EndAction(controller);
            }
        }

        protected abstract void StartTriggerAction(ViveController controller);
        protected abstract void EndTriggerAction(ViveController controller);

        protected abstract void StartPadAction(ViveController controller);
        protected abstract void EndPadAction(ViveController controller);
    }
}
