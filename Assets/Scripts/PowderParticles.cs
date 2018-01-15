using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderParticles : MonoBehaviour {
    private ParticleSystem _particles;

    private void start()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(!_particles.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartParticles()
    {
        gameObject.SetActive(true);
        if(!_particles)
        {
            _particles = GetComponent<ParticleSystem>();
        }

        _particles.Play();
        _particles.loop = true;
    }

    public void StopParticles()
    {
        if(_particles)
         _particles.loop = false;
    }
}
