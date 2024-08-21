using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeanieMovement : MonoBehaviour
{
    private AreaChecks check;
    private Rigidbody2D rb;
    public LayerMask playerLevel;
    Vector2 grav;
    private char meanieDirection = 'r';
    private bool positiveMovement;
    public GameObject projectile;
    public Transform sight;
    public Transform bulletPos;
    private float timer;
    private float attentionSpan = 0;
    public float endAttentionSpan = 3;
    public MeanieHp health;
    
    public bool PlayerCheck(){
        return Physics2D.OverlapBox(sight.position, new Vector2(12f,7.5f),0,playerLevel);
    }
    // Start is called before the first frame update
    void Start(){
        check = GetComponent<AreaChecks>();
        rb = GetComponent<Rigidbody2D>();
        grav = new Vector2(0, -Physics2D.gravity.y);
        health = GetComponent<MeanieHp>();
        
        }

    // Update is called once per frame
    void FixedUpdate(){
        if(health.hp <= 0){
            rb.velocity = new Vector2(0f,0f);
            return;
        }
        MeanieMove();
        Turn(positiveMovement);
        while (PlayerCheck() || attentionSpan < endAttentionSpan){
            timer += Time.deltaTime;
            if(timer > 1){
            timer = 0;
            Shoot();
            }
            if(!(PlayerCheck())){
                attentionSpan += Time.deltaTime;
            }
            if(attentionSpan>endAttentionSpan){
                break;
            }
        }
        
        
    }
    void MeanieMove(){
        if(rb.velocity.x > 0){
            meanieDirection = 'r';
            positiveMovement = true;
        } else {
            meanieDirection = 'l';
            positiveMovement = false;
        }
        
    }
    void Turn(bool movingRight){
        if (!movingRight){
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            meanieDirection = 'l';
            
        } else {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            meanieDirection = 'r';
            
        }
    }
    void Shoot(){
        Instantiate(projectile, bulletPos.position, Quaternion.identity);
    }
}
