using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDoorsPan : MonoBehaviour
{

    public Camera cam;
    private Vector3 cam_pos;
    private Vector3 pos_prueba;
    public float cam_speed;

    public GameObject prueba;

    List<Vector3> lista_posiciones;
    Vector3 pos_inicial = new Vector3(23, 1, -11); //-12.492,0,0
    Vector3 pos_door1 = new Vector3(37.90194f, 1.329939f, -8.764511f); //-3.268,0,0
    Vector3 pos_door2 = new Vector3(40, 1, -13); //-12.44, 90, 0
    Vector3 pos_door3 = new Vector3(20, 0, 0);
    Vector3 pos_door4 = new Vector3(30, 0, 0);
    Vector3 pos_door5 = new Vector3(30, 0, 0);
    Vector3 pos_door6 = new Vector3(30, 0, 0);
    Vector3 pos_door7 = new Vector3(30, 0, 0);
    Vector3 pos_door8 = new Vector3(30, 0, 0);

    List<Vector3> lista_rotaciones;
    Vector3 rot_inicial = new Vector3(-15, 0, 0);
    Vector3 rot_door1 = new Vector3(-15, 0, 0);
    Vector3 rot_door2 = new Vector3(-12, 90, 0);
    Vector3 rot_door3 = new Vector3(-12, 90, 0);
    Vector3 rot_door4 = new Vector3(-12, 90, 0);
    Vector3 rot_door5 = new Vector3(-12, 90, 0);



    void Start()
    {
        lista_posiciones = new List<Vector3>();
        lista_posiciones.Add(pos_inicial);
        lista_posiciones.Add(pos_door1);
        lista_posiciones.Add(pos_door2);
        lista_posiciones.Add(pos_door3);
        lista_posiciones.Add(pos_door4);
        lista_posiciones.Add(pos_door5);
        lista_posiciones.Add(pos_door6);
        lista_posiciones.Add(pos_door7);
        lista_posiciones.Add(pos_door8);

        lista_rotaciones = new List<Vector3>();
        lista_rotaciones.Add(rot_inicial);
        lista_rotaciones.Add(rot_door1);
        lista_rotaciones.Add(rot_door2);


        //cam = GetComponent<Camera>();
        cam_pos = cam.transform.position;
        pos_prueba = new Vector3(0, 0, 0);
    }

    public void IncrementarIndex()
    {
        Debug.Log("aumentar");
        if(User.nivel_index==8)
        {
            User.nivel_index = 0;
        }
        else
        {
            User.nivel_index++;
        }
        Debug.Log(User.nivel_index);
        StartCoroutine(LerpFromTo(cam.transform.position, lista_posiciones[User.nivel_index], cam.transform.eulerAngles, lista_rotaciones[User.nivel_index],cam_speed));
        //cam_pos = Vector3.Lerp(cam_pos, pos_prueba, Time.deltaTime * cam_speed);
        //cam.transform.position = cam_pos;
    }
    public void DecrementarIndex()
    {
        Debug.Log("decrementar");
        if(User.nivel_index==0)
        {
            User.nivel_index = 8;
        }
        else
        {
            User.nivel_index--;
        }
        Debug.Log(User.nivel_index);
    }


    IEnumerator LerpFromTo(Vector3 position1, Vector3 position2, Vector3 pos_rotation1, Vector3 pos_rotation2, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            cam.transform.position = Vector3.Lerp(position1, position2, t / duration);
            //cam.transform.eulerAngles = Vector3.Lerp(pos_rotation1, pos_rotation2, t / duration);
            //cam.transform.Rotate(lista_rotaciones[User.nivel_index], t / duration);
            yield return 0;
        }


        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            //cam.transform.position = Vector3.Lerp(position1, position2, t / duration);
            //cam.transform.eulerAngles = Vector3.Lerp(pos_rotation1, pos_rotation2, t / duration);
            cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, prueba.transform.rotation, t / duration);
            //yield return 0;
        }
        //cam.transform.position = position2;
        //cam.transform.LookAt(prueba.transform);
        //cam.transform.Rotate(lista_rotaciones[User.nivel_index], Time.deltaTime * 10.0f);
        //cam.transform.eulerAngles = pos_rotation2;
    }
}
