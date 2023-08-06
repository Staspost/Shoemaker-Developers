using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{
   
    public void LoadSettings()
    {
        SceneManager.LoadScene(2);
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(3);
    }
}
