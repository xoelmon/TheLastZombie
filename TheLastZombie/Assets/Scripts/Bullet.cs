using UnityEngine;

public class Bullet : MonoBehaviour
{

    //VAriables globales
    //Velocidad para la bullet
    [SerializeField]
    private float _speed;
    //Duración de la bullet antes de destruirla
    [SerializeField]
    private float _duration;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Zombie");
        transform.LookAt(player.transform.position);
        Destroy(gameObject, _duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
