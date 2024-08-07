//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024

using UnityEngine;

public class BuildingCameraControl : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public GameObject mainBuilding; // Reference to the main building
    public Vector3 topViewPosition; // Position of the camera when viewing from the top
    public Vector3 topViewRotation; // Rotation of the camera when viewing from the top

    private Vector3 originalPosition; // Store the original camera position
    private Quaternion originalRotation; // Store the original camera rotation

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Assign the main camera if not set
        }

        // Store the original camera position and rotation
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the main building's collider
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == mainBuilding.transform)
                {
                    // Move and rotate the camera to the top view
                    MoveCameraToTopView();
                }
            }
        }
    }

    void MoveCameraToTopView()
    {
        mainCamera.transform.position = topViewPosition;
        mainCamera.transform.eulerAngles = topViewRotation;
    }

    public void ResetCamera()
    {
        mainCamera.transform.position = originalPosition;
        mainCamera.transform.rotation = originalRotation;
    }
}
