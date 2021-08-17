using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Seleccion_nombre : MonoBehaviour
{
    public static string nombreUsuario;
    public static string emailUsuario;

    public static float time_coleccionObjetos=0;
    public static float time_profesiones=0;
    public static float time_seleccionObjetos=0;

    public Text nombre;
    public Text email;
    public Text txt_campos_obligatorios;

    AudioSource audioSource;
    public AudioClip background_music;

    //public TextField nombre;
    //public TextField email;

    public static User user_retrieved = new User();

    public void Start()
    {
        txt_campos_obligatorios.text = "";
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(background_music);
    }

    public void setNombre()
    {
        if(nombre.text=="" || email.text=="")
        {
            txt_campos_obligatorios.text = "* Todos los campos son obligatorios.";
        }
        else
        {
            nombreUsuario = nombre.text;
            emailUsuario = email.text;
            RetrieveFromDatabase();
            SceneManager.LoadScene("Seleccion_actividades");
        }
    }

    public static User RetrieveFromDatabase()
    {
        RestClient.Get<User>("https://secunsadb.firebaseio.com/" + emailUsuario + ".json").Then(response =>
        {
            user_retrieved = response;
            Debug.Log(user_retrieved.tiempo_ColeccionObjetos);
            PlayerMovement.tiempo_coleccion = user_retrieved.tiempo_ColeccionObjetos;
            return user_retrieved;
        });
        User temp = new User();
        return temp;
    }

    public void music_on_off()
    {
        if (User.flag_musica)
            User.flag_musica = false;
        else
            User.flag_musica = true;

        if (!User.flag_musica)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1;
        }
    }
}
