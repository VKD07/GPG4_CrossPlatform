using AstarAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathThreadingManager
{
    private List<Vector3> path;
    private Thread pathfindingThread;
    private bool isPathfindingCompleted = false;

    public PathThreadingManager()
    {
    }

    public void FindPath(Vector3 startPosition, Vector3 endPosition, Astar astar, Action<List<Vector3>> callback)
    {
        pathfindingThread = new Thread(() =>
        {
            path = new List<Vector3>();

            if (astar.FindPath(startPosition, endPosition))
            {
                foreach (Node node in astar.path)
                {
                    path.Add(node.WorldPosition);
                }
            }

            isPathfindingCompleted = true;
        });

        pathfindingThread.Start();

        ThreadPool.QueueUserWorkItem((state) =>
        {
            WaitForPathfindingCompletion(callback);
        });
    }

    private void WaitForPathfindingCompletion(Action<List<Vector3>> callback)
    {
        while (!isPathfindingCompleted)
        {
            Thread.Sleep(10); 
        }

        callback?.Invoke(path);
    }

    public void StopPathfindingThread()
    {
        if (pathfindingThread != null && pathfindingThread.IsAlive)
        {
            pathfindingThread.Abort();
        }
    }
}
