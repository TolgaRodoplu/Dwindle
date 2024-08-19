using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    private void Update()
    {
        //Vector3 playerOffset = playerCamera.position - otherPortal.position;
        //transform.position = portal.position + playerOffset;

        //float angularDiffferenceBetweenRot = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        //Quaternion portalRotDiff = Quaternion.AngleAxis(angularDiffferenceBetweenRot, Vector3.up);
        //Vector3 newCameraDir = portalRotDiff * playerCamera.forward;
        //transform.rotation = Quaternion.LookRotation(newCameraDir, Vector3.up);

        Matrix4x4 m = portal.localToWorldMatrix * otherPortal.worldToLocalMatrix * playerCamera.localToWorldMatrix;
        transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        
    }
}
