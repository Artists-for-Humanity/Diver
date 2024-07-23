using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{      
    Rigidbody2D rb; 
    //Jump values
    [SerializeField] int jumpStrength;
    [SerializeField] float fastFall;
    [SerializeField] float moveSpeed;
   
    
    Vector2 grav;
    //inputs
    public InputAction up;
    public InputAction left;
    public InputAction right;
   
    public Transform groundCheck;
    public LayerMask groundlevel;
    //checks if player is on ground
    bool onGround(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal,0,groundlevel);
    }
    void Start()
    {
        up.Enable();
        left.Enable();
        right.Enable();
        rb = GetComponent<Rigidbody2D>();
        grav = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(up.IsPressed() && onGround()){
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
            
        }
        
        if(rb.velocity.y < 0){
            rb.velocity -= grav * fastFall * Time.deltaTime;
        }
        
        if(left.IsPressed()){
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
         } else if (!(left.IsPressed()) && !(right.IsPressed())) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }


        if(right.IsPressed()){
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        } else if (!(left.IsPressed()) && !(right.IsPressed())) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }
}
    
    