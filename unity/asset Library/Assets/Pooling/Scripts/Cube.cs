using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, IpooledObject
{
    public float upForce = 1f;
    public float sideForce = 0.1f;
    private new Rigidbody rigidbody;
    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);
        if (rigidbody != null)
        {
            rigidbody.velocity = force;
        }
        else
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = force;
        }
        
    }
}
