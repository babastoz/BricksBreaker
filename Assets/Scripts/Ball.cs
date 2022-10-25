using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float maxBallVelocity = 3.0f;
    public float ballSize = 1.0f;
    private Rigidbody m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        maxBallVelocity += MainManager.Instance.BallSpeed/10.0f;
        ballSize += MainManager.Instance.BallSize/10.0f;
        ResizeBall();
        ChangeBallColor();
    }
    
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > maxBallVelocity)
        {
            velocity = velocity.normalized * maxBallVelocity;
        }

        m_Rigidbody.velocity = velocity;
    }

    void ResizeBall()
    {
        gameObject.transform.localScale *= ballSize;
    }

    void ChangeBallColor()
    {
        Material ballMaterial = GetComponent<Renderer>().material;
        ballMaterial.color = MainManager.Instance.BallColor;
    }
}
