﻿// self play AGENT script 

using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class SPAgentScript : Agent
{
    SPAreaScript spAreaScript;
    SPShooterScript shooter;
    public float range = 50f;
    public float movement = 0.3f;

    public float speed = 180f;
    public float angularvelocity = 300f;
    Rigidbody rb;

    [Range(-1, 1)]
    public float contspeed;

    [Range(-1, 1)]
    public float controtation;

    [Range(-1, 1)]
    public float contshoot;

    private float nextfire;

    public override void Initialize()
    {
        //base.Initialize();
        spAreaScript = transform.GetComponentInParent<SPAreaScript>();

        //Debug.Log(transform.parent.name);
        shooter = transform.Find("Gun").Find("Emmiter").GetComponent<SPShooterScript>();
        rb = GetComponent<Rigidbody>();
        nextfire = Time.time;
    }

    public float SignedAngleBetween(Vector3 a, Vector3 b)
    {
        Vector3 n = new Vector3(0, 1, 0);
        // angle in [0,180]
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
        return sign;
    }

    public override void OnEpisodeBegin()
    {
        //spAreaScript.ResetArea();

    }

    //public override void OnActionReceived(float[] vectorAction)
    //{

    //float movement = 0f;
    //float rotation = 0f;

    //if(vectorAction[0] == 1f)
    //{
    //shooter.shoot();
    //Debug.Log("Shots Fired");
    //}

    //if(vectorAction[1] == 1f)
    //{
    // movement = 1f;
    // }

    // else if(vectorAction[1] == 2f)
    //{
    //  movement = -1f;
    //}

    // if(vectorAction[2] == 1f)
    // {
    //     rotation = -1f;
    // }

    // else if(vectorAction[2] == 2f)
    // {
    //    rotation = 1f;
    // }

    //rb.MovePosition(transform.position + transform.forward * speed * movement * Time.fixedDeltaTime);
    //transform.Rotate(transform.up * angularvelocity * rotation * Time.fixedDeltaTime);

    //if(MaxStep>0) AddReward(-1f / MaxStep);
    //}

    public override void OnActionReceived(float[] vectorAction)
    {
        if (vectorAction[0] > 0)
        {
            if (Time.time > nextfire)
            {
                shooter.shoot();
                //Debug.Log("Shots fired");
                nextfire += 0.1f;
            }

        }

        float newspeed = vectorAction[1] * 50;
        float newrotation = vectorAction[2] * 400;
        rb.MovePosition(transform.position + transform.forward * newspeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * newrotation * Time.fixedDeltaTime);
        if (MaxStep > 0) AddReward(-1f / MaxStep);
        
     
    }



    public override void Heuristic(float[] a)
    {
        //float forwardAction = 0f;
        //float turnAction = 0f;
        //float toshoot = 0f;

        //if (Input.GetKey("w"))
        //{
        //forwardAction = 1f;
        //Debug.Log("w");
        //}

        //if (Input.GetKey("a"))
        //{
        //turnAction = 1f;
        // Debug.Log("a");
        // }

        //if (Input.GetKey("s"))
        //{
        //forwardAction = 2f;
        //Debug.Log("s");
        // }

        //if (Input.GetKey("d"))
        //{
        //turnAction = 2f;
        //Debug.Log("d");
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //toshoot = 1f;
        // Debug.Log("shoot");
        //}

        //a[0] = toshoot;
        //a[1] = forwardAction;
        //a[2] = turnAction;

        a[0] = contshoot;
        a[1] = contspeed;
        a[2] = controtation;

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.forward);

 
    }
}

   
