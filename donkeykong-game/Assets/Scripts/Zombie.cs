using UnityEngine;

public class Zombie : MonoBehaviour
{
    //i dont think this grounded stuff matters bc zombies cant jump


    [SerializeField] bool grounded = false;

    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;

    void FixedUpdate()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        grounded = false;

        // see if groundcheck object is colliding with other
        // 2D colliders in the "Ground" layer
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            grounded = true;
    }
}
