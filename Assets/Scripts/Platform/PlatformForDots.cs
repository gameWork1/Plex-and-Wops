using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlatformForDots : MonoBehaviour
{
    
    [SerializeField]public Player[] typesPlayers;
    [SerializeField] private float offset;
    [SerializeField] private int rotateX;
    [SerializeField] private bool moveTowardsFromXToZ;
    [HideInInspector] public bool IsFull = false;
     public GameObject currentPoint = null;
    [HideInInspector] public GameObject nextPoint = null;
    private GameManager gameManager;
    private float normalPointY;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
            gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        normalPointY = transform.position.y + offset;
    }

    public void CreatePlayer(string name)
    {

        foreach(Player pl in typesPlayers)
        {
            if (pl.name == name)
            {
                Vector3 position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
                GameObject playerClone = Instantiate(pl.playerObject, position, Quaternion.identity);
                playerClone.transform.Rotate(new Vector3(rotateX, 0, 0));
                if (NetworkClient.isConnected || NetworkServer.active)
                    NetworkServer.Spawn(playerClone);
                currentPoint = playerClone;
                IsFull = true;

                gameManager.nextTextColor = pl.textColor;
                gameManager.nextTriggerForMotionText = "Motion " + pl.triggerName;    
                break;
            }
        }
        if (gameManager.motionGame < 3)
        {

            gameManager.NextMotion();
        }
    }
    


    private void FixedUpdate()
    {
        if (currentPoint != null)
            currentPoint.GetComponent<Point>().platform = transform;
        NextPointToCurrent();


        if (IsFull && currentPoint == null)
            IsFull = false;


        else if (!IsFull && currentPoint != null)
            IsFull = true;


        if(currentPoint != null)
            currentPoint.GetComponent<Point>().isMoving = false;

        if (moveTowardsFromXToZ)
        {
            
            if (currentPoint != null && currentPoint.transform.position.x != transform.position.x)
            {
                Vector3 pos = new Vector3(transform.position.x, currentPoint.transform.position.y, currentPoint.transform.position.z);
                currentPoint.GetComponent<Point>().MoveToPlatform(pos);
            }
            else if (currentPoint != null && currentPoint.transform.position.z != transform.position.z)
            {
                Vector3 pos = new Vector3(currentPoint.transform.position.x, currentPoint.transform.position.y, transform.position.z);
                currentPoint.GetComponent<Point>().MoveToPlatform(pos);
            }

            else if (currentPoint == null && nextPoint != null)
                NextPointToCurrent();
        }
        else
        {
            if (currentPoint != null && currentPoint.transform.position.z != transform.position.z)
            {
                Vector3 pos = new Vector3(currentPoint.transform.position.x, currentPoint.transform.position.y, transform.position.z);
                currentPoint.GetComponent<Point>().MoveToPlatform(pos);
            }
            else if (currentPoint != null && currentPoint.transform.position.x != transform.position.x)
            {
                Vector3 pos = new Vector3(transform.position.x, currentPoint.transform.position.y, currentPoint.transform.position.z);
                currentPoint.GetComponent<Point>().MoveToPlatform(pos);
            }

            else if (currentPoint == null && nextPoint != null)
                NextPointToCurrent();
        }
        if(currentPoint != null && !currentPoint.GetComponent<Point>().isMoving && currentPoint.transform.position.y > normalPointY && !gameManager.pointCreateManager.canCreatePlayer)
        {
            Vector3 pos = new Vector3(currentPoint.transform.position.x, normalPointY, currentPoint.transform.position.z);
            currentPoint.GetComponent<Point>().MoveToPlatform(pos);
        }
        else if(currentPoint != null && !currentPoint.GetComponent<Point>().isMoving && currentPoint.GetComponentInChildren<MeshRenderer>().material != currentPoint.GetComponent<Point>().normalMaterial
            && !gameManager.pointCreateManager.canCreatePlayer)
        {
            currentPoint.GetComponentInChildren<MeshRenderer>().material = currentPoint.GetComponent<Point>().normalMaterial;
            currentPoint.GetComponent<Point>().speed = currentPoint.GetComponent<Point>().normalSpeed;
            currentPoint.GetComponent<Collider>().enabled = true;
        }
        


    }

    public void NextPointToCurrent()
    {
        if (nextPoint != null)
        {
            currentPoint = nextPoint;
            nextPoint = null;
        }
    }

    
}
[System.Serializable]
public struct Player
{
    public string name;
    public GameObject playerObject;
    public Color textColor;
    public string triggerName;
}
