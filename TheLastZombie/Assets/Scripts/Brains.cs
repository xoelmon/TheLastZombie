using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Brains : MonoBehaviour
{



    //Variables globales
    [SerializeField]
    private int _numBrains;
    [SerializeField]
    private TextMeshProUGUI _textBrainUI;


    private void Awake()
    {
        _textBrainUI.text = "LOST BRAINS: " + _numBrains.ToString();
    }


    private void OnCollisionEnter(Collision infoCollision)
    {

        if (infoCollision.collider.CompareTag("Brain"))
        {

            Destroy(infoCollision.gameObject);
            _numBrains--;
            _textBrainUI.text = "LOST BRAINS: " + _numBrains.ToString();

            if (_numBrains == 0)
            {
                GetNewScene();
            }
        }

    }

    //Repetimos la escena de Start
    private void GetNewScene()
    {
        SceneManager.LoadScene(3);
    }
}

    

   

