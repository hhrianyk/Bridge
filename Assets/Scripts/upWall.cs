using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upWall : MonoBehaviour
{

    public GameObject UpWall;
    // Start is called before the first frame update
    void Start()
    {
        var direction = new Vector3(0, gameObject.transform.localScale.y, gameObject.transform.localScale.z*0.8f) ;
        UpWall.transform.position += UpWall.gameObject.transform.TransformDirection(direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
