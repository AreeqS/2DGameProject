using UnityEngine;
using UnityEngine.SceneManagement;


public class MaonMenu : MonoBehaviour
{
   
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
