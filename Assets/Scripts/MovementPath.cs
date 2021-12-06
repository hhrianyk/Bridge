using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes // ���� ���� 
    {
        linear,
        loop
    }

    public PathTypes PathType; // ���������� ��� ����
    public int movementDirection = 1;// ����������� ��������
    public int moveigTo = 0;// � �a��� ����� ���������
    public Transform[] PathElements; // ����� �� �����

    //private void Start()
    //{
    //    PathElements[PathElements.Length - 1] = GameObject.Find("space_man_model/backpack").transform;
    //    var temp = PathElements[0];
    //    PathElements[0] = null;
    //    PathElements[0] = transform;
    //    PathElements[PathElements.Length - 1].position = new Vector3(0, moving.brickHeight, PathElements[PathElements.Length - 1].position.z);
    //    Debug.Log(PathElements[PathElements.Length - 1].position);
    //}

    public void OnDrawGizmos() //����������� ����� ����� ������� ����
    {
        if (PathElements == null || PathElements.Length < 1)// ��������� ���� �� ����� ����
        {
            return;
        }

        for (var i = 1; i < PathElements.Length; i++)// ��� �� �������
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position);

        }
        if (PathType == PathTypes.loop)//���� ���� ���������
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
        }


    }

    public IEnumerator<Transform> GetNextPathPoint()//��������� ��������� �������� �����
    {
        if (PathElements == null || PathElements.Length < 1) //��������� ���� �� ����� 
        {
            yield break; //����� �� ��������� ���� ����
        }

        while (true)
        {
            yield return PathElements[moveigTo];//���������� ������� ���������

            if (PathElements.Length==1) //���� ����� ���� �� ������
            {
                continue;
            }

            if(PathType==PathTypes.linear) //���� ����� �� ���������
            {
                if(moveigTo<=0)// �������� �� ����������
                {
                    movementDirection = 1;
                }
                else if(moveigTo>=PathElements.Length-1) //�������� �� ��������
                {
                    movementDirection = -1;
                }
            }

            moveigTo = moveigTo + movementDirection;

            if(PathType==PathTypes.loop)  //���� ���������
            {
                if(moveigTo>=PathElements.Length) ///���� �� ������ �� �����
                {
                    if(moveigTo>=PathElements.Length)
                    {
                        moveigTo = 0;
                    }

                    if(moveigTo<0) //���� �� ����� �� ������ �����
                    {
                        moveigTo = PathElements.Length - 1;
                    }
                }
            }
        }
     }
    
}
