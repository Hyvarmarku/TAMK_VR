using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class Highlighable : MonoBehaviour
    {

        public Renderer _renderer;
        private MaterialPropertyBlock _propBlock;
        //public Material _material;

        private void Awake()
        {
            _propBlock = new MaterialPropertyBlock();

            SetHighlightActive(false);
        }

        public void SetHighlightActive(bool setOn)
        {
            if (setOn)
            {
                _renderer.GetPropertyBlock(_propBlock);
                _propBlock.SetFloat("_OutlineWidth", 1.14f);
            }
            else
            {
                _propBlock.SetFloat("_OutlineWidth", 0);
            }

            _renderer.SetPropertyBlock(_propBlock);
        }
    }
}
