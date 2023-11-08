using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
            // Rotar sprite
            if (rb.velocity.x > 0.1f)
                GetComponent<SpriteRenderer>().flipX = false;
            else if (rb.velocity.x < -0.1f)
                GetComponent<SpriteRenderer>().flipX = true;

            // Salto
            if (Input.GetButtonDown("Jump")) {
                rb.AddForce(transform.up * jumpForce);
            }

            // Da valor a los parámetros de la máquina de estados del Animator
            anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));   //(en valor absoluto)
            anim.SetFloat("velocityY", rb.velocity.y);
        }
        
    }
}
