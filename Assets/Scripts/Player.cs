using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed, jumpForce;

    private Rigidbody2D rb;
    private Animator anim;

    private void Start() {
        // Si el objeto es mio, instancia
        if (GetComponent<PhotonView>().IsMine) {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.position = transform.position + (Vector3.up) + (transform.forward* -10);
        }
    }

    private void Update() {
        // Si el objeto es mio, lo controlo
        if (GetComponent<PhotonView>().IsMine) {
            // Mover jugador
            rb.velocity = (transform.right * speed * Input.GetAxis("Horizontal")) +
                            (transform.up * rb.velocity.y);

            // Rotar sprite - Solo cuando la velocidad es +-0.1 y cuando cambia de dirección
            if (rb.velocity.x > 0.1f && GetComponent<SpriteRenderer>().flipX)
                // Remote Procedure Call (función a ejecutar, a quién, parámetro)
                // El servidor hace que todos los ugadores ejecuten la función pasándole ese parámetro
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, false);
            else if (rb.velocity.x < -0.1f && !GetComponent<SpriteRenderer>().flipX)
                GetComponent<PhotonView>().RPC("RotateSprite", RpcTarget.All, true);

            // Salto
            if (Input.GetButtonDown("Jump")) {
                rb.AddForce(transform.up * jumpForce);
            }

            // Da valor a los parámetros de la máquina de estados del Animator
            anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));   //(en valor absoluto)
            anim.SetFloat("velocityY", rb.velocity.y);
        }
    }

    [PunRPC]    // Indica que la función se va a usar en el Remote Procedure Call
                // Sincroniza la variable en todas las instancias del juego
    void RotateSprite(bool rotate) {
        GetComponent<SpriteRenderer>().flipX = rotate;
    }
}
