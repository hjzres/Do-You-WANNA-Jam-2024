using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerInputController.Instance.OnMoveCamera += MoveCamera;
        PlayerInputController.Instance.SwitchActionMap();
    }

    private void MoveCamera(Vector2 val) {
        Debug.Log("test");
        transform.position += new Vector3(val.x, val.y, 0);
    }
}
