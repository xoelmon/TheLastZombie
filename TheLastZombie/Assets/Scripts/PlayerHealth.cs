using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{   
    //Variables globales
   
    [SerializeField]
    private float _maxHealth;
    //Salud actual
    [SerializeField]
    private float _currentHealth;
    //daño del enemigo
    [SerializeField]
    private float _bulletDamage;
    [SerializeField]
    private Image _lifeBar;

    
    private Animator _anim;

  

  

    void Awake()
    {
        //Recogemos componente animator
        _anim = GetComponent<Animator>();


        _currentHealth = _maxHealth;
        _lifeBar.fillAmount = _currentHealth / _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider infoAccess)
    {
        if (infoAccess.CompareTag("Bullet"))
        {
            _currentHealth -= _bulletDamage;
            _lifeBar.fillAmount = _currentHealth / _maxHealth;
            Destroy(infoAccess.gameObject);

            if(_currentHealth <= 0)
            {
                Animating();
               
                //Invocamos método en 2 segundos
                Invoke("DeathScene",2.0f);
            }

        }
        
    }


    private void Animating()
    {
        //El player muere
        _anim.SetBool("IsDeath", true);
   
    }

    //Método para cambiar de escena
    private void DeathScene()
    {
        SceneManager.LoadScene(2);
    }
}
    
