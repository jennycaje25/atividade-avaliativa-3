using System;
using UnityEngine;

public class SapoInimigo : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 2f;
    public float forcaDoPulo = 5f;
    public float tempoEntrePulos = 2f;
    public Transform[] pontosDePatrulha; // agora é um array de pontos (A, B, C...)
    
    [Header("Dano")]
    public int dano = 1;
    public float tempoEntreAtaques = 1f;

    private int indiceAtual = 0;
    private bool noChao = false;
    private bool caindo = false;
    private bool olhandoDireita = true;
    private bool podeAtacar = true;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    private float posicaoAntigaY;
    private float contadorPulo;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        posicaoAntigaY = transform.position.y;
        contadorPulo = tempoEntrePulos;
    }

    void Update()
    {
        if (pontosDePatrulha.Length == 0) return;

        // --- Movimento entre pontos ---
        Transform destino = pontosDePatrulha[indiceAtual];
        float distancia = destino.position.x - transform.position.x;

        // Quando chega perto, muda para o próximo ponto
        if (Mathf.Abs(distancia) < 0.3f)
        {
            indiceAtual++;
            if (indiceAtual >= pontosDePatrulha.Length)
                indiceAtual = 0; // volta para o primeiro ponto

            VirarPara(pontosDePatrulha[indiceAtual]);
        }

        // --- Pulo automático ---
        if (noChao)
        {
            contadorPulo -= Time.deltaTime;
            if (contadorPulo <= 0f)
            {
                Pular();
                contadorPulo = tempoEntrePulos;
            }
        }

        // --- Animações ---
        caindo = transform.position.y < posicaoAntigaY && !noChao;
        animator.SetBool("Parado", noChao && !caindo);
        animator.SetBool("Pulo", !noChao && !caindo);
        animator.SetBool("Caindo", caindo);

        posicaoAntigaY = transform.position.y;
    }

    void Pular()
    {
        float direcao = olhandoDireita ? 1f : -1f;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(velocidade * direcao, forcaDoPulo), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
            noChao = true;

        if (colisao.gameObject.CompareTag("Player"))
            CausarDano(colisao.gameObject);
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
            noChao = false;
    }

    void VirarPara(Transform destino)
    {
        bool deveOlharDireita = destino.position.x > transform.position.x;

        if (deveOlharDireita != olhandoDireita)
        {
            olhandoDireita = deveOlharDireita;
            sprite.flipX = !sprite.flipX;
        }
    }

    void CausarDano(GameObject jogador)
    {
        if (!podeAtacar) return;

        PlayerVida vida = jogador.GetComponent<PlayerVida>();
        if (vida != null)
            podeAtacar = false;
            Invoke(nameof(PermitirNovoAtaque), tempoEntreAtaques);
    }

    void PermitirNovoAtaque()
    {
        podeAtacar = true;
    }

    void OnDrawGizmosSelected()
    {
        // Desenha as linhas de patrulha
        if (pontosDePatrulha != null && pontosDePatrulha.Length > 1)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < pontosDePatrulha.Length - 1; i++)
            {
                if (pontosDePatrulha[i] != null && pontosDePatrulha[i + 1] != null)
                    Gizmos.DrawLine(pontosDePatrulha[i].position, pontosDePatrulha[i + 1].position);
            }
        }
    }
}

internal class PlayerVida
{
}