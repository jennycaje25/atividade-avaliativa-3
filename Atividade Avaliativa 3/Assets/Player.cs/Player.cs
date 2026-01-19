using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 40;
    public float forcaDoPulo = 4;
    public float forcaDoSegundoPulo = 2.5f; // força do pulo flutuante

    private bool noChao = false;
    private bool andando = false;
    private int pulosRestantes = 1; // controla o pulo extra

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    private float posicaoAntiga;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        posicaoAntiga = transform.position.y;
    }

    void Update()
    {
        andando = false;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = true;
            andando = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = false;
            andando = true;
        }

        // --- PULO E PULO DUPLO ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (noChao)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reseta a velocidade vertical
                rb.AddForce(new Vector2(0, forcaDoPulo), ForceMode2D.Impulse);
                noChao = false;
                pulosRestantes = 1; // permite o segundo pulo
            }
            else if (pulosRestantes > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(new Vector2(0, forcaDoSegundoPulo), ForceMode2D.Impulse);
                pulosRestantes--;
            }
        }

        // animações
        animator.SetBool("Andando", andando);
        animator.SetBool("Pulo", !noChao);

        bool caindo = posicaoAntiga > transform.position.y;
        animator.SetBool("Caindo", !noChao && caindo);

        posicaoAntiga = transform.position.y;
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
            pulosRestantes = 1; // reseta os pulos quando toca no chão
        }
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }

    private void OnCollisionStay2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }
}