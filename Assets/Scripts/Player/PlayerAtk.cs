using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAtk : MonoBehaviour{
    private GameObject attackArea = default;
    private GameObject attackAreaLeft = default;
    public float attackLength = 0.2f;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool startCooldown = false;
    private float attackCooldown = 0.4f;
    private PlayerMove move;
    public InputAction attackButton;
    public Transform wallCheckRight;
    public Transform wallCheckLeft;
    public LayerMask groundlevel;
    public bool nextToRightWall(){
        return Physics2D.OverlapCapsule(wallCheckRight.position, new Vector2(1.75f, 0.25f), CapsuleDirection2D.Horizontal,0,groundlevel);
    }
    public bool nextToLeftWall(){
        return Physics2D.OverlapCapsule(wallCheckLeft.position, new Vector2(1.75f, 0.25f), CapsuleDirection2D.Horizontal,0,groundlevel);
    }
    
    void Start(){
        attackButton.Enable();
        move = GetComponent<PlayerMove>();
        attackArea = transform.GetChild(0).gameObject;
        attackAreaLeft = transform.GetChild(1).gameObject;
    }
  
    void FixedUpdate(){
        AbleToAttack();
        if(attackButton.IsPressed() && canAttack){
            StartCoroutine(Attack());
            
        }
    }
    IEnumerator Attack(){
        canAttack = false;
        isAttacking = true;
        if(move.playerDirection == 'r'){
            attackArea.SetActive(isAttacking);
        } else {
            attackAreaLeft.SetActive(isAttacking);
        }
        yield return new WaitForSeconds(attackLength);
        isAttacking = false;
        attackArea.SetActive(isAttacking);
        attackAreaLeft.SetActive(isAttacking);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }
    void AbleToAttack(){
        if(startCooldown){
            canAttack = false;
        }
    }
}
