using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class loadscreen : MonoBehaviour
{
   public void LoadScene(int gameplay)
   {
 
   SceneManager.LoadScene(gameplay); 


   }
}
