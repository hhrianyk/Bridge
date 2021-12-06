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
        if(MyPath==null ) // �������� ����
        {
           Debug.LogError("������� ����");
            return;
        }

        poinInPath=MyPath.GetNextPathPoint();// ��������� � ��������

        poinInPath.MoveNext(); // ��������� ��������� �����

        if(poinInPath.Current==null) //���� �� ����� � ����� ������������
        {
            Debug.LogError("����� �����");
        }

        transform.position = poinInPath.Current.position;     ///���� ������ ����� �� ��������� �����   
    }

    // Update is called once per frame
    void Update()
    {

        if (Play == true)
        {
            if (poinInPath == null || poinInPath.Current == null) //�������� �������� ����
            {
                return;
            }


            if (Type == MovengType.Moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, poinInPath.Current.position, Time.deltaTime * spead); //������������ � ���������� �����
            }
            else if (Type == MovengType.Larping )
            {
                transform.position = Vector3.Lerp(transform.position, poinInPath.Current.position, Time.deltaTime * spead);
            }

            //�������� �� ����������� ������� � ���������
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
