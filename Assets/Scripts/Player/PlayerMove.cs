using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MonoBehaviour
{      
    //other components
    private Rigidbody2D rb; 
    private AreaChecks checks;

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

    //dash values
    private bool ableToDash = true;
    private bool dashing;
    private float dashStrength = 3f;
    private float dashLength = 0.2f;
    private float dashCooldown = 1f;


    
    public char playerDirection = 'r';


    float movementDirection(){
        float moveInput = 0;
    if (left.IsPressed()){
        moveInput = -moveSpeed;
        playerDirection = 'l';
    }
    else if (right.IsPressed()){
        moveInput = moveSpeed;
        playerDirection = 'r';
    }
    return moveInput;
    }
    void jumpCalcs(){
        if(checks.onGround()){
            extraJumps = doubleJump;
        }

        if(up.IsPressed() && checks.onGround()){
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
    rb.velocity = new Vector2(moveInput * dashStrength, 0);
    yield return new WaitForSeconds(dashLength);
    rb.velocity = new Vector2(0,charGravity);
    rb.gravityScale = charGravity;
    dashing = false;
    yield return new WaitForSeconds(dashCooldown);
    ableToDash = true;
    
}
    
    void Start(){
        checks = GetComponent<AreaChecks>();
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
        if(moveInput > 0 && !(checks.nextToRightWall())){
            rb.velocity = new Vector2(moveInput, rb.velocity.y);
        }
        if(moveInput < 0 && !(checks.nextToLeftWall())){
            rb.velocity = new Vector2(moveInput, rb.velocity.y);
        }
        if(ableToDash && dashbutton.IsPressed()){
            StartCoroutine(Dash(moveInput));
        }
        if(moveInput == 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }
}
    
    