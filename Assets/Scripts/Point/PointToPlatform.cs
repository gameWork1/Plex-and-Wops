using UnityEngine;

public class PointToPlatform : MonoBehaviour
{
    [SerializeField] public bool motionWops;

    public bool canCreatePlayer = true;

    [HideInInspector] public bool CreateAndMove;

    private void Update()
    {
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(raycast, out hit) && Input.GetMouseButtonDown(0) && !GameManager.instance.pauseGame)
        {

            if (hit.transform.gameObject.GetComponent<PlatformForDots>())
            {
                if (canCreatePlayer && !CreateAndMove && GetComponent<MovePointWithScreen>().selectPoint == null)
                {
                    PlatformForDots platform = hit.transform.gameObject.GetComponent<PlatformForDots>();
                    if (!platform.IsFull)
                    {
                        if (GetComponent<MovePointWithScreen>().gameManager.motionGame > 2)
                        {
                            CreateAndMove = true;
                            GetComponent<MovePointWithScreen>().canMovePoint = true;
                        }
                        if (motionWops)
                        {
                            platform.CreatePlayer("Wops");
                        }
                        else
                        {
                            platform.CreatePlayer("Plex");
                        }
                        
                        
                        motionWops = !motionWops;
                    }
                }
            }
        }
        
    }
}
