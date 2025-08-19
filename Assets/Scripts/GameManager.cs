using UnityEngine;
using Cinemachine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject jugador;
    [SerializeField] private CinemachineVirtualCamera camara;
    [SerializeField] private GameObject vfxMuerte;
    [SerializeField] private Animator uiAnimator;

    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private AudioSource sonidos_sfx;

    private Transform puntoDeAparicion;
    private GameObject instanciaJugador;

    private JugadorController jugadorController;

    public bool esNivelFinal;

    private void Awake()
    {
        puntoDeAparicion = GameObject.Find("PuntoDeAparicion").transform;
    }

    private void Start()
    {
        InicializarJugador();
    }

    private void InicializarJugador()
    {
        instanciaJugador = Instantiate(jugador, puntoDeAparicion.position, transform.rotation);
        camara.Follow = instanciaJugador.transform;

        jugadorController = instanciaJugador.GetComponent<JugadorController>();
        jugadorController.enemigoTocado += MatarJugador;
        jugadorController.jugadorEnLaMeta += TerminarNivel;
        jugadorController.checkpointTocado += NuevoCheckpoint;

        uiAnimator.SetTrigger("Abrir");

    }

    private void MatarJugador(object sender, EventArgs e)
    {
        ReproducirSonido("muerte_sfx");
        StartCoroutine(SecuenciaMuerte());
    }

    private void TerminarNivel(object sender, EventArgs e)
    {
        StartCoroutine(SecuenciaGanar());
    }


    private IEnumerator SecuenciaMuerte()
    {
        Vector2 pos = instanciaJugador.transform.position;
        Instantiate(vfxMuerte, pos, transform.rotation);

        jugadorController.enemigoTocado -= MatarJugador;

        uiAnimator.SetTrigger("Cerrar");

        Destroy(instanciaJugador);

        yield return new WaitForSeconds(2f);

        InicializarJugador();

    }

    private IEnumerator SecuenciaGanar()
    {
        jugadorController.jugadorEnLaMeta -= TerminarNivel;

        jugadorController.SetEstaVivo(false);
        jugadorController.GetComponent<Rigidbody2D>().gravityScale = 0;
    
        uiAnimator.SetTrigger("Cerrar");

        yield return new WaitForSeconds(2f);

        if (esNivelFinal)
            ReiniciarJuego();
        else
            SiguienteNivel();

    }

    private void ReproducirSonido(string sonido)
    {
        int new_clip = clips.FindIndex(i => i.name == sonido);
        sonidos_sfx.clip = clips[new_clip];
        sonidos_sfx.Play();

    }

    private void NuevoCheckpoint(object sender, Transform e)
    {
        puntoDeAparicion = e;
    }

    private void SiguienteNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ReiniciarJuego()
    {
        SceneManager.LoadScene(0);
    }

}