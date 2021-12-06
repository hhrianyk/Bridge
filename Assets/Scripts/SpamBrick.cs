using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamBrick : MonoBehaviour
{

    public GameObject Brick;
    public GameObject plane; 

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 110; i++)
        {
            Instantiate(Brick,new Vector3(Random.Range(-plane.transform.localScale.x * 4.5f, plane.transform.localScale.x *4.5f),
                0,Random.Range(-plane.transform.localScale.z * 4.5f, plane.transform.localScale.z * 4.5f)), Quaternion.identity).transform.SetParent(gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
