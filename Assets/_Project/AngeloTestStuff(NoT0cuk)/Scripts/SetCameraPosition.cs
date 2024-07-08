using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    [SerializeField] private Vector3[] cameraPositions;

    public void SetPosition(int positionToMove)
    {
      transform.position = cameraPositions[positionToMove];
    }
}
