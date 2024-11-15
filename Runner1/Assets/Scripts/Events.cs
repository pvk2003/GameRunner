using UnityEngine.SceneManagement;
using UnityEngine;

public class Events : MonoBehaviour
{
    public void RelayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
