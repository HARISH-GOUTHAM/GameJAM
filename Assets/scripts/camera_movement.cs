using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public float speed;
    public float borderthickness;
    public float scrollspeed;
    public Vector2 panlimitX;
    public Vector2 panlimitZ;
    public Vector2 scrollLimit;

    void Update()
    {

        Vector3 position = transform.position;
        if (Input.GetKey("w")|| Input.mousePosition.y >= Screen.height - borderthickness)
        {
            position.z += speed*Time.deltaTime; 
        }
        if (Input.GetKey("s")|| Input.mousePosition.y <= borderthickness)
        {
            position.z -= speed*Time.deltaTime; 
        } 
        if (Input.GetKey("d")|| Input.mousePosition.x >= Screen.width - borderthickness)
        {
            position.x += speed*Time.deltaTime; 
        } 
        if (Input.GetKey("a")|| Input.mousePosition.x <=  borderthickness)
        {
            position.x -= speed*Time.deltaTime; 
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y -= scroll * scrollspeed *100f* Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -panlimitX.x , panlimitX.y);
        position.y = Mathf.Clamp(position.y, scrollLimit.x, scrollLimit.y);
        position.z = Mathf.Clamp(position.z, -panlimitZ.x, panlimitZ.y);


        transform.position = position;
        
    }
}
