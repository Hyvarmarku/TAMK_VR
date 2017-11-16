using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAMKVR
{
    public class ViveController : MonoBehaviour
    {
        private IInteractable _currentTarget;

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
                Debug.Log(gameObject.name + Controller.GetAxis());
            }

            HandleTriggerInput();

            HandleGripInput();
        }

        private void HandleGripInput()
        {
            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                Debug.Log(gameObject.name + " Grip Press");
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
            {
                Debug.Log(gameObject.name + " Grip Release");
            }
        }

        private void HandleTriggerInput()
        {
            if (Controller.GetHairTriggerDown())
            {
                if (_currentTarget != null)
                {
                    _currentTarget.StartInteraction();
                }
            }

            if (Controller.GetHairTriggerUp())
            {
                if (_currentTarget != null)
                {
                    _currentTarget.EndInteraction();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<IInteractable>() != null)
            {
                _currentTarget = other.GetComponent<IInteractable>();
                print("Current Target is: " + _currentTarget);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<IInteractable>() != null)
            {
                if (other.GetComponent<IInteractable>() == _currentTarget)
                    print("Current Target: " + _currentTarget + " was removed");
            }
        }
    }
}
