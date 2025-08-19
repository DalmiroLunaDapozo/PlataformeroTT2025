using UnityEngine;

public class Patrulla : MonoBehaviour
{

    public float velocidad = 2f;
    public float distanciaRaycastPared = 0.5f;
    public float distanciaRaycastSuelo = 1f;
    public LayerMask layerTerreno;

    private bool moviendoDerecha = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Mover();
        DetectarObstaculos();
    }

    void Mover()
    {
        rb.velocity = new Vector2((moviendoDerecha ? 1f : -1f) * velocidad, rb.velocity.y);
    }

    void CambiarDireccion()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void DetectarObstaculos()
    {
        float direccion = moviendoDerecha ? 1f : -1f;
        Vector2 pos = transform.position;

        bool hayPared = Physics2D.Raycast(transform.position, Vector2.right * direccion, distanciaRaycastPared, layerTerreno);
        bool sinSuelo = !Physics2D.Raycast(pos + new Vector2(0.5f * direccion, -0.5f), Vector2.down, distanciaRaycastSuelo, layerTerreno);

        Debug.DrawRay(pos, Vector2.right * direccion * distanciaRaycastPared, Color.red);
        Debug.DrawRay(pos + new Vector2(0.5f * direccion, -0.5f), Vector2.down * distanciaRaycastSuelo, Color.blue);

        if (hayPared || sinSuelo)
            CambiarDireccion();
    }

}
