using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //movement variables
    public float runSpeed;
    public float walkSpeed;

    Rigidbody myRB;
    Animator myAnim;

    bool facingRight;

    //for jumping
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(grounded && Input.GetAxis("Jump")>0)
        {
            grounded = false;
            myAnim.SetBool("grounded", grounded);
            myRB.AddForce(new Vector3(0, jumpHeight, 0));
        }

        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if(groundCollisions.Length > 0 ) grounded = true;
        else grounded = false;

        myAnim.SetBool("grounded", grounded);

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed",Mathf.Abs(move));

        float sneaking = Input.GetAxisRaw("Fire3");
        myAnim.SetFloat("sneaking", sneaking);

        if (sneaking > 0 && grounded)
        {
            myRB.velocity = new Vector3(move * walkSpeed, myRB.velocity.y, 0);
        }
        else
        {
            myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
        }

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }

    public float GetFacing()
    {
        if (facingRight) return 1;
        else return -1;
    }
}