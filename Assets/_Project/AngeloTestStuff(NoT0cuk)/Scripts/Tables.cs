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
      tablesAvailable.value = tableCount;
      orders.order = new Order[tableCount];
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
