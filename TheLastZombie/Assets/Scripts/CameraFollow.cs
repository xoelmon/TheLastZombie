using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Variables globales
    //Objetivo a seguir
    [SerializeField]
    private Transform _target;

    //Velocidad seguimiento
    [SerializeField]
    private float _smoothing;

    //Distancia entre cámara y player
    [SerializeField]
    private Vector3 _offset;

  


    // Update is called once per frame
    void LateUpdate()
    {
        

        //Posición a la que se mueve la cámara, también con rotocaión
        Vector3 desiredPosition = _target.position + _target.rotation * _offset;

        //Mover la cámara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothing * Time.deltaTime);

        //Usamos LookAt para la rotación, que la cámara siga la visión de Player
        transform.LookAt(_target.position + Vector3.up * 1.5f);


    }
}
