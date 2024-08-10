using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperZone : DynamicTrigger2D
{
    [SerializeField] private GameObject helperObject;
    [SerializeField] private bool isActive = false;

    private void Start() => helperObject.SetActive(isActive);

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            SetActiveStatus(true);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
            SetActiveStatus(false);
    }
    public void SetActiveStatus(bool activeStatus) {
        isActive = activeStatus;
        helperObject.SetActive(activeStatus);
    }
}
