using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperZone : DynamicTrigger2D
{
    private enum HelperType { Patient, Watcher }

    [SerializeField] private HelperType helperType;
    [SerializeField] private GameObject helperObject;
    [SerializeField] private bool isActive = false;

    private void Start() => helperObject.SetActive(isActive);

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        switch(helperType) {
            case HelperType.Patient:
                if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
                    SetActiveStatus(true);
                break;

            case HelperType.Watcher:
                if(other.gameObject.layer == LayerMask.NameToLayer("Camera Vision"))
                    SetActiveStatus(true);
                break;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        switch(helperType) {
            case HelperType.Patient:
                if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
                    SetActiveStatus(false);
                break;

            case HelperType.Watcher:
                if(other.gameObject.layer == LayerMask.NameToLayer("Camera Vision"))
                    SetActiveStatus(false);
                break;
        }
    }
    public void SetActiveStatus(bool activeStatus) {
        isActive = activeStatus;
        helperObject.SetActive(activeStatus);
    }
}
