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
    [SerializeField] float higherJump;
    [SerializeField] float jumpLimit;
    [SerializeField] float doubleJump;
    [SerializeField]float moveSpeed;
    Vector2 grav;
    private bool jumping;
    private float timeJumping;
    private float extraJumps;
    
    //inputs
    public InputAction up;
    public InputAction left;
    public InputAction right;
    public InputAction dashbutton;


    public Transform wallCheckLeft;
    public Transform groundCheck;
    public Transform wallCheckRight;
    public LayerMask groundlevel;

    private bool ableToDash = true;
    private bool dashing;
    public float dashStrength = 300.0f;
    public float dashLength = 0.3f;
    private float dashCooldown = 1f;

    //checks if player is on ground
    bool onGround(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal,0,groundlevel);
    }
    bool nextToRightWall(){
        return Physics2D.OverlapCapsule(wallCheckRight.position, new Vector2(0.01f, 1.0f), CapsuleDirection2D.Vertical,0,groundlevel);
    }
    bool nextToLeftWall(){
        return Physics2D.OverlapCapsule(wallCheckLeft.position, new Vector2(0.05f, 1.0f), CapsuleDirection2D.Vertical,0,groundlevel);
    }
    float movementDirection(){
        float moveInput = 0;
    if (left.IsPressed()){
        moveInput = -moveSpeed;
    }
    else if (right.IsPressed()){
        moveInput = moveSpeed;
    }
    return moveInput;
    }
    void jumpCalcs(){
        if(onGround()){
            extraJumps = doubleJump;
        }

        if(up.IsPressed() && onGround()){
            jumping = true;
            timeJumping = 0;
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        } 
        if(rb.velocity.y>0 && jumping){
            timeJumping +=Time.deltaTime;
            if(timeJumping>jumpLimit){
                jumping = false;
            }
            rb.velocity += grav * higherJump * Time.deltaTime;
        }
        if(!(up.IsPressed())){
            jumping = false;
        }
        if(rb.velocity.y < 0){
            if(up.IsPressed() && extraJumps>0){
                extraJumps--;
                jumping = true;
                timeJumping = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
                if(rb.velocity.y>0 && jumping){
                    timeJumping +=Time.deltaTime;
                    if(timeJumping>jumpLimit){
                        jumping = false;
                    }
                    rb.velocity += grav * higherJump * Time.deltaTime;
                }
            if(!(up.IsPressed())){
            jumping = false;
            }
        }
            rb.velocity -= grav * fastFall * Time.deltaTime;
        }
    } 
    IEnumerator Dash(float moveInput){
        ableToDash = false;
        dashing = true;
        float charGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(moveInput*3f, rb.gravityScale);
        
        yield return new WaitForSeconds(dashLength);
        rb.gravityScale = charGravity;
        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        ableToDash = true;
    }
    
    void Start(){
        up.Enable();
        left.Enable();
        right.Enable();
        dashbutton.Enable();
        rb = GetComponent<Rigidbody2D>();
        grav = new Vector2(0, -Physics2D.gravity.y);
        extraJumps = doubleJump;
    }
    // Update is called once per frame
    void FixedUpdate(){
        if(dashing){
            return;
        }
        jumpCalcs();
        
        float moveInput = movementDirection();
        if(ableToDash && dashbutton.IsPressed()){
            StartCoroutine(Dash(moveInput));
        }
        if(moveInput > 0 && !(nextToRightWall())){
            rb.velocity = new Vector2(moveInput, rb.velocity.y);
        }
        if(moveInput < 0 && !(nextToLeftWall())){
            rb.velocity = new Vector2(moveInput, rb.velocity.y);
        }
        if(moveInput == 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }
}
    
    