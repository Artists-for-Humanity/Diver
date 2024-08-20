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
    public int jumpStrength;
    public float fastFall;
    public float higherJump;
    public float jumpLimit;
    public float doubleJump;
    public float moveSpeed;
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

    [Header("Camera")]
    [SerializeField] private GameObject _cameraFollowGO;
    private CameraFollowObject _cameraFollowObject;
    private float _fallSpeedYDampingChangeThreshold;

    public char playerDirection = 'r';
public bool getDashing(){
    return dashing;
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
        _cameraFollowObject = _cameraFollowGO.GetComponent<CameraFollowObject>();
        _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;
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
        if(moveInput < 0 && !(checks.nextToRightWall())){
            rb.velocity = new Vector2(moveInput, rb.velocity.y);
        }
        if(moveInput> 0 || moveInput < 0){
            Turn(moveInput);
        }
        if(ableToDash && dashbutton.IsPressed()){
            StartCoroutine(Dash(moveInput));
        }
        if(moveInput == 0){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if(rb.velocity.y < _fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling){
            CameraManager.instance.LerpYDamping(true);
        }
        if(rb.velocity.y >= 0 && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling){
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }
        
    }
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
    
  
    void Turn(float moveInput){
        if (moveInput < 0){
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            playerDirection = 'l';
            _cameraFollowObject.CallTurn();
        } else {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            playerDirection = 'r';
            _cameraFollowObject.CallTurn();
        }
    }
}
    
    