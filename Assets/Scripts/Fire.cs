using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    public bool Shrink = false;
    private float _scale = 1;
    private ParticleSystem _particles;

    private void Start()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        _scale = _particles.transform.localScale.x;
    }
    void Update ()
    {
	    if(Shrink)
        {
            _scale -= (0.1f * Time.deltaTime);
            if(_scale <= 0.2f)
            {
                gameObject.SetActive(false);
                _scale = 1;
            }
            _particles.transform.localScale = new Vector3(_scale, _scale, _scale);
        }
        Shrink = false;
	}
}
