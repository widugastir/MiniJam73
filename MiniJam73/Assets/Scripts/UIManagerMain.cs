using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerMain : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
