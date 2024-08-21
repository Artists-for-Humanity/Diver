using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    private Rigidbody2D rb;
    private AreaChecks checks;
    public float speed;
    private Transform currentPoint;
    private MeanieMovement meanieMove;
    private float directionChangeTimer = 0f;
    public float maxDirectionChangeTime = 5f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        checks = GetComponent<AreaChecks>();
        currentPoint = point2.transform;
        meanieMove = GetComponent<MeanieMovement>();
    }

    void FixedUpdate(){
        if(checks.onGround()){
            Vector2 point = currentPoint.position - transform.position;
            if(currentPoint == point2.transform){
                rb.velocity = new Vector2(speed,0);
            } else {
                rb.velocity = new Vector2(-speed,0);
            }
            directionChangeTimer += Time.deltaTime;
            if(directionChangeTimer > maxDirectionChangeTime){
                FlipDirection();
            }
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.75f && currentPoint == point2.transform){
            currentPoint = point1.transform;
            directionChangeTimer = 0f;
        }
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.75f && currentPoint == point1.transform){
            currentPoint = point2.transform;
            directionChangeTimer = 0f;
        }
    }

    void FlipDirection(){
        if(currentPoint == point2.transform){
            currentPoint = point1.transform;
        } else {
            currentPoint = point2.transform;
        }
        directionChangeTimer = 0f;
    }
}