using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[RequireComponent(typeof(Rigidbody2D))]
public class SecurityCamera : MonoBehaviour
{
    private enum Axis { Static, Horizontal, Vertical }
    [SerializeField] private Axis axis;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public CinemachineVirtualCamera VirtualCamera => virtualCamera;

    [SerializeField] private Rigidbody2D rb2D;
    public Rigidbody2D Rb2D => rb2D;

    [SerializeField] private Canvas cameraUI;
    public Canvas CameraUI => cameraUI;

    [SerializeField] private BoxCollider2D visionBounds;
    public BoxCollider2D VisionBounds => visionBounds;

    [SerializeField] private PolygonCollider2D confinerBounds;
    public PolygonCollider2D ConfinerBounds => confinerBounds;

    private void Awake() {
        if(virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if(rb2D == null)
            rb2D = GetComponent<Rigidbody2D>();

        switch(axis)
        {
            case Axis.Static:
                rb2D.constraints = RigidbodyConstraints2D.FreezePosition;
                break;

            case Axis.Horizontal:
                rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                break;

            case Axis.Vertical:
                rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
                break;
        }
    }

    public void UpdatePriority(bool newActiveCamera) {
        virtualCamera.Priority = newActiveCamera ? 1 : 0;
        cameraUI.enabled = newActiveCamera;
    }
        
}
