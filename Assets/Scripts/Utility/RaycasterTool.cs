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
            result = hit.transform.gameObject;
        }
        Debug.DrawRay(startingPosition, direction * rayLength, Color.red);

        return result;
    }
}

