using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MeanieMovement : MonoBehaviour
{
    private AreaChecks check;
    // Start is called before the first frame update
    void Start()
    {
        check = GetComponent<AreaChecks>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
