using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Goal[] goals;

    private int goalCount;

    private void OnEnable()
    {
        Goal.OnGoalComplete += GoalCompleted;
    }

    private void OnDisable()
    {
        Goal.OnGoalComplete -= GoalCompleted;
    }

    public void GoalCompleted(Goal goal)
    {
        goalCount++;

        if (goalCount == goals.Length)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}