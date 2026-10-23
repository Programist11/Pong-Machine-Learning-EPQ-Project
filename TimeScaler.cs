using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public int timeScale = 30;
    void Start()
    {
        Time.timeScale = timeScale;
    }
}
