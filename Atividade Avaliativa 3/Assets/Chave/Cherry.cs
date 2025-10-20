using System;
using UnityEngine;
public class Cherry : MonoBehaviour
{
   // public GameObject textoDeVitoria;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         //   textoDeVitoria.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}