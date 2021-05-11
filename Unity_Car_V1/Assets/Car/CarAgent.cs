using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;



public class CarAgent : Agent
{
    private CarController carDriver;
    Rigidbody car;
    public float Force = 10f;


    public override void Initialize()
    { 
        car = gameObject.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        car.velocity = Vector3.zero;
        car.angularVelocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(gameObject.transform.position);
        sensor.AddObservation(car.velocity);
        sensor.AddObservation(car.angularVelocity);
        sensor.AddObservation(gameObject.transform.forward);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector2 control = Vector2.zero;
        control.x = vectorAction[0];
        control.y = vectorAction[1];

        car.AddForce(control * Force);
        AddReward(+0.001f);
     }

    public void OnCollisionEnter(Collision collision)
    {
        // Cube crashed into the block wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetReward(-5f);
            EndEpisode();
            Debug.Log("Reset");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            AddReward(+1f);
            Debug.Log("+1");
        }
    }


    
}
