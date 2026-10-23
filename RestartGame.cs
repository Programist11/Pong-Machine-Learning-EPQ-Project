using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartGame : MonoBehaviour
{
    public static bool enemyScore = false;
    public static bool playerScore =  false;
    public object scoreSystem;
    void Start()
    {
        playerScore = false;
        enemyScore = false;
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            if (this.gameObject.name == "Enemy gates")
            {
                playerScore = true;
            }
            if (this.gameObject.name == "Player gates")
            {
                enemyScore = true;
            }
            Stats.Instance.FinishRound();
            ReastartLevel();
        }
    }
    public void ReastartLevel()
    {
        Stats.Instance.StartRound();
        SceneManager.LoadScene("0");
    }
}
