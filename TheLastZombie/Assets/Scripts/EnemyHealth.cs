using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    //Variables globales

    [SerializeField]
    private float _maxHealth;
    //Salud actual
    [SerializeField]
    private float _currentHealth;
    //daño del enemigo
    [SerializeField]
    private float _armDamage;
    [SerializeField]
    private Image _lifeBarEnemy;
   

    private Animator _anim;

    [SerializeField]
    private AudioSource _audioHit;


    void Awake()
    {
        //Recogemos componente animator
        _anim = GetComponent<Animator>();
        
        _audioHit.Stop();

        _currentHealth = _maxHealth;
        _lifeBarEnemy.fillAmount = _currentHealth / _maxHealth;

    }

    //Info de el collider del brazo colisiona con enemy
    private void OnTriggerEnter(Collider infoAccess)
    {
        if (infoAccess.CompareTag("Arm"))
        {
            _currentHealth -= _armDamage;
            _lifeBarEnemy.fillAmount = _currentHealth / _maxHealth;
            Animating();
            AudioHit();

            if (_currentHealth <= 0)
            {
                Animating();
                Destroy(gameObject, 0.6f);

               
            }

        }

    }


    private void Animating()
    {
        //El player muere
        _anim.SetTrigger("IsDeath");

    }

    private void AudioHit()
    {
        if (_audioHit.isPlaying == false)
        {
            _audioHit.Play();
        }
        
        
        
    }

}
