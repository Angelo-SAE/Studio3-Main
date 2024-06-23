using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tables : MonoBehaviour
{
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] public UnityEvent ServedCustomer;
    private List<Table> tables;

    private void Awake()
    {
      tablesAvailable.value = transform.childCount;
    }

    public void CreateTableNode(Table table)
    {
      if(tables is null)
      {
        tables = new List<Table>();
      }
      tables.Add(table);
    }

    public Vector2Int GetChairPosition(Customer customer)
    {
      if(tablesAvailable.value != 0)
      {
        for(int a = 0; a < tables.Count; a++)
        {
          if(tables[a].tableIsFree)
          {
            tablesAvailable.value--;
            tables[a].tableIsFree = false;
            tables[a].AddCustomerToTable(customer);
            return tables[a].chairPosition;
          }
        }
      }
      return Vector2Int.zero;
    }
}
