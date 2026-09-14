using UnityEngine;

public class Enemy : MonoBehaviour
{

    //Varibles globales
    //Array para posiciones
    [SerializeField]
    private Transform[] _positionArray;
    [SerializeField]
    private int _speed;
    //Almacenar la posicion del enemy
    private Vector3 _posToGo;
    //controlar en que posición de array está
    private int _i;

    //Raycast para detectar al player
    private Ray _ray;
    private RaycastHit _hit;

    //A que distancia se quedará y a que object seguir   
    [SerializeField]
    private float _distanceToPlayer;
    [SerializeField]
    private GameObject _player;
    
    //Creamos un bool para saber si está patrullando o siguiendo
    private bool _isFollow;

    //Variable para la bullet
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform _posRotBullet;
    [SerializeField]
    private float _timeBullet;

    private Animator _anim;

    //Para el audio de los pasos, serializamos para asignar en el inspector
    [SerializeField]
    private AudioSource _audioSteps;
    [SerializeField]
    private AudioSource _audioBullet;

    private void Awake()
    {

        _anim = GetComponent<Animator>();
        
        _isFollow = false;

        _audioBullet.Stop();


    }

    
    void Start()
    {
        
        _i = 0;
        _posToGo = _positionArray[_i].position;

        
    }

    private void FixedUpdate()
    {
        DetectionPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_isFollow == false)
        {
            Move();
            ChangePosition();
            Flip();
        }

        AudioSteps();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _posToGo, _speed * Time.deltaTime);

    }

    private void ChangePosition()
    {
        //si llega al destino
        if(Vector3.Distance(transform.position, _posToGo) <= Mathf.Epsilon)
        {

            if(_i == _positionArray.Length - 1)
            {
                _i = 0;
            }
            else
            {
                _i++;
            }

            _posToGo = _positionArray[_i].position;
        }
    }


    private void Flip()
    {
        transform.LookAt(_posToGo);
    }


    private void DetectionPlayer()
    {
        //Sumamos altura al Raycast
        _ray.origin = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z); 
        _ray.direction = transform.forward;

        if(Physics.Raycast(_ray, out _hit, 10.0f))
        {
            if (_hit.collider.CompareTag("Zombie"))
            {

                //si el Raycast detecta a player, mira y se mueve hacie él
                //y está true en _isFollow
                _isFollow = true;
                FollowPlayer();

                //apunta al player y ataca
                _player = GameObject.FindGameObjectWithTag("Zombie");
                Attack();
                _anim.SetBool("IsAttack", true);
                AudioBullet();



            }

        }
        else
        {
            _isFollow = false;
            _anim.SetBool("IsAttack", false);
        }
         
    }

    private void FollowPlayer()
    {
        //distancia entre enemy y player
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        transform.LookAt(_player.transform.position);

        if (distance > _distanceToPlayer)
        {
            
            //desplazamiento del enemy hacia player
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }


    }

    private void Attack()
    {
        Instantiate(_bulletPrefab, _posRotBullet.position, _posRotBullet.rotation);
    }


    //Método para activar el audio de los pasos si se mueve
    private void AudioSteps()
    {

        if (_isFollow == false)
        {
            if (_audioSteps.isPlaying == false)
            {
                _audioSteps.Play();
            }

        }
        else
        {
            _audioSteps.Stop();
        }
    }

    private void AudioBullet()
    {  
        if (_audioBullet.isPlaying == false)
        {
            _audioBullet.Play();
        }

        
        
        
    }

}
