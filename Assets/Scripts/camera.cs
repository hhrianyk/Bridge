using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class camera : MonoBehaviour
{

 

     [Header("GameObject")]
    // Пишу с нижним подчеркиванием, так gameObject - ключевое слово Unity.
    // а придумывать новое название переменной мне влом (сорян)
    public Transform _gameObject;
    public GameObject plane;

    [Header("Camera position restrictions")]
    public float minZ;
    public float maxZ;
    public float minX;
    public float maxX;

   

    public int radius = 5;

    //private float PY;
    //private float RX;

    private bool _Update = true;
    // Update is called once per frame
    private void Start()
    {
       // minX = (float)(-plane.transform.localScale.x * 4.5);
        maxX = (float)(plane.transform.localScale.x * 4);  
        minZ = -plane.transform.localScale.z * 4.5f;
       // maxZ = plane.transform.localScale.z * 4.5f;

        Debug.Log($"minX:{minX}  maxX:{maxX} minZ:{minZ} maxZ:{maxZ}");
        //PY = transform.position.y;
        //RX = 13.1f;
    }
    void Update()
    {
       
        try
        {
            transform.position = new Vector3(
                // Положение игрового объекта, за которым мы двигаемся
                Mathf.Clamp(_gameObject.position.x, minX, maxX),
                _gameObject.position.y+8,
                Mathf.Clamp(_gameObject.position.z, minZ, maxZ) - radius
              // Положение камеры z должно оставать неизменным 
              // (если камеры куда-то проваливается, заменить на, например, -10)
              ) ;
        }
        catch (Exception error)
        {
            // Ловим ошибку, если по каким то причинам код не может быть выполнен (например, забыли подставить объект в _gameObject)
            Debug.LogError(error);
        }
       

    }

    public void FixedUpdate()
    {
       // minX = -plane.transform.localScale.x * 4.5f;
        maxX = plane.transform.localScale.x * 4.5f;
        minZ = -plane.transform.localScale.z * 4.5f;
      //  maxZ = plane.transform.localScale.z * 4.5f;
    }
}
