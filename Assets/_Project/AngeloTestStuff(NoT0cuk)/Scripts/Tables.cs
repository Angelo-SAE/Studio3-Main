using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tables : MonoBehaviour
{
    [SerializeField] private IntObject tablesAvailable;
    [SerializeField] private OrderObject orders;
    [SerializeField] private int tableCount;
    [SerializeField] public UnityEvent ServedCustomer;
    private List<Table> tables;

    private void Awake()
    {
      //int tables = transform.childCount;
      orders.order = new Order[tableCount];
      orders.pairOrder = new Order[tableCount];
    }

    public void CreateTableNode(Table table)
    {
      if(tables is null)
      {
        tables = new List<Table>();
      }
      tables.Add(table);
      tablesAvailable.value++;
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
            return tables[a].ChairPosition[0];
          }
        }
      }
      return Vector2Int.zero;
    }

    public Vector2Int[] GetChairPositions(Customer[] customers)
    {
      Vector2Int[] tempPositions = new Vector2Int[customers.Length];
      if(tablesAvailable.value != 0)
      {
        for(int a = 0; a < tables.Count; a++)
        {
          if(tables[a].tableIsFree)
          {
            tablesAvailable.value--;
            tables[a].tableIsFree = false;
            for(int b = 0; b < customers.Length; b++)
            {
              tables[a].AddCustomerToTable(customers[b]);
              tempPositions[b] = tables[a].ChairPosition[b];
            }
            return tempPositions;
          }
        }
      }
      return tempPositions;
    }

    public void ResetTables()
    {
      for(int a = 0; a < tables.Count; a++)
      {
        tables[a].ResetTable();
      }
      tablesAvailable.value = tables.Count;
    }
}
