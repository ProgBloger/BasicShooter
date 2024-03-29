using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour
{
        
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip hitWallSound;
    [SerializeField] AudioClip hitEmenySound;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    void OnGui()
    {
        int size = 12;
        float posX= cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector3 point = new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if(target != null)
                {
                    target.ReactToHit();
                    Messenger.Broadcast(GameEvent.ENEMY_HIT);
                    soundSource.PlayOneShot(hitEmenySound);
                }
                else{
                    StartCoroutine(SphereIndicator(hit.point));
                    soundSource.PlayOneShot(hitWallSound);
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
            Debug.Log("sphere entered");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
