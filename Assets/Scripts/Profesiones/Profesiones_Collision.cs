using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Profesiones_Collision : MonoBehaviour
{
    public Text txt_desaparecer;
    public Text txt_dialogos;
    public Text txt_indicaciones;

    bool nurse, teacher, farmer, police;
    bool flag_nurse, flag_teacher, flag_farmer, flag_police;
    bool flag_final, flag_very_good;

    private AudioSource music = null;

    public AudioClip monica_hi;
    public AudioClip carla_hi;
    public AudioClip john_hi;
    public AudioClip marcos_hi_bye;
    public AudioClip john_bye;
    public AudioClip carla_bye;
    public AudioClip monica_bye;

    public Text timer;
    public GameObject container_txt_tiempo_logrado;
    public Text txt_tiempo_logrado;
    float tiempo_inicial = -5;
    float tiempo_limite = 300;
    public static float tiempo_final = 0;
    bool time_flag = true; // true para que continue el tiempo

    public Text txt_ganaste;
    public AudioClip very_good;
    AudioSource audioSource;

    void Start()
    {
        music = GetComponent<AudioSource>();
        txt_dialogos.text = "";
        DestroyObjectDelayed();
        nurse = false; teacher = true; farmer = false; police = false;
        flag_nurse = false; flag_teacher = false; flag_farmer = false; flag_police = false;
        flag_final = false;
        audioSource = GetComponent<AudioSource>();

        container_txt_tiempo_logrado.SetActive(false);
        //tiempo_limite = tiempo_inicial;
    }

    void FixedUpdate()
    {
        if (flag_teacher == true & flag_final)
        {
            flag_teacher = false;
            txt_ganaste.text = "Very Good " + Seleccion_nombre.nombreUsuario + "!";
            //audioSource.PlayOneShot(very_good);
            flag_very_good = false;
            container_txt_tiempo_logrado.SetActive(true);
            tiempo_final = tiempo_inicial;
            txt_tiempo_logrado.text = "You win in: " + (tiempo_final).ToString("00:00");
            time_flag = false;
            PlayerMovement.PostToDatabase();
            StartCoroutine(CambioEscena());
        }
    }

    void Update()
    {
        if (time_flag)
            tiempo_inicial = tiempo_inicial + 1 * Time.deltaTime;
        timer.text = tiempo_inicial.ToString("00:00");
        if (tiempo_inicial > 260)
            timer.color = Color.red;

        if (tiempo_inicial > tiempo_limite)
        {
            SceneManager.LoadScene("Profesiones");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (teacher && flag_teacher == false && other.CompareTag("Chair"))
        {
            music.PlayOneShot(monica_hi, 1);
            txt_dialogos.text = "Teacher: Hi, my name is Monica. I am a teacher. " +
                "Find Carla, the nurse.";
            flag_teacher = true;
            teacher = false;
            nurse = true;
        }
        if (nurse && flag_teacher && other.CompareTag("Car"))
        {
            music.PlayOneShot(carla_hi, 1);
            txt_dialogos.text = "Nurse: Hello, my name is Carla. I am a nurse. " +
                "Find John, the policeman.";
            flag_nurse = true;
            flag_teacher = false;
            nurse = false;
            police = true;
        }
        if (police && flag_nurse && other.CompareTag("Box"))
        {
            music.PlayOneShot(john_hi, 1);
            txt_dialogos.text = "Policeman: Hello!. My name is John. I am a policeman. " +
                "Find Marcos, the farmer.";
            flag_police = true;
            police = false;
            farmer = true;
            flag_nurse = false;
        }
        if (farmer && flag_police && other.CompareTag("Ladder"))
        {
            music.PlayOneShot(marcos_hi_bye, 1);
            txt_dialogos.text = "Farmer: Hi!. My name is Marcos. I am a farmer. " +
                "Find John. See you later!";
            flag_farmer = true;
            farmer = false;
            police = true;
        }
        if (police && flag_farmer && other.CompareTag("Box"))
        {
            music.PlayOneShot(john_bye, 1);
            txt_dialogos.text = "Policeman: Find Carla. Goodbye!.";
            flag_police = false;
            police = false;
            nurse = true;
            flag_nurse = true;
        }
        if (nurse && flag_nurse && other.CompareTag("Car"))
        {
            music.PlayOneShot(carla_bye,1);
            txt_dialogos.text = "Nurse: Find Monica. See you!";
            flag_teacher = true;
            nurse = false;
            teacher = true;
        }
        if (teacher && flag_teacher && other.CompareTag("Chair"))
        {
            music.PlayOneShot(monica_bye,1);
            txt_dialogos.text = "Teacher: Very good!";
            flag_teacher = true;
            teacher = false;
            nurse = false;
            farmer = false;
            police = false;
            flag_final = true;
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
