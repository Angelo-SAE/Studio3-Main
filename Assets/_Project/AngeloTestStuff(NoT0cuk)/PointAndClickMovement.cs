using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStarFind : MonoBehaviour
{

    private void Start()
    {
      closedList = new LinkedList<GridNode>();
      openList = new LinkedList<GridNode>();
      GenerateGridNodes();
    }

    private void Update()
    {
      DetectMovementClick();


    }

    private void FixedUpdate()
    {
      if(moving)
      {
        MoveTowardsPoint();
      }
    }

    //Get Start and End point

    private Vector2Int mousePosition;
    [SerializeField] private float movementSpeed;

    private void DetectMovementClick()
    {
      if(Input.GetButtonDown("Movement"))
      {
        Vector2 tempPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2Int((int)Mathf.Floor(tempPosition.x), (int)Mathf.Floor(tempPosition.y));
        CheckForValidMovementSpot();
      }
    }

    private void CheckForValidMovementSpot()
    {
      if(mousePosition.x >= gridStart.x && mousePosition.x < gridEnd.x && mousePosition.y >= gridStart.y && mousePosition.y < gridEnd.y)
      {
        startPosition = mousePosition;
        endPosition = new Vector2Int((int)Mathf.Floor(transform.position.x), (int)Mathf.Floor(transform.position.y));
        moving = false;
        foundPath = false;
        FindStarPath();
      }
    }

    //Finding Path
    [Header("Path Finding")]
    [SerializeField] private GameObject pathPiece;

    [SerializeField] private Vector2Int startPosition, endPosition;
    [SerializeField] private int orthogonalMovementToll, maxChecks;
    private LinkedList<GridNode> closedList, openList;
    private GridNode currentNode, startNode, endNode;
    private bool foundPath, moving;
    private int tempCheck;

    private void FindStarPath()
    {
      ClearGridNodes();
      tempCheck = 0;
      endNode = gridNodes[endPosition.x, endPosition.y];
      startNode = gridNodes[startPosition.x, startPosition.y];
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
      } else if(tempCheck > maxChecks)
      {

      }
      tempCheck++;
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
    }

    private void MoveTowardsPoint()
    {
      transform.position = Vector2.MoveTowards(transform.position, new Vector2(currentNode.worldPosition.x + 0.5f, currentNode.worldPosition.y + 0.5f), movementSpeed);
      if((Vector2)transform.position == new Vector2(currentNode.worldPosition.x + 0.5f, currentNode.worldPosition.y + 0.5f))
      {
        if(currentNode.parentNode is null)
        {
          foundPath = false;
          moving = false;
        } else {
          currentNode = currentNode.parentNode;
        }
      }
    }

    //Grid Stuff
    [Header("GridStuff")]
    [SerializeField] private Vector3 gridStart;
    [SerializeField] private Vector3 gridEnd;
    [SerializeField] private LayerMask walkableLayer;
    [SerializeField] private int nodeHeight, nodeLength,nodeWidth;
    private GridNode[,] gridNodes;

    private void GenerateGridNodes()
    {
      int tempXStart = 0;
      int tempYStart = 0;
      int xGrid = Mathf.Abs((int)gridEnd.x - (int)gridStart.x);
      int yGrid = Mathf.Abs((int)gridEnd.y - (int)gridStart.y);
      gridNodes = new GridNode[xGrid, yGrid];

      for(int a = 0; a < xGrid; a++)
      {
        for(int b = 0; b < yGrid; b++)
        {
          Vector3Int nodeGridPosition = new Vector3Int(tempXStart, tempYStart, 0);
          Vector2 nodeWorldPosition = new Vector2(tempXStart * nodeWidth, tempYStart * nodeHeight);
          //Debug.Log(Physics2D.OverlapBox(nodeWorldPosition, new Vector2(nodeWidth/2, nodeHeight/2), 0, walkableLayer));
          if(Physics2D.OverlapBox(nodeWorldPosition, new Vector2(nodeWidth/2, nodeHeight/2), 0, walkableLayer) is not null)
          {
            gridNodes[a,b] = new GridNode(nodeGridPosition, nodeWorldPosition, true);
          } else {
            Debug.Log("waodwoaod");
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
