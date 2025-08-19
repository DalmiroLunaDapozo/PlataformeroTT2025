using UnityEngine;

public class AnimacionesJugador : MonoBehaviour
{
    private Animator animator;
    private MovimientoJugador movimiento;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        movimiento = GetComponentInParent<MovimientoJugador>();
    }


    void Update()
    {
        SetearAnimaciones();
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (movimiento.GetMovimiento() > 0)
            sprite.flipX = false;
        else if (movimiento.GetMovimiento() < 0)
            sprite.flipX = true;
    }

    private void SetearAnimaciones()
    {
        bool estaEnElSuelo = movimiento.GetSalto();
        float velocidadX = Mathf.Abs(movimiento.GetMovimiento());

        if (!estaEnElSuelo)
        {
            animator.SetBool("movimiento", false);
            animator.SetBool("salto", true);
        }
        else if (velocidadX > 0.01f)
        {
            animator.SetBool("salto", false);
            animator.SetBool("movimiento", true);
        }
        else
        {
            animator.SetBool("salto", false);
            animator.SetBool("movimiento", false);
        }
    }


}