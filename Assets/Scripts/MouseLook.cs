using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float verticalRot = 0;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body != null)
        {
            body.freezeRotation = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch(axes)
        {
            case RotationAxes.MouseX:
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

            break;
            case RotationAxes.MouseY:
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
            
            break;
            case RotationAxes.MouseXAndY:
                verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
                verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                horizontalRot = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
            break;
        }
        
    }
}
