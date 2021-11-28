using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatosJugador : MonoBehaviour
{
    //este script debe ir en un objeto que se encuentre desde el inicio del juego 
    //ya que debe viajar entre todas las escenas para guardar y para cargar
    //como en el GameManager 

    //aqui se debe poner todos los datos en tiempo real que se van a guardar
    //cantidad de guardadas
    public int Saves;
    public float percentMap;
    //posicion del jugador
    public Vector3 posPlayer;
    //vida maxima y corazones actuales
    public float maxHealth;
    public int currentHearts;
    //Oro
    public int gold;
    public int costRespawn;
    //llaves
    [Header("LLAVES")]
    public bool blueKey = false;
    public bool cianKey = false;
    public bool redKey = false;
    public bool yellowKey = false;
    public bool pinkKey = false;
    public bool greenKey = false;
    //latigos
    public int typeWhip;
    //subarmas
    public int typeSub;
    public bool haveSub;
    public int multiplierPow;

    //items del mapa
    //cuando se agreguen mas items tambien poner mas booleanos con el nombre wasCaughtItem y aumentar el numero respectivamente
    [Header("ITEMS UNICOS DEL MAPA")]
    public bool wasCaughtItem1;
    public bool wasCaughtItem2;
    public bool wasCaughtItem3;

    

    //partes del mapa
    [Header("PARTES DEL MAPA")]
    public bool mapPart0;
    public bool mapPart1;
    public bool mapPart2;
    public bool mapPart3;
    public bool mapPart4;
    public bool mapPart5;
    public bool mapPart6;
    public bool mapPart7;
    public bool mapPart8;
    public bool mapPart9;
    public bool mapPart10;
    public bool mapPart11;
    public bool mapPart11B;
    public bool mapPart12;
    public bool mapPart13;
    public bool mapPart14;
    public bool mapPart15;
    public bool mapPart16;
    public bool mapPart17;
    public bool mapPart18;
    public bool mapPart19;
    public bool mapPart20;
    public bool mapPart21;
    public bool mapPart22;
    public bool mapPart23;

    //estado de paredes rompibles del mapa
    [Header("PAREDES ROMPIBLES DEL MAPA")]
    public bool wallBroken1;
    public bool wallBroken2;

    //estado de los boss del mapa
    //cuando se agreguen mas boss, tambien agregar su respectivo booleano
    [Header("BOSS UNICOS DEL MAPA")]
    public bool bossBatDefeated;
    public bool bossMedusaDefeated;
    public bool bossMummyA;
    public bool bossMummyB;

    //estado de eventos del mapa
    //cuando se agreguen mas eventos, tambien agregar su respectivo booleano
    [Header("EVENTOS DEL MAPA")]
    public bool pitActive;
    public bool floorBrokenLevel3;
}
