using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath: MonoBehaviour
{
    public enum PathTypes
    {
        linear,
        loop
    }

    public PathTypes PathType;
    public int moveDirection = 1; //forward or back
    public int movingTo = 0;
    public Transform[] PathElements; //array of objects from scene

    public void OnDrawGizmos()
    {
        if (PathElements == null || PathElements.Length < 2){ return; }

        for (var i = 1; i < PathElements.Length;i++) // goed through all the points
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); // draws line between the points
        }

        if(PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length -1 ].position);// draws line between first and last points
        }
    }

    public IEnumerator<Transform> GetNextPathPoint() //for CoRoutine
    {
        if(PathElements == null || PathElements.Length < 1) { yield break; }
        while(true)
        {
            yield return PathElements[movingTo]; // returns current position of point

            if(PathElements.Length == 1) { continue; }

            if(PathType == PathTypes.linear)
            {
                if(movingTo <= 0)
                {
                    moveDirection = 1; //goes forward through point 
                }
                else if(movingTo >= PathElements.Length - 1)
                {
                    moveDirection = -1; //goes backwards through point 
                }
            }

            movingTo += moveDirection;

            if (PathType == PathTypes.loop)
            {
                if (movingTo >= PathElements.Length) //if reached last point
                {
                    movingTo = 0;
                }
                if (movingTo < 0) //if reachde first point
                {
                    movingTo = PathElements.Length - 1;
                }
            }
        }
    }
}
