using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Opciones de Movimiento")]

    public float velocidadDeMovimiento;
    public float fuerzaDeSalto;
    public bool estaEnElSuelo;

    private float ejeX;

    private Rigidbody2D fisicas;

    [SerializeField] private LayerMask layerSuelo;
    [SerializeField] private GameObject humoSalto;
    [SerializeField] private ParticleSystem humoCaminar;

    private AudioSource audioSalto;

    void Start()
    {
        fisicas = GetComponent<Rigidbody2D>();
        audioSalto = GetComponent<AudioSource>();
    }

    void Update()
    {
        ejeX = Input.GetAxis("Horizontal");
        Salto();
        ManejarHumo();
    }

    private void FixedUpdate()
    {
        Movimiento();
        DeteccionSuelo();
       
    }

    private void Movimiento()
    {
        fisicas.velocity = new Vector2(ejeX * velocidadDeMovimiento, fisicas.velocity.y);
    }

    private void DeteccionSuelo()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, layerSuelo);

        if (hit.collider != null)
        {
            estaEnElSuelo = true;
        }
        else
            estaEnElSuelo = false;

    }

    private void Salto()
    {
        if (Input.GetButtonDown("Jump") && estaEnElSuelo)
        {
            fisicas.AddForce(Vector2.up * fuerzaDeSalto);
            Instantiate(humoSalto, transform.position - new Vector3(0, 0.5f,0), transform.rotation);
            audioSalto.Play();
        }
    }

    public float GetMovimiento()
    {
        return ejeX;
    }

    public bool GetSalto()
    {
        return estaEnElSuelo;
    }

    private void ManejarHumo()
    {
        if (GetMovimiento() != 0 && GetSalto())
            humoCaminar.Play();
        else
            humoCaminar.Stop();
    }

}

