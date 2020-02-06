using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    private bool completed = false;

    public static event UnityAction<Goal> OnGoalComplete;

    public void Complete()
    {
        if (!completed)
        {
            completed = true;
            OnGoalComplete?.Invoke(this);
        }
    }
}
