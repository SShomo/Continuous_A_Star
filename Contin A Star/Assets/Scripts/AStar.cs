using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;
using Vector2 = UnityEngine.Vector2;


public class AStar : MonoBehaviour
{
    TextFields textField;

    private void Start()
    {
        textField = GetComponent<TextFields>();
    }

    public List<Node> generatePath()
    {
        Dictionary<Node, Node> cameFrom; // to build the flowfield and build the path

        Data.PriorityQueue<float, Node> frontier; // to store next ones to visit
        HashSet<Vector2> frontierSet; //what positions are already found
        Dictionary<Tile, bool> visited; // use .at() to get data, if the element dont exist [] will give you wrong results
        List<Node> path;

        // bootstrap
        Node t = new Node();
        frontier = new Data.PriorityQueue<float, Node>();
        frontierSet = new HashSet<Vector2>();
        visited = new Dictionary<Tile, bool> ();
        cameFrom = new Dictionary<Node, Node> ();
        path = new List<Node> ();

        Node catPos = textField.source;
        t.currentTile = catPos.currentTile;
        t.costSoFar = 0;


        frontier.Enqueue(t.costSoFar, t);
        frontierSet.Add(t.currentTile.currentPos);
        Node borderExit = textField.des; // if at the end of the loop we dont find a border, we have to return random points

        Node current = new Node();
        while (frontier.Count != 0)
        {
            // get the current from frontier
            Node first = frontier.Dequeue();
            current.currentTile = first.currentTile;
            current.costSoFar = first.costSoFar;
            frontierSet.Remove(current.currentTile.currentPos);

            // remove the current from frontierset
            if (textField.des.currentTile.currentPos == current.currentTile.currentPos)
            {
                borderExit = current;
                break;
            }

            // mark current as visited
            visited[current.currentTile] = true;
            // getVisitableNeighbors(world, current) returns a vector of neighbors that are not visited, not cat, not block, not in the queue
            List<Node> neigh = getVisitableNeighbors(current, frontierSet, visited);
            // iterate over the neighs:
            foreach(Node node in neigh)
            {
                Vector2 returnVec;
                frontierSet.TryGetValue(node.currentTile.currentPos, out returnVec);
                if (frontierSet.Count != 0 && returnVec == frontierSet.Last())
                {
                    //int e = cost
                    
                    //if(frontierSet.find(var))
                    cameFrom[node] = current; // for every neighbor set the cameFrom
                                                   // enqueue the neighbors to frontier and frontierset

                    //if(frontierSet.find(var))
                    frontier.Enqueue(node.costSoFar, node);
                    frontierSet.Add(node.currentTile.currentPos);

                    visited[node.currentTile] = true;
                    // do this up to find a visitable border and break the loop
                }
            }
        }
        // if the border is not infinity, build the path from border to the cat using the camefrom map
        if (borderExit.currentTile.currentPos == textField.des.currentTile.currentPos)
        {
            return path;
        }

        Node current2 = borderExit;
        while (current2.currentTile != catPos.currentTile)
        {
            path.Add(current2);
            current2 = cameFrom[current2];
        }
        //path.push_back(catPos);
        return path;
        // if your vector is filled from the border to the cat, the first element is the catcher move, and the last element is the cat move
    }

    //assumes that we only have walls, if we make more comprehensive weights we will also need to change this accordingly
    public List<Node> getVisitableNeighbors(Node current, HashSet<Vector2> frontierSet, Dictionary<Tile, bool> visited)
    {
        List<Tile> validTileNeighbors = current.currentTile.GetNeighbors();
        List<Node> validNodeNeighbors = new List<Node>();

        Node temp = new Node();
        foreach (Tile neighbor in validTileNeighbors)
        {
            temp.InitNode(neighbor, current.costSoFar + 1);
            if (visited.ContainsKey(temp.currentTile))
            {
                if (temp.currentTile.GetWeight() == 0 && !frontierSet.Contains(temp.currentTile.currentPos) && !visited[temp.currentTile])
                {
                    validNodeNeighbors.Add(temp);
                }
            }
            else if (temp.currentTile.GetWeight() == 0 && !frontierSet.Contains(temp.currentTile.currentPos))
            {
                validNodeNeighbors.Add(temp);
            }
            validNodeNeighbors.Add(temp);
        }

        return validNodeNeighbors;
    }
}
