﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class ViveController : MonoBehaviour
    {
        public enum InputID
        {
            None,
            Trigger,
            Pad,
            Grip
        }
        // Object which is in users hand
        public GameObject ObjectInHand
        {
            get { return _objectInHand; }
        }

        public ViveController OtherController;
        public LaserPointer LaserPointer;

        [SerializeField] private Interactable _currentTarget;
        [SerializeField] private GameObject _objectInHand;

        private SteamVR_TrackedObject trackedObj;
        private SteamVR_Controller.Device Controller
        {
            get { return SteamVR_Controller.Input((int)trackedObj.index); }
        }

        void Awake()
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }

        private void Update()
        {
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if(!ObjectInHand && LaserPointer)
                    LaserPointer.InputDownAction(this);
                else if (_currentTarget)
                {
                    _currentTarget.InputDownAction(this, InputID.Pad);
                }
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (!ObjectInHand && LaserPointer)
                    LaserPointer.InputUpAction(this);
                else if (_currentTarget)
                {
                    _currentTarget.InputUpAction(this, InputID.Pad);
                }
            }

            HandleTriggerInput();

            HandleGripInput();
        }

        private void HandleGripInput()
        {
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                if (_currentTarget != null)
                {
                    _currentTarget.InputDownAction(this, InputID.Grip);
                }
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                if (_currentTarget != null)
                {
                    _currentTarget.InputUpAction(this, InputID.Grip);
                }
            }
        }

        private void HandleTriggerInput()
        {
            if (Controller.GetHairTriggerDown())
            {
                if (_currentTarget != null)
                {
                    _currentTarget.InputDownAction(this, InputID.Trigger);
                }
            }

            if (Controller.GetHairTriggerUp())
            {
                if (_currentTarget != null)
                {
                    _currentTarget.InputUpAction(this, InputID.Trigger);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Interactable>() != null)
            {
                _currentTarget = other.GetComponent<Interactable>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(_currentTarget)
                _currentTarget.InputUpAction(this, InputID.None);

            _currentTarget = null;
        }

        private FixedJoint AddFixedJoint()
        {
            FixedJoint fx = gameObject.AddComponent<FixedJoint>();
            fx.breakForce = 20000;
            fx.breakTorque = 20000;
            return fx;
        }

        public void SetObjectInHand(GameObject go, bool linkWithJoint)
        {
            _objectInHand = go;

            if (linkWithJoint)
            {
                if(OtherController.ObjectInHand == go)
                {
                    OtherController.ReleaseObject();
                }

                var joint = AddFixedJoint();
                joint.connectedBody = _objectInHand.GetComponentInParent<Rigidbody>();
            }
        }

        public void ReleaseObject()
        {
            var fx = GetComponent<FixedJoint>();
            if(fx)
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(fx);

                var objectInHandRigidbody = _objectInHand.GetComponentInParent<Rigidbody>();
                objectInHandRigidbody.velocity = Controller.velocity;
                objectInHandRigidbody.angularVelocity = Controller.angularVelocity;
            }

            _objectInHand = null;
            //_currentTarget = null;
        }
    }
}
