using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


public class AStar : MonoBehaviour
{
    Node source;
    Node des;
    public List<Node> generatePath()
    {
        Dictionary<Node, Node> cameFrom; // to build the flowfield and build the path

        Data.PriorityQueue<float, Node> frontier; // to store next ones to visit
        HashSet<Vector2> frontierSet; //what positions are already found
        Dictionary<Node, bool> visited; // use .at() to get data, if the element dont exist [] will give you wrong results
        List<Node> path;

        // bootstrap
        Node t = new Node();
        frontier = new Data.PriorityQueue<float, Node>();
        frontierSet = new HashSet<Vector2>();
        visited = new Dictionary<Node, bool> ();
        cameFrom = new Dictionary<Node, Node> ();

        Node catPos = source;
        t.currentTile = catPos.currentTile;
        t.costSoFar = 0;
        path = new List<Node> ();

        frontier.Enqueue(t.costSoFar, t);
        frontierSet.Add(t.currentTile.currentPos);
        Node borderExit = des; // if at the end of the loop we dont find a border, we have to return random points

        Node current = new Node();
        while (frontier.Count != 0)
        {
            // get the current from frontier
            Node first = frontier.Dequeue();
            current.currentTile = first.currentTile;
            current.costSoFar = first.costSoFar;
            frontierSet.Remove(current.currentTile.currentPos);

            // remove the current from frontierset
            if (source.currentTile.currentPos == current.currentTile.currentPos)
            {
                borderExit = current;
                break;
            }

            // mark current as visited
            visited[current] = true;
            // getVisitableNeighbors(world, current) returns a vector of neighbors that are not visited, not cat, not block, not in the queue
            List<Node> neigh = getVisitableNeighbors(current, frontierSet);
            // iterate over the neighs:
            foreach(Node node in neigh)
            {
                if (frontierSet.Contains(node.currentTile.currentPos) == frontierSet.Last())
                {
                    //int e = cost
                    //if(frontierSet.find(var))
                    cameFrom[node] = current; // for every neighbor set the cameFrom
                                                   // enqueue the neighbors to frontier and frontierset

                    //if(frontierSet.find(var))
                    frontier.Enqueue(node.costSoFar, node);
                    frontierSet.Add(node.currentTile.currentPos);

                    visited[node] = true;
                    // do this up to find a visitable border and break the loop
                }
            }
        }
        // if the border is not infinity, build the path from border to the cat using the camefrom map
        if (borderExit.currentTile.currentPos == des.currentTile.currentPos)
        {
            return path;
        }

        Node current2 = borderExit;
        while (current2 != catPos)
        {
            path.Add(current2);
            current2 = cameFrom[current2];
        }
        //path.push_back(catPos);
        return path;
        // if your vector is filled from the border to the cat, the first element is the catcher move, and the last element is the cat move
    }

    //assumes that we only have walls, if we make more comprehensive weights we will also need to change this accordingly
    List<Node> getVisitableNeighbors(Node current, HashSet<Vector2> existingSet)
    {
        List<Tile> validTileNeighbors = current.currentTile.GetNeighbors();
        List<Node> validNodeNeighbors = new List<Node>();

        //north
        Node north = new Node();
        north.InitNode(validTileNeighbors[0], current.costSoFar + 1);
        if(north.currentTile.GetWeight() == 0 && !existingSet.Contains(north.currentTile.currentPos)) 
        {
            validNodeNeighbors.Add(north);
        }
        //east
        Node east = new Node();
        east.InitNode(validTileNeighbors[1], current.costSoFar + 1);
        if (east.currentTile.GetWeight() == 0 && !existingSet.Contains(east.currentTile.currentPos))
        {
            validNodeNeighbors.Add(east);
        }
        //south
        Node south = new Node();
        south.InitNode(validTileNeighbors[2], current.costSoFar + 1);
        if (south.currentTile.GetWeight() == 0 && !existingSet.Contains(south.currentTile.currentPos))
        {
            validNodeNeighbors.Add(south);
        }
        //west
        Node west = new Node();
        west.InitNode(validTileNeighbors[3], current.costSoFar + 1);
        if (west.currentTile.GetWeight() == 0 && !existingSet.Contains(west.currentTile.currentPos))
        {
            validNodeNeighbors.Add(west);
        }

        return validNodeNeighbors;
    }

}
