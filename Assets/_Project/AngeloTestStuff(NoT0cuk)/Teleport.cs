using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject teleportPoint;
    [SerializeField] private string playerTag;
    [SerializeField] private float beforeTeleportTime;
    [SerializeField] private UnityEvent BeforeTeleport, DuringTeleport;
    private GameObject currentPlayer;

    private void OnTriggerEnter2D(Collider2D col)
    {
      if(col.tag == playerTag)
      {
        TeleportPlayer(col.gameObject);
      }
    }

    private void BeforeTeleportPlayer(GameObject player)
    {
      BeforeTeleport.Invoke();
      Invoke("TeleportPlayer(player)", beforeTeleportTime);
    }

    private void TeleportPlayer(GameObject player)
    {
      DuringTeleport.Invoke();
      player.transform.position = teleportPoint.transform.position;
    }
}
