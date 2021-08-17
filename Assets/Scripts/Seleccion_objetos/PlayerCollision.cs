using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{

    bool carros, sillas, escalera, cajas, sombrillas;
    int n_carros = 4, n_sillas = 3, n_escalera = 1, n_cajas = 5, n_sombrillas = 2;
    bool flag_cambio_escena;


    public Text num_carros;
    public Text num_sillas;
    public Text num_escalera;
    public Text num_cajas;
    public Text num_sombrillas;
    public Text txt_desaparecer;

    public GameObject fondo_ganaste;

    private AudioSource music = null;
    public AudioClip sound_recoger = null;

    public Text timer;
    public GameObject container_txt_tiempo_logrado;
    public Text txt_tiempo_logrado;
    float tiempo_inicial = 0;
    float tiempo_limite = 300;
    public static float tiempo_final =0;
    bool time_flag = true; // true para que continue el tiempo

    public Text txt_ganaste;
    public AudioClip very_good;
    AudioSource audioSource;
    bool flag_very_good;

    void Start()
    {
        music = GetComponent<AudioSource>();
        carros = true;
        sillas = false;
        escalera = false;
        cajas = false;
        sombrillas = false;
        DestroyObjectDelayed();
        flag_cambio_escena = false;
        flag_very_good = false;
        audioSource = GetComponent<AudioSource>();

        container_txt_tiempo_logrado.SetActive(false);
        
    }

    void FixedUpdate()
    {
        if (flag_very_good)
        {
            txt_ganaste.text = "Very Good " + Seleccion_nombre.nombreUsuario + "!";
            audioSource.PlayOneShot(very_good);
            flag_very_good = false;
            container_txt_tiempo_logrado.SetActive(true);
            tiempo_final = tiempo_inicial;
            txt_tiempo_logrado.text = "Ganaste en: " + (tiempo_final).ToString("00:00");
            time_flag = false;
            PlayerMovement.PostToDatabase();
            StartCoroutine(CambioEscena());
        }
    }

    void Update()
    {
        if(n_sombrillas == 0)
        {
            flag_very_good = true;
            n_sombrillas--;
        }

        if (time_flag)
            tiempo_inicial = tiempo_inicial + 1 * Time.deltaTime;
        timer.text = tiempo_inicial.ToString("00:00");
        if (tiempo_inicial > 260)
            timer.color = Color.red;

        if (tiempo_inicial > 300)
        {
            SceneManager.LoadScene("Seleccion_objetos");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (n_carros == 0) {sillas = true;}
        if (n_sillas == 0) { escalera = true; }
        if (n_escalera == 0) { cajas = true; }
        if (n_cajas == 0) { sombrillas = true; }
        /*if (n_sombrillas == 0)
        {
            fondo_ganaste.SetActive(false);
            StartCoroutine(CambioEscena());
        }*/

        if (carros && other.CompareTag("Car"))
        {
            Destroy(other.gameObject);
            n_carros--;
            num_carros.text = "Car: " + n_carros.ToString();
            music.PlayOneShot(sound_recoger, 1);
        }

        if (sillas && other.CompareTag("Chair"))
        {
            Destroy(other.gameObject);
            n_sillas--;
            num_sillas.text = "Chair: " + n_sillas.ToString();
            music.PlayOneShot(sound_recoger, 1);
        }

        if (escalera && other.CompareTag("Ladder"))
        {
            Destroy(other.gameObject);
            n_escalera--;
            num_escalera.text = "Ladder: " + n_escalera.ToString();
            music.PlayOneShot(sound_recoger, 1);
        }

        if (cajas && other.CompareTag("Box"))
        {
            Destroy(other.gameObject);
            n_cajas--;
            num_cajas.text = "Box: " + n_cajas.ToString();
            music.PlayOneShot(sound_recoger, 1);
        }

        if (sombrillas && other.CompareTag("Umbrella"))
        {
            Destroy(other.gameObject);
            n_sombrillas--;
            num_sombrillas.text = "Umbrella: " + n_sombrillas.ToString();
            music.PlayOneShot(sound_recoger, 1);
        }


    }

    void DestroyObjectDelayed()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(txt_desaparecer, 5);
    }

    IEnumerator CambioEscena()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Seleccion_actividades");
    }

}
