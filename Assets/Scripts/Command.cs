using System;
using UnityEngine;

public class Command : MonoBehaviour {
    public bool RequireHold
    {
        get { return _requireHold; }
    }

    private bool _requireHold = false;
    private Action _action;

    public Command(Action action)
    {
        _action = action;
    }
}
