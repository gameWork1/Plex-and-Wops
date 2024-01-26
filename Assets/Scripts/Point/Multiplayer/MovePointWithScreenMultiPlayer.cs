using Mirror;
using UnityEngine;

public class MovePointWithScreenMultiPlayer : NetworkBehaviour
{
    public bool canMovePoint;
    [HideInInspector]public GameManagerMultiplayer gameManager;
    public Point selectPoint = null;
    private Ray ray;
    private float normalYPoint;
    [SerializeField] public float downerPointSpeed;
    [SerializeField] private float upperYPoint;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManagerMultiplayer>();
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer) return;


        if(gameManager.motionGame > 2)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (canMovePoint || !canMovePoint && !GetComponent<PointToPlatformMultiplayer>().CreateAndMove)
            {


                if (Input.GetMouseButtonDown(0) && GetComponent<PointToPlatformMultiplayer>().canCreatePlayer)
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
                    if (GetComponent<PointToPlatformMultiplayer>().CreateAndMove && GetComponent<PointToPlatformMultiplayer>().motionWops == point.isWops)
                    {
                        selectPoint = point;
                    }

                    else if (!GetComponent<PointToPlatformMultiplayer>().CreateAndMove && point.isWops == GetComponent<PointToPlatformMultiplayer>().motionWops)
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
            GetComponent<PointToPlatformMultiplayer>().motionWops = !GetComponent<PointToPlatform>().motionWops;
        canMovePoint = false;
        if (!GetComponent<PointToPlatformMultiplayer>().CreateAndMove)
        {
            foreach (Player pl in selectPoint.platform.GetComponent<PlatformForDots>().typesPlayers)
            {
                if (GetComponent<PointToPlatformMultiplayer>().motionWops)
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

        GetComponent<PointToPlatformMultiplayer>().CreateAndMove = false;
        selectPoint = null;
        gameManager.NextMotion();
    }

}
