using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointWithScreen : MonoBehaviour
{
    public bool canMovePoint;
    [HideInInspector]public GameManager gameManager;
    public Point selectPoint = null;
    private Ray ray;
    private float normalYPoint;
    [SerializeField] public float downerPointSpeed;
    [SerializeField] private float upperYPoint;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void LateUpdate()
    {

        if(gameManager.motionGame > 2)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (canMovePoint || !canMovePoint && !GetComponent<PointToPlatform>().CreateAndMove)
            {


                if (Input.GetMouseButtonDown(0) && GetComponent<PointToPlatform>().canCreatePlayer)
                {
                    if (selectPoint == null)
                    {
                        SelectingPoint();
                    }
                    else
                    {
                        SetSelectPoint();
                    }
                }

            }

            if(selectPoint != null && selectPoint.transform.position.y < normalYPoint + upperYPoint)
            {
                Vector3 pos = new Vector3(selectPoint.transform.position.x, normalYPoint + upperYPoint, selectPoint.transform.position.z);
                selectPoint.MoveToPlatform(pos);
            } 
        }
        
    }

    void SelectingPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.GetComponent<Point>())
        {
            Point point = hit.transform.GetComponent<Point>();
            if(point.platform != null)
            {
                if (point.platform.GetComponent<NearPlatform>().nearPlatforms != null && point.platform.GetComponent<NearPlatform>().nearPlatforms.Count > 0)
                {
                    if (GetComponent<PointToPlatform>().CreateAndMove && GetComponent<PointToPlatform>().motionWops == point.isWops)
                    {
                        selectPoint = point;
                    }

                    else if (!GetComponent<PointToPlatform>().CreateAndMove && point.isWops == GetComponent<PointToPlatform>().motionWops)
                    {
                        selectPoint = point;
                    }
                    if(selectPoint != null)
                    {
                        selectPoint.GetComponent<Point>().speed = downerPointSpeed;
                        normalYPoint = selectPoint.transform.position.y;
                        selectPoint.transform.position = new Vector3(selectPoint.transform.position.x, selectPoint.transform.position.y + upperYPoint, selectPoint.transform.position.z);
                        selectPoint.point.GetComponent<MeshRenderer>().material = selectPoint.transparentMaterial;
                        selectPoint.GetComponent<Collider>().enabled = false;
                    }
                    
                }
            }
            
            
                
        }
    }
    void SetSelectPoint()
    {

        RaycastHit hit;
        if(selectPoint.platform.GetComponent<NearPlatform>().nearPlatforms.Count > 0)
        {
            foreach (PlatformForDots platform in selectPoint.platform.GetComponent<NearPlatform>().nearPlatforms)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.GetComponent<PlatformForDots>() == platform)
                    {
                        selectPoint.platform.GetComponent<PlatformForDots>().currentPoint = null;
                        
                        platform.nextPoint = selectPoint.gameObject;
                        platform.NextPointToCurrent();
                        NextMotion();
                    }
                    

                }
            }
        }
        
    }
    void NextMotion()
    {
        if (!canMovePoint)
            GetComponent<PointToPlatform>().motionWops = !GetComponent<PointToPlatform>().motionWops;
        canMovePoint = false;
        if (!GetComponent<PointToPlatform>().CreateAndMove)
        {
            foreach (Player pl in selectPoint.platform.GetComponent<PlatformForDots>().typesPlayers)
            {
                if (GetComponent<PointToPlatform>().motionWops)
                {
                    if (pl.name == "Plex")
                    {
                        gameManager.nextTextColor = pl.textColor;
                        gameManager.nextTriggerForMotionText = "Motion " + pl.triggerName;
                    }

                }
                else
                {
                    if (pl.name == "Wops")
                    {
                        gameManager.nextTextColor = pl.textColor;
                        gameManager.nextTriggerForMotionText = "Motion " + pl.triggerName;
                    }
                }
            }
        }

        GetComponent<PointToPlatform>().CreateAndMove = false;
        selectPoint = null;
        gameManager.NextMotion();
    }

}
