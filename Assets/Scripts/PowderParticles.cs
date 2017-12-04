using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowderParticles : MonoBehaviour {
    private ParticleSystem _particles;

    private void Start()
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
        _particles.Play();
        _particles.loop = true;
    }

    public void StopParticles()
    {
        _particles.loop = false;
    }
}
