using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public bool freezeVertical, freezeHorizontal;
    private Vector3 positionStore;
    public bool clampPosition;
    public Transform clampMin, clampMax;
    private float halfWidth, halfHeight;
    public Camera theCam;

    // Start is called before the first frame update
    void Start()
    {
        positionStore = transform.position;

        clampMin.SetParent(null);
        clampMax.SetParent(null);//means they no longer have a parent

        halfHeight = theCam.orthographicSize;
        halfWidth = theCam.orthographicSize * theCam.aspect; 
    }

    // Update is called once per frame
    void LateUpdate()   //lateupdate is called after calling every single update in a scene
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        if(freezeVertical == true)
        {
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }

        if(freezeHorizontal == true) //we dont have to write ==true cuz if statement checks if the entire fact is true ie.checking (freezhri ==true) is the same ase checkinf (freezhri)true. but wriring == ture is better for reading
        {
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        }

        if(clampPosition == true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth),
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z);
        }
        if (ParallaxBackground.Instance != null)
        {
            ParallaxBackground.Instance.MoveBackground();
        }
    }

    private void OnDrawGizmos()
    {
        if(clampPosition == true)
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));

            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0f));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0f));
        }
        
    }
}
