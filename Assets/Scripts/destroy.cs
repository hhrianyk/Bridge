using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public GameObject playn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > playn.transform.position.y-0.15 && transform.position.y < playn.transform.position.y+0.15) 
        {
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag=="stepUp")
        {
            transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y,
                collision.gameObject.transform.position.z + collision.gameObject.transform.localScale.z * 0.8f);
             
        }
    }
}
