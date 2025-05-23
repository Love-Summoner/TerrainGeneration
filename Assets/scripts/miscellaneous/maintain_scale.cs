using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maintain_scale : MonoBehaviour
{
    private ParticleSystem system;
    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(system.particleCount == 0) 
            Destroy(gameObject);
    }
}
