using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeanieMovement : MonoBehaviour
{
    private AreaChecks check;
    private Rigidbody2D rb;
    Vector2 grav;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<AreaChecks>();
        rb = GetComponent<Rigidbody2D>();
        grav = new Vector2(0, -Physics2D.gravity.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
