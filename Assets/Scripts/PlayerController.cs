using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    RigidBody2D rs;
    int jumpheight;
    // Start is called before the first frame update
    void Start()
    {
        rs = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
