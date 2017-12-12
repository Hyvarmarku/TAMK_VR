using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private float _movementSpeed = 0.05f;
    [SerializeField] private float _turningSpeed = 0.05f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime * _movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * _movementSpeed;
        }
        if(Input.GetKey(KeyCode.R))
        {
            var newPosition = transform.position;
            newPosition.y += 0.05f;
            transform.position = newPosition;
        }
        if (Input.GetKey(KeyCode.F))
        {
            var newPosition = transform.position;
            newPosition.y -= 0.05f;
            transform.position = newPosition;
        }

        if (Input.GetKey(KeyCode.A))
        {
            var newRotation = transform.eulerAngles;
            newRotation.y -= _turningSpeed;
            transform.eulerAngles = newRotation;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var newRotation = transform.eulerAngles;
            newRotation.y += _turningSpeed;
            transform.eulerAngles = newRotation;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            var newRotation = transform.eulerAngles;
            newRotation.x -= _turningSpeed;
            transform.eulerAngles = newRotation;
        }

        if (Input.GetKey(KeyCode.E))
        {
            var newRotation = transform.eulerAngles;
            newRotation.x += _turningSpeed;
            transform.eulerAngles = newRotation;
        }

    }
}
