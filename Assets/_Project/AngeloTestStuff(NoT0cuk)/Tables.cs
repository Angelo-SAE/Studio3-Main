using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tables : MonoBehaviour
{
    [SerializeField] private IntObject tablesAvailable;
    private LinkedList<TableNode> tables;

    private void Awake()
    {
      tablesAvailable.value = transform.childCount;
    }

    public void CreateTableNode(Table table, Vector2Int position)
    {
      if(tables is null)
      {
        tables = new LinkedList<TableNode>();
      }
      tables.AddToFront(new TableNode(table, position, true));
    }

    public Vector2Int GetChairPosition(Customer customer)
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
            tempNode.data.table.AddCustomerToTable(customer);
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
    public Table table;
    public Vector2Int chairLocation;
    public bool tableIsFree;

    public TableNode(Table currentTable, Vector2Int location, bool isFree)
    {
      table = currentTable;
      chairLocation = location;
      tableIsFree = isFree;
    }
}
