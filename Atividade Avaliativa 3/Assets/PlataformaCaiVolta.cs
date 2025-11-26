using UnityEngine;

public class PlataformaCaiVolta : MonoBehaviour
{
    public float tempoParaCair = 0.5f;
    public float tempoParaVoltar = 2f;

    private Rigidbody2D rb;
    private Vector3 posicaoInicial;
    private bool caiu = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position; // guarda posição original
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!caiu && col.collider.CompareTag("Player"))
        {
            caiu = true;
            Invoke("Cair", tempoParaCair);
        }
    }

    void Cair()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Invoke("VoltarParaPosicao", tempoParaVoltar);
    }

    void VoltarParaPosicao()
    {
        // trava ela de novo
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero; // tira a velocidade da queda

        // resetar a posição
        transform.position = posicaoInicial;

        // disponível para cair novamente
        caiu = false;
    }
}