using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNave : MonoBehaviour
{

    #region VARIABLES

    //VARIABLES MOVIMIENTO

    public float velocidad_forward;
    public float velocidad_strafe;
    public float velocidad_hover;

    private float velocidad_forward_actual;
    private float velocidad_strafe_actual;
    private float velocidad_hover_actual;

    private float acelaracion_forward = 2.5f;
    private float acelaracion_strafe = 2f;
    private float acelaracion_hover = 2f;

    public float velocidad_rotacion;
    
    private Vector2 mouse_input;
    private Vector2 centro_de_la_pantalla;
    private Vector2 distancia_mouse;

    public float velocidad_roll;
    private float aceleracion_roll = 3.5f;
    private float rollInput;

    private Rigidbody fisicas;

    //OTRAS VARIABLES

    public int vida;
    public bool esta_vivo;

    public Bala bala;
    public int daño_bala;
    public float velocidad_bala;
    public bool puede_disparar;

    public float fire_rate;
    private float fire_rate_timer;
    public Transform de_donde_sale_la_bala;

    #endregion

    void Start()
    {
        fisicas = GetComponent<Rigidbody>();

        //SACO EL CENTRO DE LA PANTALLA (BASE POR ALTURA POR LA MITAD)
        centro_de_la_pantalla.x = Screen.width * .5f;
        centro_de_la_pantalla.y = Screen.height * .5f;

        //LOCKEO EL CURSOR PARA QUE NO SE SALGA
        Cursor.lockState = CursorLockMode.Confined;

        fire_rate_timer = fire_rate;
        puede_disparar = true;
    }

    void Update()
    {
        Movimiento();
        Disparar();
    }

    private void FixedUpdate()
    {
        fisicas.velocity += transform.forward * velocidad_forward_actual * Time.fixedDeltaTime;
        fisicas.velocity += (transform.right * velocidad_strafe_actual * Time.fixedDeltaTime) + (transform.up * velocidad_hover_actual * Time.fixedDeltaTime);
    }

    private void Disparar()
    {
        
        if (Input.GetButton("Fire1") && puede_disparar)
        {
            puede_disparar = false;
            Bala nueva_bala = Instantiate(bala, de_donde_sale_la_bala.position, transform.rotation);
            nueva_bala.SetBala(velocidad_bala, daño_bala);
        }

        if (!puede_disparar)
        {
            if (fire_rate_timer > 0)
                fire_rate_timer -= Time.deltaTime;
            else
            {
                puede_disparar = true;
                fire_rate_timer = fire_rate;
            }

        }
    }

    private void Movimiento()
    {
        //OBTENGO EL INPUT DE LOS MOUSE 
        mouse_input.x = Input.mousePosition.x;
        mouse_input.y = Input.mousePosition.y;

        //ENCUENTRO EL CENTRO DE LA PANTALLA 
        distancia_mouse.x = (mouse_input.x - centro_de_la_pantalla.x) / centro_de_la_pantalla.x;
        distancia_mouse.y = (mouse_input.y - centro_de_la_pantalla.y) / centro_de_la_pantalla.y;

        //HAGO CLAMP PARA QUE EL ANCHO Y ALTO DE LA PANTALLA NO HAGAN DIFERENCIA
        distancia_mouse = Vector2.ClampMagnitude(distancia_mouse, 1f);

        //PARA ROTAR SOBRE EL EJE Z
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), aceleracion_roll * Time.deltaTime);

        //ROTACION CON EL MOUSE EN X e Y
        transform.Rotate(-distancia_mouse.y * velocidad_rotacion * Time.deltaTime, distancia_mouse.x * velocidad_rotacion * Time.deltaTime, rollInput * velocidad_roll * Time.deltaTime, Space.Self);

        //PARA MOVERME PARA ADELANTE Y ATRAS
        velocidad_forward_actual = Mathf.Lerp(velocidad_forward_actual, Input.GetAxisRaw("Vertical") * velocidad_forward, acelaracion_forward * Time.deltaTime);
        //PARA MOVERME PARA LOS COSTADOS
        velocidad_strafe_actual = Mathf.Lerp(velocidad_strafe_actual, Input.GetAxisRaw("Horizontal") * velocidad_strafe, acelaracion_strafe * Time.deltaTime);
        //PARA MOVERME EN Y
        velocidad_hover_actual = Mathf.Lerp(velocidad_hover_actual, Input.GetAxisRaw("Hover") * velocidad_hover, acelaracion_hover * Time.deltaTime);
    }

    public float GetVelocity()
    {
        return velocidad_forward_actual;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (vida > 0)
        {
            vida -= 1;
        }
        else
        {
            esta_vivo = false;
        }
    }
}
