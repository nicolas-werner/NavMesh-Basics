using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    public Transform goalCube;
    private NavMeshAgent agent;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false; // Disable automatic movement
        lineRenderer = GetComponent<LineRenderer>();
        
        // Configure the line renderer appearance
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
    }

    // Update is called once per frame
    void Update()
    {
        DrawPathToGoal();
        ManualMove();
    }

    void DrawPathToGoal()
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(goalCube.position, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
    }

    void ManualMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
        if (moveDirection != Vector3.zero)
        {
            // Move the agent manually
            agent.nextPosition = transform.position + moveDirection * agent.speed * Time.deltaTime;
            transform.position = agent.nextPosition;
        }
    }
}
