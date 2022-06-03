using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        // Get RigidBody
        body = GetComponent<Rigidbody2D>();

        // Get Animations
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get left and right button inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip the character left or right 
        if(horizontalInput > .01f)
            transform.localScale = Vector3.one;
        else if(horizontalInput < -.01f)
            transform.localScale = new Vector3(-1, 1, 1);        


        // Jumping with 'W' or 'SpaceBar'
        //if(Input.GetKey(KeyCode.W) && grounded)
        //    Jump();
        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        /*
         Set parameters for animations
        */
    
        // if no input for horizontal then is idle
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
        

        
    }

    
    private void Jump(){
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");
        grounded = false;
    }

    // Check if player has touched ground
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
