using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class GuardVision: DynamicTrigger2D
{
    [SerializeField] private Guard parent;
    [SerializeField] private GameObject popUpAlert;

    public void CatchPatient() {
        popUpAlert.SetActive(true);
        
        parent.Alert();
        PlayerInputReader.Instance.Deinitialize();

        Invoke(nameof(ReloadScene), 3);
    }
}


