using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class ScoreSystem : MonoBehaviour
{
    public int enemyScore;
    public int playerScore;
    public Text EnemyScore;
    public Text PlayerScore;
    private bool end = false;
    public int winningScore = 0;
    public EnemyPaddleAgent enemyAgent;
    public GameObject AI;
    public GameObject Bot;
    void Start()
    {
        LoadScore();
    }
    void Update()
    {
        CountScore();
        if (end)
        {
            End();
        }
    }
    void SaveScore()
    {
        PlayerPrefs.SetInt("playerScore", playerScore);
        PlayerPrefs.SetInt("enemyScore", enemyScore);
        PlayerPrefs.Save();
    }
    void LoadScore()
    {
        playerScore = PlayerPrefs.GetInt("playerScore", 0);
        enemyScore = PlayerPrefs.GetInt("enemyScore", 0);
        PlayerScore.text = playerScore.ToString();
        EnemyScore.text = enemyScore.ToString();
    }
    async Task CountScore()
    {
        if (RestartGame.playerScore)
        {
            Debug.Log("Player scored");
            playerScore += 1;
            //enemyAgent.MissBall();
            Stats.Instance.PlayerScored();
            if (AI.activeSelf)
                Stats.Instance.EndMatchVsAI(true);
            if (Bot.activeSelf)
                Stats.Instance.EndMatchVsBot(true);
            end = true;
        }
        if (RestartGame.enemyScore)
        {
            Debug.Log("Enemy scored");
            enemyScore += 1;
            //enemyAgent.ScoredBall();
            Stats.Instance.EnemyScored();
            if (AI.activeSelf)
                Stats.Instance.EndMatchVsAI(false);
            if (Bot.activeSelf)
                Stats.Instance.EndMatchVsBot(false);
            end = true;
        }
        if (playerScore == winningScore || enemyScore == winningScore)
        {
            ResetScore();
        }
    }
    void End()
    {
        SaveScore();
        end = false;
    }
    public void ResetScore()
    {
        enemyScore = 0;
        playerScore = 0;
        PlayerPrefs.SetInt("playerScore", playerScore);
        PlayerPrefs.SetInt("enemyScore", enemyScore);
        PlayerScore.text = playerScore.ToString();
        EnemyScore.text = enemyScore.ToString();
    }
}
