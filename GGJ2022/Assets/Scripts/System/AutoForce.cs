using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoForce : MonoBehaviour
{
    public Vector3 force;
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + force);
    }
}
