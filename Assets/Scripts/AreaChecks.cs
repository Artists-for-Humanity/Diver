using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecks : MonoBehaviour
{
    public Transform groundCheck;
    public Transform wallCheckRight;
    public Transform wallCheckLeft;
    public LayerMask groundlevel;
    public bool onGround(){
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.1f), CapsuleDirection2D.Horizontal,0,groundlevel);
    }
    public bool nextToRightWall(){
        return Physics2D.OverlapCapsule(wallCheckRight.position, new Vector2(0.05f, 1f), CapsuleDirection2D.Vertical,0,groundlevel);
    }
    public bool nextToLeftWall(){
        return Physics2D.OverlapCapsule(wallCheckLeft.position, new Vector2(0.05f, 1f), CapsuleDirection2D.Vertical,0,groundlevel);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
