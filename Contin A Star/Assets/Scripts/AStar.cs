using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


public class AStar : MonoBehaviour
{
    Node source;
    Node des;
    public List<Node> generatePath()
    {
        Dictionary<Node, Node> cameFrom; // to build the flowfield and build the path

        Data.PriorityQueue<float, Node> frontier; // to store next ones to visit
        HashSet<Node> frontierSet; // OPTIMIZATION to check faster if a point is in the queue
        Dictionary<Node, bool> visited; // use .at() to get data, if the element dont exist [] will give you wrong results
        List<Node> path;

        // bootstrap
        Node t = new Node();
        frontier = new Data.PriorityQueue<float, Node>();
        frontierSet = new HashSet<Node> ();
        visited = new Dictionary<Node, bool> ();
        cameFrom = new Dictionary<Node, Node> ();

        Node catPos = source;
        t.currentTile = catPos.currentTile;
        t.costSoFar = 0;
        path = new List<Node> ();

        frontier.Append(t);
        frontierSet.Add(catPos);
        Node borderExit = des; // if at the end of the loop we dont find a border, we have to return random points

        Node current = new Node();
        while (frontier.Count() != 0)
        {
            // get the current from frontier
            current.currentTile = frontier.First().currentTile;
            current.costSoFar = frontier.First().costSoFar;
            frontier.Dequeue();
            frontierSet.Remove(current);

            // remove the current from frontierset
            if (source.currentTile.currentPos == current.currentTile.currentPos)
            {
                borderExit = current;
                break;
            }

            // mark current as visited
            visited[current] = true;
            // getVisitableNeighbors(world, current) returns a vector of neighbors that are not visited, not cat, not block, not in the queue
            List<Node> neigh = getVisitableNeighbors(current);
            // iterate over the neighs:
            foreach(Node node in neigh)
            {
                if (frontierSet.Contains(node) == frontierSet.Last())
                {
                    //int e = cost
                    //if(frontierSet.find(var))
                    cameFrom[node] = current; // for every neighbor set the cameFrom
                                                   // enqueue the neighbors to frontier and frontierset

                    //if(frontierSet.find(var))
                    frontier.Append(node);
                    frontierSet.Add(node);

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


    List<Node> getVisitableNeighbors(Node current)
    {
        List<Node> newPos = new List<Node>();

/*        if (w->isValidPosition(World::N(current)))
            newPos.Add(World::N(current));
        if (w->isValidPosition(World::S(current)))
            newPos.Add(World::S(current));
        if (w->isValidPosition(World::E(current)))
            newPos.Add(World::E(current));
        if (w->isValidPosition(World::W(current)))
            newPos.Add(World::W(current));*/


        return newPos;
    }

}
