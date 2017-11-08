using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Vector3 center = new Vector3(5, 0, 5);
    public float heightAngle = 40;
    public float distance = 20;
    public float rotationSpeed;

    float velocityStartupFactor = 1;
    float angle = 0;
    Vector3 oldMouse;

    // Update is called once per frame
	void Update ()
    {
        if (!Input.GetMouseButton(1))
            angle += rotationSpeed * Time.deltaTime * velocityStartupFactor;
        else
        {
            angle += rotationSpeed * Time.deltaTime * (Input.mousePosition.x - oldMouse.x) * 0.25f;
            velocityStartupFactor = 0;
        }
        angle = angle % (2 * Mathf.PI);

        distance *= 1 - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 7;
        float height = distance * Mathf.Sin(Mathf.Deg2Rad * heightAngle);
        float flatDistance = distance * Mathf.Cos(Mathf.Deg2Rad * heightAngle);

        transform.position = new Vector3(flatDistance * Mathf.Sin(angle), height, flatDistance * Mathf.Cos(angle)) + center;
        transform.LookAt(center);

        velocityStartupFactor = Mathf.Lerp(velocityStartupFactor, 1, .2f);
        oldMouse = Input.mousePosition;
	}
}
