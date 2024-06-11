using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    private int score = 0;
    public int targetScore = 100; // Set a target score

    public void UpdateScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        if (score >= targetScore)
        {
            Debug.Log("Level Completed!");
            // Implement level completion logic here (e.g., load next level, show a completion screen, etc.)
        }
    }

    public int GetScore()
    {
        return score;
    }
}
