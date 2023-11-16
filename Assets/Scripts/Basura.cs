using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : MonoBehaviour
{
    public float empujon_inicial = 0.1f;
    private Rigidbody fisicas;

    public int vida;

    public GameObject particulas_muerte;

    void Start()
    {
        fisicas = GetComponent<Rigidbody>();
        fisicas.AddForce(transform.right * empujon_inicial);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (vida > 0)
            vida -= 1;
        else
        {
            Instantiate(particulas_muerte, transform.position, transform.rotation);
            Destroy(gameObject);
        }
            
    }
}
