using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    // Update is called once per frame

    public GameObject fondo;
    public GameObject tab;
    public GameObject txt_titulo;
    public GameObject textos_canvas;
    public Text txt_colour;
    public Text txt_professions;
    public Text txt_objects;
    public Text txt_name;
    public Text txt_email;
    public Text score_colour;
    public Text score_professions;
    public Text score_objects;
    public Text score_name;
    public Text score_email;

    public static User player;
    public static float tiempo_coleccion=0;
    public static float tiempo_profesiones = 0;
    public static float tiempo_seleccion = 0;
    //static User user_retrieved = new User();

    void Start()
    {
        fondo.SetActive(false);
        txt_titulo.SetActive(false);
        tab.SetActive(true);
        textos_canvas.SetActive(false);
        //player = Seleccion_nombre.RetrieveFromDatabase();
        Debug.Log("aqui" + Seleccion_nombre.user_retrieved.tiempo_ColeccionObjetos.ToString());
        if (Seleccion_nombre.user_retrieved.tiempo_ColeccionObjetos == 0 && Seleccion_nombre.user_retrieved.tiempo_Profesiones== 0 && Seleccion_nombre.user_retrieved.tiempo_SeleccionObjetos== 0)
        {
            score_colour.text = PlayerController.tiempo_final.ToString("00:00");
            score_professions.text = Profesiones_Collision.tiempo_final.ToString("00:00");
            score_objects.text = PlayerCollision.tiempo_final.ToString("00:00");
            score_name.text = Seleccion_nombre.nombreUsuario;
            score_email.text = Seleccion_nombre.emailUsuario;
        }
        else
        {
            score_colour.text = player.tiempo_ColeccionObjetos.ToString("00:00");
            score_professions.text = player.tiempo_Profesiones.ToString("00:00");
            score_objects.text = player.tiempo_SeleccionObjetos.ToString("00:00");
            score_name.text = player.nombreUsuario;
            score_email.text = player.emailUsuario;
        }
        User.flag_musica = true;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move =  transform.forward * z;
        Vector3 rotate = new Vector3(0, Input.GetAxis("Horizontal"), 0);//transform.right * x;
        transform.Rotate(rotate * speed * 10 * Time.deltaTime);

        controller.Move(move * speed * Time.deltaTime);

        /*if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }*/

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKey(KeyCode.Tab))
        {
            fondo.SetActive(true);
            txt_titulo.SetActive(true);
            tab.SetActive(false);
            textos_canvas.SetActive(true);
        }
        else
        {
            fondo.SetActive(false);
            txt_titulo.SetActive(false);
            tab.SetActive(true);
            textos_canvas.SetActive(false);
        }
    }

    public static void PostToDatabase()
    {
        User user = new User();
        RestClient.Put("https://secunsadb.firebaseio.com/" + user.emailUsuario + ".json", user);
    }

    
}
