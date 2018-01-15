using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fire : MonoBehaviour, QuestObject {

    public bool Shrink = false;
    private float _scale = 1;
    private ParticleSystem _particles;
    private TAMKVR.ExitPathManager _exitPathManager;

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Cleared()
    {
        gameObject.SetActive(false);
        this._exitPathManager.HideNavigation(true);
        _exitPathManager.SetDestination();
    }

    private void Start()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        _scale = _particles.transform.localScale.x;
        _exitPathManager = FindObjectOfType<TAMKVR.ExitPathManager>();
        this.gameObject.SetActive(false);
    }
    void Update ()
    {
	    if(Shrink)
        {
            _scale -= (0.2f * Time.deltaTime);
            if(_scale <= 0.2f)
            {
                Cleared();
                _scale = 1;
            }
            _particles.transform.localScale = new Vector3(_scale, _scale, _scale);
        }
        Shrink = false;
	}
}
