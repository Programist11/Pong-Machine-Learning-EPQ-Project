using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyPaddleAgent : Agent
{
    public Vector3 startPosition;
    public Transform ball;
    public float speed = 6f;
    private Rigidbody rb;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void OnEpisodeBegin()
    {
        transform.position = startPosition + new Vector3(0, 0, Random.Range(-0.2f, 0.2f));
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
        AddReward(-0.003f);
        float direction = Mathf.Sign(ball.position.z - transform.position.z);
        if (direction * move > 0)
        {
            AddReward(+0.002f);
        }
        else
        {
            AddReward(-0.003f);
        }
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        float predictedZ = ball.position.z + ballRb.linearVelocity.z * 0.5f;
        float dist = Mathf.Abs(transform.position.z - predictedZ);
        if (dist < 0.3f && Mathf.Abs(ballRb.linearVelocity.x) > 0.1f)
        {
            AddReward(+0.01f);
        }
    }
    public void HitBall()
    {
        AddReward(+1.5f);
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
