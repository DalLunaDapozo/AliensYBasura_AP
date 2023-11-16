using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody fisicas;

    private float velocidad;
    private int daño;

    public float tiempo_antes_de_destruirse = 10f;

    private void Start()
    {
        fisicas = GetComponent<Rigidbody>();
        fisicas.AddForce(transform.forward * velocidad, ForceMode.Impulse);
    }

    private void Update()
    {
        if (tiempo_antes_de_destruirse > 0)
        {
            tiempo_antes_de_destruirse -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBala(float _vel, int _d)
    {
        this.velocidad = _vel;
        this.daño = _d;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
