using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0f;
    public float ballPosition = 0f;
    public float move = 0f;
    public float chnangeInSenceOfBall = 0f;
    void Update()
    {
        if (ballPosition > transform.position.z - chnangeInSenceOfBall)
        {
            move = 1;
        }
        else if (ballPosition < transform.position.z + chnangeInSenceOfBall)
        {
            move = -1;
        }
        transform.Translate(new Vector3(0, 0, move * speed * Time.deltaTime));
    }
    public void GetBallScript(float z)
    {
        ballPosition = z;
    }
}
