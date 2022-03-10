using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f;

    void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

            Debug.Log($"Colliders count {hitColliders.Length}");
            foreach(Collider hitCollider in hitColliders){
                Vector3 hitPosition = hitCollider.transform.position;
                hitPosition.y = transform.position.y;
                
                Debug.Log($"Current collider is {hitCollider == null}");
                Vector3 direction = hitPosition - transform.position;
                if(Vector3.Dot(transform.forward, direction.normalized) > 0.5f)
                {
                    hitCollider?.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
