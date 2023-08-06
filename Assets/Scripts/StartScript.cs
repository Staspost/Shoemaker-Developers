using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(SceneMenuLoad());
    }
    private IEnumerator SceneMenuLoad() 
    {
        yield return new WaitForSeconds(1.5f);        
        SceneManager.LoadScene(1);
        yield break;
    }

}
