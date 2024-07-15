using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    [SerializeField] private Camera mCamera;
    [SerializeField] private Vector3[] cameraPositions;
    [SerializeField] private float[] cameraSize;

    public void SetPosition(int positionToMove)
    {
      transform.position = cameraPositions[positionToMove];
      mCamera.orthographicSize = cameraSize[positionToMove];
    }
}
