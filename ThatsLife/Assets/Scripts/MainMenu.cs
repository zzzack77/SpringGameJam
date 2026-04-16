using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void OnEnable()
    {
        TimeManager.PauseGame?.Invoke();
    }

    public void UIPlayGame()
    {
        TimeManager.PlayGame?.Invoke();
        gameObject.SetActive(false);
    }
}
