using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{      
    Rigidbody2D rb; 
    //Jump values
    [SerializeField] int JumpStrength;
    [SerializeField] float fastFall;
    
    Vector2 grav;
    //inputs
    public InputAction inputAction;
    public InputAction Left;
   
    public Transform groundCheck;
    public LayerMask groundlevel;

    bool onGround(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal,0,groundlevel);
    }
    void Start()
    {
        inputAction.Enable();
        RightLeft.Enable();
        rb = GetComponent<Rigidbody2D>();
        grav = new Vector2(0, -Physics2D.gravity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(inputAction.IsPressed() && onGround()){
            rb.velocity = new Vector2(rb.velocity.x, JumpStrength);
        }
        if(rb.velocity.y < 0){
            rb.velocity -= grav * fastFall * Time.deltaTime;
        }

    }
}
    //checks player is grounded before allowing jumps
    