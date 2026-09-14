using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variable globales
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _turnSpeed;

    //Las hacemos globales para poder usarlas en distintos métodos
    private float _horizontal;
    private float _vertical;

    //Creamos variable para acceder al Animator
    private Animator _anim;
    //Creamos el collider arm para el ataque del player
    [SerializeField]
    private Collider _armCollider;

    //Accedemos al audio serializado para diferenciar los audiossources
    [SerializeField]
    private AudioSource _audioSteps;
    [SerializeField]    
    private AudioSource _audioAttack;

    //Creamos bool para activar el collider del ataque Arm 
    private bool _isAttacking;

    private void Awake()
    {
        //Mi variable _anim apunta al componente Animator
        _anim = GetComponent<Animator>();
        
        //El collider del ataque desactivado
        _armCollider.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        InputsPlayer();
        Move();
        Turn();
        Animating();
        AudioSteps();
        AudioAttack();

        //Para la animación del ataque, si clicas y no está atacando
        //se activa el collider Arm y anim de ataque
        if (Input.GetKeyDown(KeyCode.Space) && !_isAttacking) 
        {
            
            _isAttacking = true;
            _armCollider.enabled = true;
            _anim.SetTrigger("IsAttack");
            //Usamos Invoke para resetear a false isAttacking y disable
            //el collider Arm, 0.8 segundos dura la animación del ataque
            Invoke("DisableColliderArm", 0.7f);
        }

    }
 
    //Recogemos información información ejes
    private void InputsPlayer()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
    }

    //Recoger la translación del player
    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * _vertical * Time.deltaTime);

    }

    //Recoger la rotación del player
    private void Turn()
    {

        transform.Rotate(Vector3.up * _turnSpeed * _horizontal * Time.deltaTime);
    }

    private void Animating()
    {
        //El player se mueve se activa anim de correr
        if (_vertical != 0)
        {
            _anim.SetBool("IsRun", true);
        }
        else
        {
            _anim.SetBool("IsRun", false);
        }
    }

    //Método para activar el audio de los pasos si se mueve
    private void AudioSteps()
    {

        if (_vertical != 0)
        {
            if(_audioSteps.isPlaying == false)
            {
                _audioSteps.Play();
            }
            
        }
        else 
        {
            _audioSteps.Stop();
        }
    }

    private void AudioAttack()
    {

        if (_armCollider.enabled == true)
        {
            if (_audioAttack.isPlaying == false)
            {
                _audioAttack.Play();
            }

        }
        else
        {
            _audioAttack.Stop();
        }
    }

    //Método para desactivar el collider Arm de ataque
    private void DisableColliderArm()
    {
        
        _armCollider.enabled = false;
        _isAttacking = false;

    }
}
