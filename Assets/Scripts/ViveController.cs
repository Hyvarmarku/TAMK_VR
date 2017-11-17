using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class ViveController : MonoBehaviour
    {
        // Object which is in users hand
        public GameObject ObjectInHand
        {
            get { return _objectInHand; }
        }

        public ViveController OtherController;

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
            if (Controller.GetAxis() != Vector2.zero)
            {
                //Debug.Log(gameObject.name + Controller.GetAxis());
            }

            HandleTriggerInput();

            HandleGripInput();
        }

        private void HandleGripInput()
        {
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                //Debug.Log(gameObject.name + " Grip Press");
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                //Debug.Log(gameObject.name + " Grip Release");
            }
        }

        private void HandleTriggerInput()
        {
            if (Controller.GetHairTriggerDown())
            {
                if (_currentTarget != null)
                {
                    _currentTarget.InputDownAction(this);
                }
            }

            if (Controller.GetHairTriggerUp())
            {
                if (_currentTarget != null)
                {
                    _currentTarget.InputUpAction(this);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject);
            if (other.GetComponent<Interactable>() != null)
            {
                _currentTarget = other.GetComponent<Interactable>();
               // print("Current Target is: " + _currentTarget);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_objectInHand == null)
            {
                Debug.Log("EXIT");
                if(_currentTarget)
                    _currentTarget.EndInteractionAction(this);

                _currentTarget = null;
            }
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
                joint.connectedBody = _objectInHand.GetComponent<Rigidbody>();
            }
        }

        public void ReleaseObject()
        {
            var fx = GetComponent<FixedJoint>();
            if(fx)
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(fx);

                var objectInHandRigidbody = _objectInHand.GetComponent<Rigidbody>();
                objectInHandRigidbody.velocity = Controller.velocity;
                objectInHandRigidbody.angularVelocity = Controller.angularVelocity;
            }

            _objectInHand = null;
            _currentTarget = null;
        }
    }
}
