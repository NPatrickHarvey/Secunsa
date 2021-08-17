using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float velocidad;
    private int count;

    int green_count;
    int blue_count;
    int red_count;

    public Text green_txt;
    public Text blue_txt;
    public Text red_txt;
    public Text txt_desaparecer;
    public Text txt_ganaste;
    public Image fondo_desaparecer;

    bool green, blue, red;
    bool flag_very_good;

    public AudioClip green_sound;
    public AudioClip red_sound;
    public AudioClip blue_sound;
    public AudioClip pick_up;
    public AudioClip very_good;
    public AudioClip error;
    AudioSource audioSource;

    public Text timer;
    public GameObject container_txt_tiempo_logrado;
    public Text txt_tiempo_logrado;
    float tiempo_inicial = -5;
    float tiempo_limite = 300;
    public static float  tiempo_final= 0;
    bool time_flag = true; // true para que continue el tiempo

    public int num_vidas = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocidad = 10;
        flag_very_good = true;
        green = true; blue = false; red = false;
        green_count = 5; blue_count = 4; red_count = 4;
        txt_ganaste.text = "";
        green_txt.text = "Green: 5";
        blue_txt.text = "";
        red_txt.text = "";

        audioSource = GetComponent<AudioSource>();
        DestroyObjectDelayed();

        container_txt_tiempo_logrado.SetActive(false);
        //tiempo_limite = tiempo_inicial;
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * velocidad);

        if (green_count == 0 && blue_count == 0 && red_count == 0 && flag_very_good)
        {
            txt_ganaste.text = "Very Good " + Seleccion_nombre.nombreUsuario + "!";
            audioSource.PlayOneShot(very_good);
            flag_very_good = false;
            container_txt_tiempo_logrado.SetActive(true);
            tiempo_final = tiempo_inicial;//tiempo_inicial - tiempo_actual;
            txt_tiempo_logrado.text = "You win in: " + (tiempo_final).ToString("00:00");
            time_flag = false;
            PlayerMovement.PostToDatabase();
            StartCoroutine(CambioEscena());
        }

    }

    void Update()
    {
        if(time_flag)
        tiempo_inicial = tiempo_inicial + 1 * Time.deltaTime;
        timer.text = tiempo_inicial.ToString("00:00");
        if (tiempo_inicial > 260)
            timer.color = Color.red;

        if (tiempo_inicial >tiempo_limite)//(tiempo_actual < 0.10)
        {
            SceneManager.LoadScene("Coleccion_objetos");
        }

        switch(num_vidas)
        {
            case 3: heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(true); break;
            case 2: heart1.SetActive(true); heart2.SetActive(true); heart3.SetActive(false); break;
            case 1: heart1.SetActive(true); heart2.SetActive(false); heart3.SetActive(false); break;
            case 0: SceneManager.LoadScene("Coleccion_objetos"); break;
        }

        if (green_count == 0) { green_txt.text = ""; blue_txt.text = "Blue: " + blue_count.ToString(); }
        if (blue_count == 0) { green_txt.text = ""; blue_txt.text = ""; red_txt.text = "Red: " + red_count.ToString(); }
        if (red_count == 0) { red_txt.text = ""; }
    }

    void OnTriggerEnter(Collider other)
    {
        if (green_count == 0) {green=false; blue = true;}
        if (blue_count == 0) { blue = false; red = true; }
        if (red_count == 0) { red = false;}

        if(other)
        {
            Debug.Log(num_vidas);
        }

        if (green && other.gameObject.CompareTag("PickUps_Green"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            green_count--;
            green_txt.text = "Green: " + green_count.ToString();
            audioSource.PlayOneShot(green_sound);
            audioSource.PlayOneShot(pick_up);
        }
        if(green && !other.gameObject.CompareTag("PickUps_Green"))
        {
            audioSource.PlayOneShot(error);
            num_vidas--;
        }
        //{ SceneManager.LoadScene("Coleccion_objetos"); }

        if (blue && other.gameObject.CompareTag("PickUps_Blue"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            blue_count--;
            blue_txt.text = "Blue: " + blue_count.ToString();
            audioSource.PlayOneShot(blue_sound);
            audioSource.PlayOneShot(pick_up);
        }
        if (blue && !other.gameObject.CompareTag("PickUps_Blue"))
        {
            audioSource.PlayOneShot(error);
            num_vidas--;
        }
        //else { SceneManager.LoadScene("Coleccion_objetos"); }

        if (red && other.gameObject.CompareTag("PickUps_Red"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            red_count--;
            red_txt.text = "Red: " + red_count.ToString();
            audioSource.PlayOneShot(red_sound);
            audioSource.PlayOneShot(pick_up);
        }
        else { count--; }//SceneManager.LoadScene("Coleccion_objetos"); }
    }

    void DestroyObjectDelayed()
    {
        // Kills the game object in 10 seconds after loading the object
        Destroy(txt_desaparecer, 5);
        Destroy(fondo_desaparecer, 5);
    }

    IEnumerator CambioEscena()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Seleccion_actividades");
    }

}
