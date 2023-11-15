using System.Collections.Generic;
using UnityEngine;

public class GeneradorBasura : MonoBehaviour
{
    public float rango_de_aparicion;
    public float numero_de_objetos;
    public float rango_seguro_inicio;
   
    public List<GameObject> objetos_basura;

    private Vector3 puntos_de_spawn;
    private List<GameObject> objetos_a_instanciar = new List<GameObject>();

    
    void Start()
    {
        for (int i = 0; i < numero_de_objetos; i++)
        {
            PickSpawnPoint();

           
            while (Vector3.Distance(puntos_de_spawn, Vector3.zero) < rango_seguro_inicio)
            {
                PickSpawnPoint();
            }

            int index_random = Random.Range(0, objetos_basura.Count);

            objetos_a_instanciar.Add(Instantiate(objetos_basura[index_random], puntos_de_spawn, Quaternion.Euler(Random.Range(0f,360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
            objetos_a_instanciar[i].transform.parent = this.transform;
        }

        //objetos_basura.SetActive(false);
    }

    public void PickSpawnPoint()
    {
        puntos_de_spawn = new Vector3(
            Random.Range(-1f,1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));

        if(puntos_de_spawn.magnitude > 1)
        {
            puntos_de_spawn.Normalize();
        }

        puntos_de_spawn *= rango_de_aparicion;
    }
}

