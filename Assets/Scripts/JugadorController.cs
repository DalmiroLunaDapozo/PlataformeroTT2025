using UnityEngine;
using System;

public class JugadorController : MonoBehaviour
{

    public event EventHandler enemigoTocado;
    public event EventHandler jugadorEnLaMeta;
    public event EventHandler<Transform> checkpointTocado;

    private bool estaVivo;

   
    void Start()
    {
        SetEstaVivo(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            enemigoTocado?.Invoke(this, EventArgs.Empty);
        }
        
        if (collision.gameObject.CompareTag("Meta"))
        {
            jugadorEnLaMeta?.Invoke(this, EventArgs.Empty);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            checkpointTocado?.Invoke(this, collision.gameObject.transform);
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Animator>().SetTrigger("On");
        }
    }


    public bool GetEstaVivo()
    {
        return estaVivo;
    }

    public void SetEstaVivo(bool estado)
    {
        estaVivo = estado;
    }

}
