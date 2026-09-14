using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{



    //Accedemos al scene list
    //Caragamos la escena 1 con el botón de "Start" y "Try Again"
    //y la 0 con "CompleteLevel"
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    

    
}
