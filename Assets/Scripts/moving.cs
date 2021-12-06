using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class moving : MonoBehaviour
{


    public float speed=5;
    public GameObject backpack;
    public AudioSource audioBridge;
    public Image bar;
 
    private float mY, mX;

    private Rigidbody rb;
    private Vector3 moveVelocity; 
    private Vector3 brickHeight=new Vector3(0,0.2f,-0.1f);
    private Animator anim;
    private int LimitBrics = 18;

    private List<GameObject>  arrayBrick; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

         mX = Camera.main.scaledPixelWidth / 2;
         mY = Camera.main.scaledPixelHeight / 5;

        
      //  brickHeight = backpack.transform.position.y;

        arrayBrick = new List<GameObject>();
        //   Debug.Log($"X:{mX} Y:{mY}");


    }

    // Update is called once per frame
    void Update()
    {
        float x, z;
        Vector3 moveInput;
 
        if (Input.GetKey(KeyCode.Mouse0))
        {
 
            x = (Input.mousePosition.x - mX) / mX;
            z = (Input.mousePosition.y - mY) / mY;
            if (z > 1) z = 1;
            float rotation = (float)(Math.Acos((0 * x + 1 * z) / (Math.Sqrt(1) * Math.Sqrt(x * x + z * z))) * 180 / Math.PI);
           
            if (x < 0) rotation = -rotation;
             
            gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
            anim.SetBool("IsRun", true);
 
            moveInput = new Vector3(x, 0, z);
 
        }
        else
        {
            anim.SetBool("IsRun", false);
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        } 
        moveVelocity = moveInput.normalized * speed;
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        if (transform.position.y < -50) transform.position = new Vector3();


        //if (arrayBrick.Count > LimitBrics - 2) bar.color = new Color(UnityEngine.Random.Range(50, 250), UnityEngine.Random.Range(50, 250), UnityEngine.Random.Range(50, 250));
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Brick" && arrayBrick.Count<= LimitBrics)
        {
            collision.gameObject.tag = "Brige";

            
            collision.gameObject.GetComponent<FollowPath>().Play = true;
 
            //collision.gameObject.transform.SetParent(GameObject.Find("Pelvis").transform);
            collision.gameObject.transform.SetParent(gameObject.transform);

            backpack.transform.position += new Vector3(0, collision.gameObject.transform.localScale.y*1.5f, 0);
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;

           // collision.gameObject.transform.position =new Vector3( backpack.transform.position.x, brickHeight, backpack.transform.position.z);
            collision.gameObject.transform.rotation = backpack.transform.rotation;
            GameObject temp = collision.gameObject;
            arrayBrick.Add(temp);
            GetComponent<AudioSource>().Play();


            bar.fillAmount = (float)arrayBrick.Count / LimitBrics;
            //bar.color = new  Color(bar.color.r, -256+100*arrayBrick.Count / 256, bar.color.b);

            bar.color = Color.green;
            if (arrayBrick.Count > LimitBrics * 0.7) bar.color = Color.yellow;
            if (arrayBrick.Count > LimitBrics - 1) bar.color =Color.red;

        }
        if (collision.gameObject.tag == "stepUp" && arrayBrick.Count >0  && collision.gameObject.transform.position.y<9.9f)
        {
   
            var temp=Instantiate(collision.gameObject,
                collision.gameObject.transform.position, collision.gameObject.transform.rotation);

            var direction = new Vector3(0, collision.gameObject.transform.localScale.y, collision.gameObject.transform.localScale.z * 0.8f);
            temp.transform.position += collision.gameObject.transform.TransformDirection(direction);

            //var direction = new Vector3(0, collision.gameObject.transform.localScale.y, collision.gameObject.transform.localScale.z * 0.8f);
            //collision.gameObject.transform.position += collision.gameObject.transform.TransformDirection(direction);

             collision.gameObject.tag = "Brige";
             Destroy(arrayBrick[arrayBrick.Count-1]);

             arrayBrick.RemoveAt(arrayBrick.Count-1);

                backpack.transform.position -=new Vector3(0, arrayBrick[arrayBrick.Count - 1].transform.localScale.y*1.5f,0);
            if (backpack.transform.position.y < 0 || arrayBrick.Count < 1) backpack.transform.position = brickHeight;

            audioBridge.Play();
            bar.fillAmount =  (float)arrayBrick.Count / LimitBrics;
            bar.color = Color.green;
            if (arrayBrick.Count > LimitBrics * 0.6) bar.color = Color.yellow;
            if (arrayBrick.Count > LimitBrics - 2) bar.color = Color.red;
            //arrayBrick[arrayBrick.Count - 1].transform.SetParent(null);
            //arrayBrick[arrayBrick.Count - 1].tag = "stepUp";
            //arrayBrick[arrayBrick.Count - 1].transform.position = new Vector3(collision.gameObject.transform.position.x,
            //    collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y,
            //   collision.gameObject.transform.position.z + collision.gameObject.transform.localScale.z * 0.75f);
            //arrayBrick[arrayBrick.Count - 1].transform.rotation = Quaternion.identity;
            //arrayBrick[arrayBrick.Count - 1].transform.localScale = collision.gameObject.transform.localScale;
            //arrayBrick[arrayBrick.Count - 1].GetComponent<Rigidbody>() = collision.gameObject.GetComponent<Rigidbody>();
        }   

    }
}
