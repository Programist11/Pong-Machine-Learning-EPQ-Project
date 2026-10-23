using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerPaddleAgent : Agent
{
    public Transform ball;
    public float speed = 6f;
    private Rigidbody rb;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(-3.4f, 0.7f, 0f);
        if (ball == null)
        {
            GameObject foundBall = GameObject.FindWithTag("Ball");
            if (foundBall != null)
            {
                ball = foundBall.transform;
            }
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        if (ball == null)
        {
            Debug.LogWarning("Ball is null!");
            return;
        }
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        sensor.AddObservation(ball.position.z);
        sensor.AddObservation(ballRb.linearVelocity.z);
        sensor.AddObservation(ballRb.linearVelocity.x);
        sensor.AddObservation(transform.position.z);
        sensor.AddObservation(ball.position.z - transform.position.z);
        float predictedZ = ball.position.z + ballRb.linearVelocity.z * 0.5f;
        sensor.AddObservation(predictedZ);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        if (ball == null) return;
        float move = Mathf.Clamp(actions.ContinuousActions[0], -1f, 1f);
        rb.linearVelocity = new Vector3(0, 0, move * speed);
        AddReward(-0.005f);
        float direction = Mathf.Sign(ball.position.z - transform.position.z);
        if (direction * move > 0)
        {
            AddReward(+0.005f);
        }
        else
        {
            AddReward(-0.005f);
        }
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        float predictedZ = ball.position.z + ballRb.linearVelocity.z * 0.5f;
        float dist = Mathf.Abs(transform.position.z - predictedZ);
    }
    public void HitBall()
    {
        AddReward(+3f);
    }
    public void MissBall()
    {
        AddReward(-5f);
        EndEpisode();
    }
    public void ScoredBall()
    {
        AddReward(+5f);
        EndEpisode();
    }
}
