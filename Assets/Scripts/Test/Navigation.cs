using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour {

    public Transform Player;
    private NavMeshAgent _navMeshAgent;
    private Vector3 _destination;
    private TrailRenderer _trailRenderer;
    private Vector3 _prevPosition;

	void Start ()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if(transform.position == _prevPosition)
        {
            SetDestination(_destination);
        }

        _prevPosition = transform.position;
    }

    public void SetDestination(Vector3 destination)
    {
        Vector3 position = transform.position;
        position.x = Player.position.x;
        position.z = Player.position.z;
        //transform.position = position;
        _navMeshAgent.Warp(position);

        _navMeshAgent.SetDestination(destination);
        _destination = destination;
        _trailRenderer.Clear();
    }
}
