using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Variables 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;        
    [SerializeField] private Collider2D collider;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Text collectableCounter;

    [SerializeField]private int horizontalSpeed = 4;
    [SerializeField]private int verticalSpeed = 15;
    [SerializeField] public int crosses = 0;

    private enum State {idle, walking, jumping, falling};
    private State state = State.idle;

    

    //Standard methods
    void Start(){
        //not sure why
       rb.GetComponent<Rigidbody2D>(); 
       anim.GetComponent<Animator>();
       collider.GetComponent<Collider2D>();

    }

    void Update(){

        CheckMovement();
        StateSwitcher();
        UpdateCollectableCounter();
        //fix this
        anim.SetInteger("state", (int)state);


    }


    //My methods
    void CheckMovement() {
            
        CheckJumping();
        Walking();
        Falling();

    }

    void Walking() {

        float horizontalDirection = Input.GetAxis("Horizontal");

        if (horizontalDirection != 0 ) {
            rb.velocity = new Vector2 (HorizontalVelocity(), rb.velocity.y);
            FlippigCharacter();
        }
        
    }

    void CheckJumping() {
        
        //put this in jump function
        float verticalDirection = Input.GetAxis("Jump");

        if ((verticalDirection > 0) && (collider.IsTouchingLayers(ground))) {

            Jump();
            
        }

    }

    void Jump() {

        rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);
        state = State.jumping;

    }

    void Falling(){

        if (rb.velocity.y < -0.1f) {

            state = State.falling;

        }

    } 

    //a mess
    void StateSwitcher() {

        if (state == State.falling) {
            
            if (collider.IsTouchingLayers(ground)) {
                state = State.idle;
            }

        }
        else if(state == State.jumping) {
            Falling();
        }
        else if(Mathf.Abs(rb.velocity.x) > 1f) {
            //moving
            state = State.walking;
            
        }

        else {
            state = State.idle;
        }

    }

    int HorizontalVelocity() {

        float horizontalDirection = Input.GetAxis("Horizontal");

        return NegativeOrPositiveDirection() * horizontalSpeed;

    }

    void FlippigCharacter() {

        float horizontalDirection = Input.GetAxis("Horizontal");

        transform.localScale = new Vector2(NegativeOrPositiveDirection(), 1);

    }


    int NegativeOrPositiveDirection (){

        float horizontalDirection = Input.GetAxis("Horizontal");
        
        if (horizontalDirection >= 0) {
            return 1;
        }
        else {
            return -1;
        }

    }

    //triggering crosses collectibles
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Collectable") {
            Destroy(collision.gameObject);
            crosses += 1;
            //deleted updatecolectablecounter
        }

    }

    private void UpdateCollectableCounter() {
        collectableCounter.text = crosses.ToString() + " / 5";
    }

    //colliding with enemies
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            EnemyDamage(other);
            
        }
    }

    private void EnemyDamage(Collision2D other) {
        //damaging enemies
        if (state == State.falling) {
            Destroy(other.gameObject);
            Jump();

        }

        //getting damage
        else {
            
            GettingDamage(other);

        }
    }

    private void GettingDamage(Collision2D other) {


    }

}

// fix floating problem
// set boundaries
// figure out how to add suggestions when coding
// add going down animation from platforms
// fix going up walls