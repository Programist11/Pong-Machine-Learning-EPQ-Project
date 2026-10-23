using UnityEngine;
using System.IO;

public class Stats : MonoBehaviour
{
    public static Stats Instance;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public int playerGoals = 0;
    public int enemyGoals = 0;
    public int playerWins = 0;
    public int playerLosses = 0;
    public float matchStartTime;
    public float roundStartTime;
    public float totalRoundTime = 0f;
    public int roundsPlayed = 0;
    public int hits = 0;
    public float timeStats = 0f;
    public float maxTimeStats = 5f;
    private void Update()
    {
        timeStats += Time.deltaTime;
        if (timeStats >= maxTimeStats)
        {
            timeStats = 0f;
            PrintStats();
            SaveStats();
        }
    }
    public void PlayerScored()
    {
        playerGoals++;
    }
    public void EnemyScored()
    {
        enemyGoals++;
    }
    public void StartMatch()
    {
        matchStartTime = Time.time;
    }
    public float MatchTime()
    {
        return Time.time - matchStartTime;
    }
    public void StartRound()
    {
        roundStartTime = Time.time;
    }
    public void FinishRound()
    {
        totalRoundTime += Time.time - roundStartTime;
        roundsPlayed++;
    }
    public float AverageRoundTime()
    {
        if (roundsPlayed == 0)
            return 0;
        return totalRoundTime / roundsPlayed;
    }
    public void RegisterHit()
    {
        hits++;
    }
    public int winsVsAI = 0;
    public int gamesVsAI = 0;
    public int winsVsBot = 0;
    public int gamesVsBot = 0;
    public void EndMatchVsAI(bool win)
    {
        gamesVsAI++;
        if (win)
            winsVsAI++;
    }
    public void EndMatchVsBot(bool win)
    {
        gamesVsBot++;
        if (win)
            winsVsBot++;
    }
    public float WinRateAI()
    {
        if (gamesVsAI == 0)
            return 0;
        return (float)winsVsAI / gamesVsAI;
    }
    public float WinRateBot()
    {
        if (gamesVsBot == 0)
            return 0;
        return (float)winsVsBot / gamesVsBot;
    }
    void PrintStats()
    {
        Debug.Log("Score: player - " + playerGoals + ", enemy - " + enemyGoals);
        Debug.Log("Wins vs AI: " + winsVsAI + ", wins vs BOT: " + winsVsBot);
        Debug.Log("Win rate vs AI: " + (WinRateAI() * 100) + "%");
        Debug.Log("Win rate vs BOT: " + (WinRateBot() * 100) + "%");
        Debug.Log("Hits: " + hits);
        Debug.Log("Match Time: " + MatchTime());
        Debug.Log("Avg Round Time: " + AverageRoundTime());
    }
    public void SaveStats()
    {
        string path = Application.dataPath + "/stats.csv";
        string stats = 
        "Player Goals: " + playerGoals + "\n" +
        "Enemy Goals: " + enemyGoals + "\n" +
        "Wins vs AI: " + winsVsAI + "/" + gamesVsAI + "\n" +
        "Wins vs Bot: " + winsVsBot + "/" + gamesVsBot + "\n" +
        "Win Rate vs AI: " + WinRateAI() * 100 + "%\n" +
        "Win Rate vs Bot: " + WinRateBot() * 100 + "%\n" +
        "Hits: " + hits + "\n" +
        "Match Time: " + MatchTime() + "\n" +
        "Average Round Time: " + AverageRoundTime();
        File.AppendAllText(path, stats + "\n\n");
    }
}
