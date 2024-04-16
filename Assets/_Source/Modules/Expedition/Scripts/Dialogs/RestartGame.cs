using Expedition;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private GameManager gameManager;

    public void RestartGame()
    {
        // Перезагрузка текущей сцены

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
