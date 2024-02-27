using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SetNavigationTarget : MonoBehaviour
{
 
    [SerializeField]
    private Camera topDownCamera;

     [SerializeField]
     private GameObject navTargetObject;

     private NavMeshPath path;
     private LineRenderer line;

    void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
void Update()
{
    NavMesh.CalculatePath(transform.position, navTargetObject.transform.position, NavMesh.AllAreas, path);
    line.positionCount = path.corners.Length;
    line.SetPositions(path.corners);
    line.enabled = true;

    // Calculate and print the length of the path
    float pathLength = CalculatePathLength(path);
    Debug.Log("Path Length: " + pathLength);
}

float CalculatePathLength(NavMeshPath path)
{
    float length = 0.0f;
    if (path.corners.Length < 2) // Not enough points to form a path
        return length;

    for (int i = 0; i < path.corners.Length - 1; i++)
    {
        length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
    }

    return length;
}

}