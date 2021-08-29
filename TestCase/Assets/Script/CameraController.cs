using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0, 10f)]
    [SerializeField] private float moveSpeed;
    [Range(0f, 5f)]
    [SerializeField] private float sensitivity;
    [SerializeField] private bool isDragging { get; set; }
    [SerializeField] private new Camera camera { get; set; }

    [SerializeField] float leftBorder;
    [SerializeField] float rightBorder;
    [SerializeField] float bottomBorder;
    [SerializeField] float upperBorder;

    [SerializeField] private float zoomSpeed = 1;
    [SerializeField] private float cameraSize;
    [SerializeField] private float smoothSpeed = 2.0f;
    [SerializeField] private float minZoom = 1.0f;
    [SerializeField] private float maxZoom = 20.0f;

    private Vector3 tempCenter, targetDirection, tempMousePos;
    private float tempSens;
    private new Transform transform;

    private void Start()
    {
        camera = GetComponent<Camera>();
        transform = camera.GetComponent<Transform>();
        cameraSize = camera.orthographicSize; ;
    }

    private void Update()
    {
        UpdateInput();
        UpdatePosition();
        ZoomCamera();
    }

    private void UpdateInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0)) OnPointDown(mousePosition);
        else if (Input.GetMouseButtonUp(0)) OnPointUp(mousePosition);
        else if (Input.GetMouseButton(0)) OnPointMove(mousePosition);
    }
    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            cameraSize -= scroll * zoomSpeed;
            cameraSize = Mathf.Clamp(cameraSize, minZoom, maxZoom);
            camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, cameraSize, smoothSpeed * Time.deltaTime);
        }
    }
    private void UpdatePosition()
    {
        float speed = Time.deltaTime * moveSpeed;
        if (isDragging) tempSens = sensitivity;
        else tempSens = Mathf.Lerp(tempSens, 0f, speed);

        Vector3 newPosition = position + targetDirection * tempSens;
        transform.position = Vector3.Lerp(position, newPosition, speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), Mathf.Clamp(transform.position.y, bottomBorder, upperBorder), transform.position.z);
    }

    private void OnPointDown(Vector3 mousePosition)
    {
        tempCenter = GetWorldPoint(mousePosition);
        targetDirection = Vector3.zero;
        tempMousePos = mousePosition;
        isDragging = true;
    }

    private void OnPointMove(Vector3 mousePosition)
    {
        if (isDragging)
        {
            Vector3 point = GetWorldPoint(mousePosition);
            float sqrDist = (tempCenter - point).sqrMagnitude;
            if (sqrDist > 0.1f)
            {
                targetDirection = (tempMousePos - mousePosition).normalized;
                tempMousePos = mousePosition;
            }
        }
    }

    private void OnPointUp(Vector3 mousePosition)
    {
        isDragging = false;
    }

    public Vector3 position
    {
        get { return transform.position; }
        set { transform.position = new Vector3(value.x, value.y, -10f); }
    }
    private Vector3 GetWorldPoint(Vector3 mousePosition)
    {
        Vector3 point = Vector3.zero;
        Ray ray = camera.ScreenPointToRay(mousePosition);

        Vector3 normal = Vector3.forward;
        Vector3 position = Vector3.zero;
        Plane plane = new Plane(normal, position);

        float distance;
        plane.Raycast(ray, out distance);
        point = ray.GetPoint(distance);

        return point;
    }
}
