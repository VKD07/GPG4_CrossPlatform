using AstarAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
    public PathThreadManager pathThreadManager;
    public Astar astar;
    public Transform start, end;
    public List<Vector3> path;
    public float moveSpeed;
    int currentPathIndex;
    public bool isNotActive;
    public CarType carType;

    public void FindNewPath()
    {
        WithoutThread();
        //WithThread();
    }

    void WithoutThread()
    {
        if (astar.FindPath(transform.position, end.position))
        {
            currentPathIndex = 0;
            path.Clear();
            foreach (Node node in astar.path)
            {
                path.Add(node.WorldPosition);
            }
        }
    }
   
    void WithThread()
    {
        pathThreadManager.FindPath(transform.position, end.position, astar, (List<Vector3> newPath) =>
        {
            path = newPath;
            currentPathIndex = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        GoToPath();
    }

    private void GoToPath()
    {
        if (path.Count > 0 && currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex];
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.LookAt(targetPosition);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPathIndex++;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetPath()
    {
        gameObject.SetActive(false);
        currentPathIndex = 0;
    }

    private void OnApplicationQuit()
    {
        pathThreadManager.StopPathfindingThread();
    }
}

[System.Serializable]
public enum CarType
{
    None = 0,
    Enemy,
    Ambulance,
}