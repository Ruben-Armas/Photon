using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour {
    // Jugador 1 (principal) instancia en Pos Vector3 --> Frog
    // Jugador 2 instancia en Pos Vector3 --> Guy
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Frog", new Vector3(0, 0, 0), Quaternion.identity);
        else
            PhotonNetwork.Instantiate("Guy", new Vector3(-1, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
