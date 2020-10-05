using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class EnemyController : MonoBehaviour
{
    //Variables
    [SerializeField] private GameObject enemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim; 
    [SerializeField] private float leftEndPoint;
    [SerializeField] private float rightEndPoint;
    [SerializeField] private int horizontalSpeed = 3;

    [SerializeField] private bool walkLeft;
    [SerializeField] private bool walkRight;
    //private State state = State.idle;

    

    // Start is called before the first frame update
    void Start()
    {
         walkLeft = true;
         walkRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        AssigningValues();
        EnemyWalking();
        WalkingLeft();
        WalkingRight();
        
    }

    void EnemyWalking() {
        if (rb.position.x >= this.rightEndPoint) {
            //push left
            this.walkRight = false;
            this.walkLeft = true;
            
        }
        else if (rb.position.x <= this.leftEndPoint) {
            //push right
            this.walkLeft = false;
            this.walkRight = true;
        }
    }

    void WalkingLeft() {
        if (this.walkLeft) {
            
            rb.velocity = new Vector2 (-horizontalSpeed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void WalkingRight() {
        if (this.walkRight) {
            
            rb.velocity = new Vector2 (horizontalSpeed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
    }

    void AssigningValues(){
        switch (enemy.name)
        {
            case ("Semiramide"):
                this.leftEndPoint = 2.72f;
                this.rightEndPoint = 8.34f;
                break;  
            
            case ("Cleopatra"):
                this.leftEndPoint = 15f;
                this.rightEndPoint = 24f;
                break;
                     
            case ("Elena"):
                this.leftEndPoint = 32f;
                this.rightEndPoint = 35f;
                break;
            case ("Paride"):
                this.leftEndPoint = 36.2f;
                this.rightEndPoint = 39f;
                break;
            case ("Achille"):
                this.leftEndPoint = 51f;
                this.rightEndPoint = 55.3f;
                break;
            case ("Tristano"):
                this.leftEndPoint = 56.5f;
                this.rightEndPoint = 59.5f;
                break;
            default:
                Debug.Log("enemy error");
                break;
        } 
    }
}
