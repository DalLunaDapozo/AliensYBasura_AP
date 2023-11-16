using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoCola : MonoBehaviour
{
    private Light luz;
    [SerializeField] private ControladorNave jugador;

    private void Start()
    {
        luz = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (jugador.GetVelocity() > 0)
        {
            luz.intensity = Mathf.Lerp(0.1f, 0.6f, 1f);
        }
        else
        {
            luz.intensity = Mathf.Lerp(0.6f, 0.1f, 1f);
        }
    }
}
