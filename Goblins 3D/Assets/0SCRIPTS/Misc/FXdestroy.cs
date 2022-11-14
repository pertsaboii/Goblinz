using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXdestroy : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Invoke("Destroy", ps.startLifetime);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
