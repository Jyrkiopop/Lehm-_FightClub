using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBtn : MonoBehaviour
{
    public void GotoScene()
    {
        SceneManager.LoadScene("Lenni");
    }

    public void GotoControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void ExitMenu()
    {
        Application.Quit();
    }
}