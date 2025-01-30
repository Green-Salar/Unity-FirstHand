using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravityUnselect : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddGravity()
    {
        if (rb!=null)
        rb.useGravity = true;
    }
     
}
