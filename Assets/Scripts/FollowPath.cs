using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    // Start is called before the first frame update
    public enum MovengType
    {
        Moving,
        Larping
    }

    public MovengType Type = MovengType.Moving;
    public MovementPath MyPath;
    public float spead = 1;
    public float maxDistanse = .1f;
    public bool Play = false;
    public GameObject[] delete;

    public IEnumerator<Transform> poinInPath;

    void Start()
    {
        if(MyPath==null ) // проверка пути
        {
           Debug.LogError("Примени путь");
            return;
        }

        poinInPath=MyPath.GetNextPathPoint();// обращение к коротину

        poinInPath.MoveNext(); // получение следующей точки

        if(poinInPath.Current==null) //есть ли точка к торой передвигатся
        {
            Debug.LogError("нужни точки");
        }

        transform.position = poinInPath.Current.position;     ///обєкт должен стать на стартовою точку   
    }

    // Update is called once per frame
    void Update()
    {

        if (Play == true)
        {
            if (poinInPath == null || poinInPath.Current == null) //проверка отсутцвя пути
            {
                return;
            }


            if (Type == MovengType.Moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, poinInPath.Current.position, Time.deltaTime * spead); //передвижение к следующейй точки
            }
            else if (Type == MovengType.Larping )
            {
                transform.position = Vector3.Lerp(transform.position, poinInPath.Current.position, Time.deltaTime * spead);
            }

            //проверка на возможность перейти к следующий
            var distensSquere = (transform.position - poinInPath.Current.position).sqrMagnitude;
            if (distensSquere < maxDistanse * maxDistanse)
            {

                poinInPath.MoveNext();
                if (MyPath.moveigTo == MyPath.PathElements.Length - 1)
                {
                    Play = false;
                    //var temp = GameObject.Find("BlaydTrail").transform;
                    //Destroy(temp);
                    foreach (var a in delete)
                        Destroy(a,0.2f);


                    GetComponent<FollowPath>().enabled = false;
                }
            }
        }
    }
}
