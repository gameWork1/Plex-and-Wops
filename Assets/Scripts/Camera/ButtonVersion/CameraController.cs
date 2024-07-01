using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField] private float speed;
    private bool editMode = false;
    [SerializeField] private GameObject controller;
    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;

    public void SetDirectionForX(float x)
    {
        direction = new Vector3(x, direction.y, direction.z);
    }
    public void SetDirectionForZ(float z)
    {
        direction = new Vector3(direction.x, direction.y, z);
    }

    public void ChangeMode()
    {
        editMode = !editMode;
        controller.SetActive(editMode);
        GameManager.instance.pauseGame = editMode;
        GetComponent<SaveAndLoadPosition>().SavePosition();
    }

    private void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPosition.x, maxPosition.x), transform.position.y,
            Mathf.Clamp(transform.position.z, minPosition.z, maxPosition.z));
    }

}
