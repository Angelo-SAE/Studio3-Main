using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    [SerializeField] private Customer customer;
    public bool goingToReception, goingToTable, goingToCashier;

    private void Start()
    {
      customerAnimator.runtimeAnimatorController = customerAnimatorSelector.animator[Random.Range(0, customerAnimatorSelector.animator.Length)];
      closedList = new LinkedList<GridNode>();
      openList = new LinkedList<GridNode>();
      GenerateGridNodes();
      MovePlayerToFrontCounter();
    }

    private void FixedUpdate()
    {
      if(moving)
      {
        MoveTowardsPoint();
      }
    }

    //Get Start and End point

    [SerializeField] private GameObjectObject frontCounter, cashier;
    [SerializeField] private float movementSpeed;

    private void MovePlayerToFrontCounter()
    {
      frontCounter.value.GetComponent<FrontCounter>().AddCustomer(customer);
      startPosition = frontCounter.value.GetComponent<FrontCounter>().GetNextSpot();
      endPosition = new Vector2Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y));
      moving = false;
      foundPath = false;
      goingToReception = true;
      FindStarPath();
    }

    public void MovePlayerToCashier()
    {
      cashier.value.GetComponent<Cashier>().AddCustomer(customer);
      startPosition = cashier.value.GetComponent<Cashier>().GetNextSpot();
      endPosition = new Vector2Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y));
      moving = false;
      foundPath = false;
      FindStarPath();
    }

    public void MoveToPosition(Vector2Int startPos)
    {
      startPosition = startPos;
      endPosition = new Vector2Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y));
      moving = false;
      foundPath = false;
      FindStarPath();
    }

    //Finding Path
    [Header("Path Finding")]
    [SerializeField] private GameObject pathPiece;

    [SerializeField] private Vector2Int startPosition, endPosition;
    [SerializeField] private int orthogonalMovementToll, maxChecks;
    private LinkedList<GridNode> closedList, openList;
    private GridNode currentNode, startNode, endNode;
    private bool foundPath, moving;

    private void FindStarPath()
    {
      ClearGridNodes();
      endNode = gridNodes[endPosition.x, endPosition.y];
      startNode = gridNodes[startPosition.x, startPosition.y];
      CheckForPathFound(startNode);
      GetNodeNeighbours(startNode);
    }

    private void GetNodeNeighbours(GridNode node)
    {
      //Debug.Log(node.gridPosition);
      currentNode = node;
      CloseNode(currentNode);
      for(int a = 0; a < movementDirection.Count; a++)
      {
        int currentXDirection = node.gridPosition.x + movementDirection[a].x;
        int currentYDirection = node.gridPosition.y + movementDirection[a].y;
        if(currentXDirection < gridNodes.GetLength(0) && currentXDirection >= 0 && currentYDirection < gridNodes.GetLength(1) && currentYDirection >= 0)
        {
          if(!gridNodes[currentXDirection, currentYDirection].closed && gridNodes[currentXDirection, currentYDirection].isWalkable)
          {
            //Debug.Log("CheckingCost");
            //Debug.Log(gridNodes[currentXDirection, currentYDirection].gridPosition);
            //Debug.Log(gridNodes[currentXDirection, currentYDirection].isWalkable);
            CalculateGCost(gridNodes[currentXDirection, currentYDirection]);
          }
        }
      }
      if(!foundPath && openList.Count() > 0)
      {
        CheckNextNode();
      }
    }

    private void CheckNextNode()
    {
      GetNodeNeighbours(openList.PopFirst());
    }

    private void CalculateGCost(GridNode node)
    {
      int tempGCost = currentNode.gCost + orthogonalMovementToll; //Calculating G cost moving from the current node.

      //Checks to see if the node is currently open. If it is then there is no need to check if the current H cost is more or not. Also need to get H cost because it does not have one.
      if(!node.open)
      {
        node.gCost = tempGCost;
        node.parentNode = currentNode;
        CalculateHCost(node);
      } else if(node.gCost > tempGCost) //If its already open then there is a need to check if the current G cost is more then the nodes G cost so we know if we have to replace it or not.
      {
        node.gCost = tempGCost;
        node.parentNode = currentNode;
        CalculateFCost(node);
      }
    }

    private void CalculateHCost(GridNode node)
    {
      int tempHCost = (Mathf.Abs(node.gridPosition.x - endNode.gridPosition.x) + Mathf.Abs(node.gridPosition.y - endNode.gridPosition.y)) * orthogonalMovementToll;
      node.hCost = tempHCost;
      CalculateFCost(node);
    }

    private void CalculateFCost(GridNode node)
    {
      node.fCost = node.gCost + node.hCost;
      SortNodeIntoList(node);
      CheckForPathFound(node);
    }

    private void SortNodeIntoList(GridNode node)
    {
      if(!node.open)
      {
        if(openList.Count() == 0)
        {
          openList.AddToFront(node);
          node.open = true;
        } else {
          for(int a = 0; a < openList.Count(); a++)
          {
            if(node.fCost <= openList.GetElementAt(a).fCost)
            {
              openList.InsertElementAt(a, node);
              node.open = true;
              break;
            }
          }
        }
      }
      if(!node.open)
      {
        openList.AddToBack(node);
        node.open = true;
      }
    }

    private void CloseNode(GridNode node)
    {
      closedList.AddToFront(node);
      node.open = false;
      node.closed = true;
    }

    private void CheckForPathFound(GridNode node)
    {
      //Debug.Log("Checking" + node.gridPosition);
      if(node.gridPosition.x == endPosition.x && node.gridPosition.y == endPosition.y)
      {
        //Debug.Log("Found");
        currentNode = node;
        moving = true;
        foundPath = true;
      }
    }

    private void ClearGridNodes()
    {
      int xGrid = Mathf.Abs((int)gridEnd.x - (int)gridStart.x);
      int yGrid = Mathf.Abs((int)gridEnd.y - (int)gridStart.y);
      for(int a = 0; a < xGrid; a++)
      {
        for(int b = 0; b < yGrid; b++)
        {
          GridNode tempNode = gridNodes[a,b];
          tempNode.open = false;
          tempNode.closed = false;
          tempNode.parentNode = null;
        }
      }
      closedList = new LinkedList<GridNode>();
      openList = new LinkedList<GridNode>();
      //for(int a = 0; a <= openList.Count(); a++)
      //{
      //  GridNode tempNode = openList.PopFirst();
      //  tempNode.open = false;
      //  tempNode.parentNode = null;
      //}
      //for(int a = 0; a <= closedList.Count(); a++)
      //{
      //  GridNode tempNode = closedList.PopFirst();
      //  tempNode.closed = false;
      //  tempNode.parentNode = null;
      //}
    }

    private void MoveTowardsPoint()
    {
      transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentNode.worldPosition.x, currentNode.worldPosition.y), movementSpeed);
      GetDirection(transform.position, new Vector2(currentNode.worldPosition.x, currentNode.worldPosition.y));
      if((Vector2)transform.position == new Vector2(currentNode.worldPosition.x, currentNode.worldPosition.y))
      {
        if(currentNode.parentNode is null)
        {
          foundPath = false;
          moving = false;
          currentDirection = 4;
          if(goingToReception)
          {
            goingToReception = false;
            customer.isAtReception = true;
            AnimateCustomer(4);
          } else if(goingToTable)
          {
            goingToTable = false;
            customer.isAtTable = true;
            AnimateCustomer(5);
          } else if(goingToCashier)
          {
            goingToCashier = false;
            customer.isAtCashier = true;
            AnimateCustomer(4);
          } else {
            AnimateCustomer(4);
          }

        } else {
          currentNode = currentNode.parentNode;
        }
      }
    }

    //Customer Animations
    [Header("Customer Animations")]
    [SerializeField] private CustomerAnimatorObject customerAnimatorSelector;
    [SerializeField] private Animator customerAnimator;
    private int currentDirection;
    //0 = up
    //1 = right
    //2 = down
    //3 = left
    //4 = Idle
    //5 = Sit Top

    private void GetDirection(Vector2 customerPosition, Vector2 endPoint)
    {
      Vector2 tempDirection = endPoint - customerPosition;
      if(tempDirection.y > 0)
      {
        CheckForNewDirection(0);
      } else if(tempDirection.x > 0)
      {
        CheckForNewDirection(1);
      } else if(tempDirection.y < 0)
      {
        CheckForNewDirection(2);
      } else if(tempDirection.x < 0)
      {
        CheckForNewDirection(3);
      }
    }

    private void CheckForNewDirection(int newDirection)
    {
      if(currentDirection != newDirection)
      {
        currentDirection = newDirection;
        AnimateCustomer(currentDirection);
      }
    }

    public void AnimateCustomer(int animationNumber)
    {
      switch(animationNumber)
      {
        case(0):
        customerAnimator.Play("NPCWalkUp");
        break;
        case(1):
        customerAnimator.Play("NPCWalkRight");
        break;
        case(2):
        customerAnimator.Play("NPCWalkDown");
        break;
        case(3):
        customerAnimator.Play("NPCWalkLeft");
        break;
        case(4):
        customerAnimator.Play("NPCIdle");
        break;
        case(5):
        customerAnimator.Play("NPCSitTop");
        break;
      }
    }

    //Grid Stuff
    [Header("GridStuff")]
    [SerializeField] private Vector2Int gridStart;
    [SerializeField] private Vector2Int gridEnd;
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private int nodeHeight, nodeLength,nodeWidth;
    private GridNode[,] gridNodes;

    private void GenerateGridNodes()
    {
      int tempXStart = 0;
      int tempYStart = 0;
      int xGrid = Mathf.Abs(gridEnd.x - gridStart.x);
      int yGrid = Mathf.Abs(gridEnd.y - gridStart.y);
      gridNodes = new GridNode[xGrid, yGrid];

      for(int a = 0; a < xGrid; a++)
      {
        for(int b = 0; b < yGrid; b++)
        {
          Vector3Int nodeGridPosition = new Vector3Int(tempXStart, tempYStart, 0);
          Vector2 nodeWorldPosition = new Vector2((tempXStart * nodeWidth) + (nodeWidth/2f), (tempYStart * nodeHeight) + (nodeHeight/2f));
          //Debug.Log(Physics2D.OverlapBox(nodeWorldPosition, new Vector2(nodeWidth/1.5f, nodeHeight/1.5f), 0f, walkableLayer));
          if(Physics2D.OverlapBox(nodeWorldPosition, new Vector2(nodeWidth/1.5f, nodeHeight/1.5f), 0f, walkableLayer) is not null)
          {
            gridNodes[a,b] = new GridNode(nodeGridPosition, nodeWorldPosition, true);
          } else {
            //Debug.Log("waodwoaod");
            gridNodes[a,b] = new GridNode(nodeGridPosition, nodeWorldPosition, false);
          }
          tempYStart++;
        }
        tempYStart = 0;
        tempXStart++;
      }
    }

    private List<Vector3Int> movementDirection = new List<Vector3Int>
    {
      new Vector3Int(0,1,0),
      new Vector3Int(1,0,0),
      new Vector3Int(0,-1,0),
      new Vector3Int(-1,0,0)
    };
}
