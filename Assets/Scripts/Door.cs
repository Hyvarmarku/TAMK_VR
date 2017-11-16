using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable {
    public void EndInteraction()
    {
        print("INTERACTION ENDED");
    }

    public void StartInteraction()
    {
        print("DOOR WAS OPENED");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
