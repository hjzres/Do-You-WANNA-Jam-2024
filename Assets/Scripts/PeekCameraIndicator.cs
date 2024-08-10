using UnityEngine;
using UnityEngine.UI;

public class PeekCameraIndicator : MonoBehaviour
{
    public Camera mainCamera;               // Reference to the main camera
    public Camera peekCamera;               // Reference to the peek camera
    public RectTransform arrowIndicator;    // UI element for the arrow indicator
    public Image peekCameraImage;        // UI element to display the peek camera's RenderTexture
    public float arrowRadius = 50f;         // Distance from the center of the RawImage

    private void Start()
    {
        // Initially hide the arrow and peek camera
        arrowIndicator.gameObject.SetActive(false);
        peekCameraImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);
        bool isOffScreen = screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;

        if (isOffScreen)
            ShowPeekCamera(screenPos);
        else
            HidePeekCamera();
    }

    private void ShowPeekCamera(Vector3 screenPos)
    {
        // Calculate the direction from the screen center to the off-screen fighter
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, screenPos.z);
        Vector3 direction = screenPos - screenCenter;
        direction.z = 0; // Ignore z-axis for 2D
        direction.Normalize();

        // Position the arrow relative to the center of the RawImage
        Vector2 peekCenter = peekCameraImage.rectTransform.position;
        Vector2 arrowPos = peekCenter + new Vector2(direction.x, direction.y) * arrowRadius;
        arrowIndicator.position = arrowPos;

        // Rotate the arrow to point toward the fighter
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowIndicator.rotation = Quaternion.Euler(0, 0, angle);

        // Enable the arrow and peek camera display
        arrowIndicator.gameObject.SetActive(true);
        peekCameraImage.gameObject.SetActive(true);

        // Ensure the peek camera is active and set to follow the fighter
        peekCamera.gameObject.SetActive(true);
        peekCamera.transform.position = new Vector3(transform.position.x, transform.position.y, peekCamera.transform.position.z);
    }

    private void HidePeekCamera()
    {
        // Disable the arrow and peek camera display
        arrowIndicator.gameObject.SetActive(false);
        peekCameraImage.gameObject.SetActive(false);
    }
}



