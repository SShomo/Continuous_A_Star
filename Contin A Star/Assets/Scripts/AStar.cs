using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.Experimental.GraphView;
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

    public List<Tile> generatePath()
    {
        Dictionary<Tile, Tile> cameFrom; // to build the flowfield and build the path
        Data.PriorityQueue<float, Node> frontier; // to store next ones to visit
        HashSet<Vector2> frontierSet; //what positions are already found
        Dictionary<Tile, bool> visited; // use .at() to get data, if the element dont exist [] will give you wrong results
        List<Tile> path;

        // bootstrap
        Node t = new Node();
        frontier = new Data.PriorityQueue<float, Node>();
        frontierSet = new HashSet<Vector2>();
        visited = new Dictionary<Tile, bool> ();
        cameFrom = new Dictionary<Tile, Tile> ();
        path = new List<Tile> ();

        Node catPos = TextFields.source;
        t.currentTile = catPos.currentTile;
        t.costSoFar = 0;


        frontier.Enqueue(t.costSoFar, t);
        frontierSet.Add(t.currentTile.currentPos);
        Vector2 desPos = TextFields.des.currentTile.currentPos; // if at the end of the loop we dont find a border, we have to return random points

        Node current = new Node();
        bool found = false;
        while (!found)
        {
            // get the current from frontier
            Node first = frontier.Dequeue();
            current.currentTile = first.currentTile;
            current.costSoFar = first.costSoFar;
            frontierSet.Remove(current.currentTile.currentPos);


            // mark current as visited
            visited[current.currentTile] = true;
            // getVisitableNeighbors(world, current) returns a vector of neighbors that are not visited, not cat, not block, not in the queue
            List<Tile> neigh = getVisitableNeighbors(current, frontierSet, visited);
            // iterate over the neighs:
            foreach(Tile tile in neigh)
            {
                if (desPos == tile.currentPos)
                {
                    cameFrom[TextFields.des.currentTile] = current.currentTile;
                    found = true;
                    break;
                }
                //Vector2 returnVec;
                //frontierSet.TryGetValue(node.currentTile.currentPos, out returnVec);
                //this is never true
                if (!frontierSet.Contains(tile.currentPos))
                {
                    //int e = cost   
                    //if(frontierSet.find(var))
                    cameFrom[tile] = current.currentTile; // for every neighbor set the cameFrom
                                                   // enqueue the neighbors to frontier and frontierset

                    //if(frontierSet.find(var))
                    Node tempNode = new Node();
                    tempNode.currentTile = tile;
                    tempNode.costSoFar = current.costSoFar + 1;
                    frontier.Enqueue(tempNode.costSoFar, tempNode);
                    frontierSet.Add(tile.currentPos);

                    visited[tile] = true;
                    // do this up to find a visitable border and break the loop
                }
            }
            if (found)
                break;
        }
        // if the border is not infinity, build the path from border to the cat using the camefrom map
        /*        if (desPos == textField.des.currentTile.currentPos)
                {
                    return path;
                }
        */
        int wf = 0;
        Tile current2 = TextFields.des.currentTile;
        while (current2 != TextFields.source.currentTile)
        {
            path.Add(current2);
            current2 = cameFrom[current2];
            wf++;
            Debug.Log(current2.currentPos);
            if(wf > 1000){ break; }
        }
        return path;
        // if your vector is filled from the border to the cat, the first element is the catcher move, and the last element is the cat move
    }

    //assumes that we only have walls, if we make more comprehensive weights we will also need to change this accordingly
    public List<Tile> getVisitableNeighbors(Node current, HashSet<Vector2> frontierSet, Dictionary<Tile, bool> visited)
    {
        List<Tile> validTileNeighbors = current.currentTile.GetNeighbors();
        List < Tile > cleanup = new List<Tile>();

        foreach (Tile neighbor in validTileNeighbors)
        {
            if (visited.ContainsKey(neighbor))
            {
                if (neighbor.GetWeight() > 0 || frontierSet.Contains(neighbor.currentPos) || visited[neighbor])
                {
                    cleanup.Add(neighbor);
                }
            }
            else if (neighbor.GetWeight() > 0 || frontierSet.Contains(neighbor.currentPos))
            {
                cleanup.Add(neighbor);
            }
        }

        foreach(Tile neighbor in cleanup)
        {
            validTileNeighbors.Remove(neighbor);
        }

        return validTileNeighbors;
    }
}
