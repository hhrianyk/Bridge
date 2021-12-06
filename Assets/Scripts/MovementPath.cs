using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes // виды пути 
    {
        linear,
        loop
    }

    public PathTypes PathType; // определ€ет тип пути
    public int movementDirection = 1;// направление движение
    public int moveigTo = 0;// к кaкой точке двигатьс€
    public Transform[] PathElements; // масив из точек

    //private void Start()
    //{
    //    PathElements[PathElements.Length - 1] = GameObject.Find("space_man_model/backpack").transform;
    //    var temp = PathElements[0];
    //    PathElements[0] = null;
    //    PathElements[0] = transform;
    //    PathElements[PathElements.Length - 1].position = new Vector3(0, moving.brickHeight, PathElements[PathElements.Length - 1].position.z);
    //    Debug.Log(PathElements[PathElements.Length - 1].position);
    //}

    public void OnDrawGizmos() //оттображает линии между точками пути
    {
        if (PathElements == null || PathElements.Length < 1)// провер€ет есть ли точки пути
        {
            return;
        }

        for (var i = 1; i < PathElements.Length; i++)// л≥н≥€ м≥ж точками
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position);

        }
        if (PathType == PathTypes.loop)//€кщо шл€х зумкнутий
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
        }


    }

    public IEnumerator<Transform> GetNextPathPoint()//получение положение следущей точки
    {
        if (PathElements == null || PathElements.Length < 1) //провер€ет есть ли точки 
        {
            yield break; //выйти из координат если нету
        }

        while (true)
        {
            yield return PathElements[moveigTo];//возврвщ€ет текущее положение

            if (PathElements.Length==1) //если точка одна то ввыйти
            {
                continue;
            }

            if(PathType==PathTypes.linear) //если лиини не зациклени
            {
                if(moveigTo<=0)// двигатса по нарастанию
                {
                    movementDirection = 1;
                }
                else if(moveigTo>=PathElements.Length-1) //двигатса по убиванию
                {
                    movementDirection = -1;
                }
            }

            moveigTo = moveigTo + movementDirection;

            if(PathType==PathTypes.loop)  //если зациклена
            {
                if(moveigTo>=PathElements.Length) ///если ми дошили до конца
                {
                    if(moveigTo>=PathElements.Length)
                    {
                        moveigTo = 0;
                    }

                    if(moveigTo<0) //ксли ми дошли до первой точки
                    {
                        moveigTo = PathElements.Length - 1;
                    }
                }
            }
        }
     }
    
}
