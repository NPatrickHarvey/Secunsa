using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class User
{
    public string nombreUsuario;
    public string emailUsuario;
    public float tiempo_ColeccionObjetos=0;
    public float tiempo_Profesiones=0;
    public float tiempo_SeleccionObjetos=0;

    public static int nivel_index = 0;

    public static bool flag_musica = true;

    public User()
    {
        nombreUsuario = Seleccion_nombre.nombreUsuario;
        emailUsuario = Seleccion_nombre.emailUsuario;
        tiempo_ColeccionObjetos = PlayerController.tiempo_final;
        tiempo_Profesiones = Profesiones_Collision.tiempo_final;
        tiempo_SeleccionObjetos = PlayerCollision.tiempo_final;
    }
}
