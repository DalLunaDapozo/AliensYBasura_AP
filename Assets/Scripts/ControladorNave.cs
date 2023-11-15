using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorNave : MonoBehaviour
{

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

    void Start()
    {
        fisicas = GetComponent<Rigidbody>();

        centro_de_la_pantalla.x = Screen.width * .5f;
        centro_de_la_pantalla.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        mouse_input.x = Input.mousePosition.x;
        mouse_input.y = Input.mousePosition.y;

        distancia_mouse.x = (mouse_input.x - centro_de_la_pantalla.x) / centro_de_la_pantalla.x;
        distancia_mouse.y = (mouse_input.y - centro_de_la_pantalla.y) / centro_de_la_pantalla.y;

        distancia_mouse = Vector2.ClampMagnitude(distancia_mouse, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), aceleracion_roll * Time.deltaTime);

        transform.Rotate(-distancia_mouse.y * velocidad_rotacion * Time.deltaTime, distancia_mouse.x * velocidad_rotacion * Time.deltaTime, rollInput * velocidad_roll * Time.deltaTime, Space.Self);

        velocidad_forward_actual = Mathf.Lerp(velocidad_forward_actual, Input.GetAxisRaw("Vertical") * velocidad_forward, acelaracion_forward * Time.deltaTime);
        velocidad_strafe_actual = Mathf.Lerp(velocidad_strafe_actual, Input.GetAxisRaw("Horizontal") * velocidad_strafe, acelaracion_strafe * Time.deltaTime);
        velocidad_hover_actual = Mathf.Lerp(velocidad_hover_actual, Input.GetAxisRaw("Hover") * velocidad_hover, acelaracion_hover * Time.deltaTime);

       
       
    }

    private void FixedUpdate()
    {
        fisicas.velocity += transform.forward * velocidad_forward_actual * Time.fixedDeltaTime;
        fisicas.velocity += (transform.right * velocidad_strafe_actual * Time.fixedDeltaTime) + (transform.up * velocidad_hover_actual * Time.fixedDeltaTime);
    }
}
