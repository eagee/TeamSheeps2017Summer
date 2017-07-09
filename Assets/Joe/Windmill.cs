using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour {

    public float torque = 1f;
    public float turn = -1f;

    public Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.AddTorque(transform.forward * torque * turn);
    }
}
