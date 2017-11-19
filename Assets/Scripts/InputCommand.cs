using System;
using UnityEngine;

namespace TAMKVR
{
    public class InputCommand : MonoBehaviour
    {
        public bool RequireHold
        {
            get { return _requireHold; }
        }
        public CommandAction StartAction
        {
            get { return _startAction; }
        }
        public CommandAction EndAction
        {
            get { return _endAction; }
        }
        public ViveController.InputID InputID
        {
            get { return _inputId; }
        }

        public delegate void CommandAction(ViveController controller);

        [SerializeField] private ViveController.InputID _inputId;
        [SerializeField] private bool _requireHold;
        private CommandAction _startAction;
        private CommandAction _endAction;

        public void Init(Interactable master)
        {
            _startAction = master.GetStartAction(_inputId);
            _endAction = master.GetEndAction(_inputId);
        }
    }
}
