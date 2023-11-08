using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Connection : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;    // Sincroniza la escena
    }

    override
    public void OnConnectedToMaster() {
        print("Conectado al master");
    }

    //Función que asignamos y se ejecuta al pulsar el botón Connect
    //Creamos o nos unimos a una sala (4 jugadores max) y tipo por defecto
    public void buttonConnect() {
        RoomOptions options = new RoomOptions() { MaxPlayers = 4  };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }

    //Función que se ejecuta al conectarse al servidor
    override
    public void OnJoinedRoom() {
        Debug.Log("Conectado a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("hay " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores conectados");
    }

    private void Update() {
        // Si eres el Master y hay más de un jugador
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            // Cambia de escena
            PhotonNetwork.LoadLevel(1);
            Destroy(this);  // Destruye el Script cuando se carga la nueva escena (para que no cargue infinitas)
        }
    }
}