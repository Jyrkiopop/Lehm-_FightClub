using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public void GotoScene()
    {
        SceneManager.LoadScene("Lenni");
    }

    public void ExitMenu()
    {
        Application.Quit();
    }
}