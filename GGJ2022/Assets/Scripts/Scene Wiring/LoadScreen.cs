using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    public void LoadScene(int gameplay)
    {
        SceneManager.LoadScene(gameplay);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
