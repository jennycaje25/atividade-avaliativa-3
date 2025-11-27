using UnityEngine;

public class PlataformaMover : MonoBehaviour
{
    public Transform[] pontos; 
    public float velocidade = 2f;

    private int indice = 0;

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            pontos[indice].position,
            velocidade * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, pontos[indice].position) < 0.1f)
        {
            indice++;

            if (indice >= pontos.Length)
                indice = 0; // volta para o primeiro ponto
        }
    }
}