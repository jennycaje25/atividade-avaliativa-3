using System;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform postposicaoDeChegada;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colizor)
    {
        if (colizor.gameObject.tag == "Player")
        {
            colizor.gameObject.transform.position = postposicaoDeChegada.position;
        }
    }

}
