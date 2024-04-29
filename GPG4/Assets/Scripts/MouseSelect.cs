using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelect : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public LayerMask carLayers;
    public UIManager uiManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, carLayers))
                {
                    FindPath path = hit.transform.GetComponent<FindPath>();
                    path.ResetPath();
                    CheckCarType(path);
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, carLayers))
            {
                FindPath path = hit.transform.GetComponent<FindPath>();
                path.ResetPath();
                CheckCarType(path);
            }
        }
    }

    void CheckCarType(FindPath findpath)
    {
        if(findpath.carType == CarType.Enemy)
        {
            uiManager.AddScore();
        }else if(findpath.carType == CarType.Ambulance)
        {
            uiManager.ReduceHealth();
        }
    }
}
