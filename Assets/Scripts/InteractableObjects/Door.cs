using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class Door : Interactable
    {
        private Renderer _renderer;
        private MaterialPropertyBlock _propBlock;
        //public Material _material;

        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();

            SetHighlightActive(false);
        }

        protected override void EndPadAction(ViveController controller)
        {
           // throw new System.NotImplementedException();
        }

        protected override void EndTriggerAction(ViveController controller)
        {
            //throw new System.NotImplementedException();
        }

        protected override void StartPadAction(ViveController controller)
        {
            //throw new System.NotImplementedException();
        }

        protected override void StartTriggerAction(ViveController controller)
        {
            FindObjectOfType<ExitPathManager>().RequestSpawn();
        }

        public void SetHighlightActive(bool setOn)
        {
            if(setOn)
            {
                _renderer.GetPropertyBlock(_propBlock);
                _propBlock.SetFloat("_OutlineWidth", 1.14f);
            } else
            {
                _propBlock.SetFloat("_OutlineWidth", 0);
            }

            _renderer.SetPropertyBlock(_propBlock);
        }
    }
}
