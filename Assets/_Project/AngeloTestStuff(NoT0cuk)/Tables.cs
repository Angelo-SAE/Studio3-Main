using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tables : MonoBehaviour
{
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private Vector2Int[] chairPositions;
    private LinkedList<TableNode> tables;

    private void Awake()
    {
      tablesAvailable.value = chairPositions.Length;
      tables = new LinkedList<TableNode>();
      CreateTableNodes();
    }

    private void CreateTableNodes()
    {
      for(int a = 0; a < chairPositions.Length; a++)
      {
        tables.AddToFront(new TableNode(chairPositions[a], true));
      }
    }

    public Vector2Int GetChairPosition()
    {
      if(tablesAvailable.value != 0)
      {
        LinkedListNode<TableNode> tempNode = tables.first;
        for(int a = 0; a < tables.Count(); a++)
        {
          if(tempNode.data.tableIsFree)
          {
            tablesAvailable.value--;
            tempNode.data.tableIsFree = false;
            return tempNode.data.chairLocation;
          } else if(a < tables.Count() - 1 )
          {
            tempNode = tables.GetNextElement(tempNode);
          }
        }
      }
      return Vector2Int.zero;
    }
}

public class TableNode
{
    public Vector2Int chairLocation;
    public bool tableIsFree;

    public TableNode(Vector2Int location, bool isFree)
    {
      chairLocation = location;
      tableIsFree = isFree;
    }
}
