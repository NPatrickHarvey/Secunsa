using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transportador : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Colores")
        {
            SceneManager.LoadScene("Coleccion_objetos");
        }
        if (other.gameObject.name == "Profesiones")
        {
            SceneManager.LoadScene("Profesiones");
        }
        if (other.gameObject.name == "Objetos")
        {
            SceneManager.LoadScene("Selección_objetos");
        }
        
    }

}
