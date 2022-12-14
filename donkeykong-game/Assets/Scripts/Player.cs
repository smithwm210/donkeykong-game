using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    //private SpriteRenderer spriteRenderer;
    //public Sprite[] runSprites;
    //public Sprite climbSprite;
    //private int spriteIndex;
    //private new Collider2D collider;
    //private Collider2D[] results;
    //private Vector2 direction;

    private new Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;
    public float moveSpeed = 1;
    [SerializeField] float jumpStrength = 150;
    private float horizontalValue;

    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField] bool grounded = false;
    private bool jump = false;
        //private bool climbing;
    private bool facingLeft = true;
    private bool dead = false;


    private void Awake() {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
            //collider = GetComponent<Collider2D>();
            //results = new Collider2D[4];
        animator = GetComponent<Animator>();
    }

    //private void OnEnable() {
        //InvokeRepeating(nameof(AnimateSprite), 1f/12f, 1f/12f);
    //}

    //private void OnDisable() {
        //CancelInvoke();
    //}

    private void CheckCollision() {

        //climbing = false;

        //Vector2 size = collider.bounds.size;
        //size.y += 0.25f;
        //size.x /= 2f;

        //int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results);

        //for (int i=0; i<amount; i++) {
            //GameObject hit = results[i].gameObject;

            //if (hit.layer == LayerMask.NameToLayer("Ground")) {
                //grounded = hit.transform.position.y < (transform.position.y + 2f);
                //Physics2D.IgnoreCollision(GetComponent<Collider>(), results[i], !grounded);
            //}
            //else if (hit.layer == LayerMask.NameToLayer("Ladder")) {
                //climbing = true;
            //}
        //}
    }

    private void Update() {

        if(dead)
        {
            enabled = false;
            return;
        }

            //CheckCollision();

        // Store horizonal value
        horizontalValue = Input.GetAxis("Horizontal");

        // jump controller
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("Jump", true);
            jump = true;
        }
            
        else if (Input.GetButtonUp("Jump"))
            jump = false;


        // set the yVelocity in the animator
        animator.SetFloat("yVelocity", rigidbody.velocity.y);


        // Weapon code
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetFloat("Shoot", 1);
            StartCoroutine(Shoot());
        }



        //if (climbing) {
        //direction.y = Input.GetAxis("Vertical") * moveSpeed;
        // }

        // jump controller
        //else if (grounded && Input.GetButtonDown("Jump")) {
        //direction = Vector2.up * jumpStrength;
        //}
        //else {
        //direction += Physics2D.gravity * Time.deltaTime;
        //}

        //direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        // prevent gravity from compounding while grounded
        //if (grounded) {
        //direction.y = Mathf.Max(direction.y, -1f);
        //}

        // make player face the correct direction
        //if (direction.x < 0f) {
        //transform.eulerAngles = Vector3.zero;
        //}
        //else if (direction.x > 0f) {
        //transform.eulerAngles = new Vector3(0f, 180f, 0f);
        //}
    }

    private void FixedUpdate() {

        GroundCheck();
        Move(horizontalValue, jump);

        //rigidbody.MovePosition(rigidbody.position + direction * Time.fixedDeltaTime);
    }


    private void Move(float dir, bool jumpFlag)
    {

        // Set value of x using direction and speed
        float xVal = dir * moveSpeed * 100 * Time.fixedDeltaTime;

        Vector2 targetVelocity = new Vector2(xVal, rigidbody.velocity.y);

        // Set the player's velocity
        rigidbody.velocity = targetVelocity;

        if(facingLeft && dir > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            firePoint.eulerAngles = Vector3.zero;
            facingLeft = false;
        }
        else if(!facingLeft && dir < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            firePoint.eulerAngles = new Vector3(0f, 180f, 0f);
            facingLeft = true;
        }

        // Set the float according to the x velocity of the rigidbody
        animator.SetFloat("xVelocity", Mathf.Abs(rigidbody.velocity.x));


        if (grounded && jumpFlag)
        {
            grounded = false;
            jumpFlag = false;
            rigidbody.AddForce(new Vector2(0f, jumpStrength));
        }

    }


    IEnumerator Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(0.5f);
        animator.SetFloat("Shoot", 0);
    }


    private void GroundCheck()
    {
        grounded = false;

        // see if groundcheck object is colliding with other
        // 2D colliders in the "Ground" layer
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if(colliders.Length > 0)
            grounded = true;

        // if grounded the jump bool in animator is diabled
        animator.SetBool("Jump", !grounded);
    }




    //private void AnimateSprite() {
        //if (climbing) {
            //spriteRenderer.sprite = climbSprite;
        //}
        //else if (direction.x != 0f) {
            //spriteIndex++;
            //if(spriteIndex >= runSprites.Length) {
                //spriteIndex = 0;
            //}

           //spriteRenderer.sprite = runSprites[spriteIndex];
        //}
    //}

    //private void OnCollisionEnter2D(Collision2D collision) {
        //if (collision.gameObject.CompareTag("Objective")) {
            //enabled = false;
            //FindObjectOfType<GameManager>().LevelComplete();
        //}
        //else if (collision.gameObject.CompareTag("Obstacle")) {
            //enabled = false;
            //FindObjectOfType<GameManager>().LevelFailed();

            // FindObjectOfType<GameManager>().DecrementHealth();
        //}
    //}

    public void Die()
    {
        dead = true;
        FindObjectOfType<GameManager>().Restart();
    }

    private void OnDrawGizmoSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
    }

}
