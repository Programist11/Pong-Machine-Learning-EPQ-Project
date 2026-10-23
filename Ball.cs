using Unity.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 0f;
    public float speedMultiplayer = 1f;
    public float direction1 = 1f;
    public float direction2 = 1f;
    public Enemy ballScript;
    public EnemyPaddleAgent enemyAgent;
    void Start()
    {
        if (Random.value > 0.5f)
        {
            direction1 = 1f;
        }
        else
        {
            direction1 = -1f;
        }

        if (Random.value > 0.5f)
        {
            direction2 = 1f;
        }
        else
        {
            direction2 = -1f;
        }
    }
    void Update()
    {
        ballScript.GetBallScript(transform.position.z);
        transform.Translate(new Vector3(direction1 * speed * Time.deltaTime, 0, direction2 * speed * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player")
        {
            direction1 *= -1;
            speed *= speedMultiplayer;
            Stats.Instance.RegisterHit();
        }
        if (other.gameObject.tag == "Wall")
        {
            direction2 *= -1;
        }
        if (other.gameObject.tag == "Enemy")
        {
            direction1 *= -1;
            speed *= speedMultiplayer;
            //enemyAgent.HitBall();
        }
    }
}
