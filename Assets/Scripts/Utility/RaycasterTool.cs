using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class RaycasterTool
{
    public static GameObject CheckCollision(Vector3 startingPosition, Vector3 direction, float rayLength, LayerMask layer)
    {
        GameObject result = null;

        RaycastHit hit;
        
        if (Physics.Raycast(startingPosition, direction, out hit, rayLength, layer)) {
            Debug.Log(hit);
            //hit.transform.gameObject.SetActive(false);
        }
        Debug.DrawRay(startingPosition, direction, Color.red);

        return result;
    }
}

