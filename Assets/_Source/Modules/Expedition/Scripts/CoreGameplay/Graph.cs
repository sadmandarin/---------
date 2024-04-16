using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Expedition
{
    public class Graph : MonoBehaviour
    {
        public List<CastleNode> castleNodes;

        public List<Road> roads;

        public Graph()
        {
            //castleNodes = new List<CastleNode>();

            roads = new List<Road>();
        }

        public void AddNode(GameObject nodeObject)
        {
            CastleNode castleNode = nodeObject.GetComponent<CastleNode>();

            if (castleNode != null)
            {
                castleNodes.Add(castleNode);
            }

            else
            {
                Debug.LogError("The provided GameObject does not have a Road component.");
            }
        }

        public void AddRoadBetweenCastles(GameObject castle, CastleNode castleObject1, CastleNode castleObject2)
        {
            Road road = castle.GetComponent<Road>();

            if (castleObject1 != null && castleObject2 != null)
            {
                roads.Add(road);

            }
            else
            {
                Debug.LogError("One or both of the provided GameObjects do not have a CastleNode component.");
            }
        }

        //public List<CastleNode> FindAllNodesBetween(CastleNode startNode, CastleNode endNode)
        //{
        //    var visited = new HashSet<CastleNode>();
        //    var currentPath = new List<CastleNode>();
        //    List<CastleNode> shortestPath = new List<CastleNode>();    

        //    DFS(startNode, endNode, visited, currentPath, ref shortestPath);

        //    return shortestPath;
        //}

        //private void DFS(CastleNode currentNode, CastleNode endNode, HashSet<CastleNode> visited, List<CastleNode> currentPath, ref List<CastleNode> shortestPath)
        //{
        //    visited.Add(currentNode);

        //    currentPath.Add(currentNode);

        //    if (currentNode == endNode)
        //    {
        //        if (shortestPath.Count == 0 && currentPath.Count < shortestPath.Count + 10)
        //        {
        //            shortestPath = new List<CastleNode>(currentPath);
        //        }
        //    }

        //    else
        //    {
        //        foreach (var connectedNode in currentNode.connectedCastles)
        //        {
        //            if (!visited.Contains(connectedNode) && endNode.gameObject.layer == connectedNode.gameObject.layer)
        //            {
        //                DFS(connectedNode, endNode, visited, currentPath, ref shortestPath);
        //            }
        //        }
        //    }

        //    currentPath.Remove(currentNode);
        //    //visited.Remove(currentNode);

        //}

        public List<CastleNode> FindAllNodesBetween(CastleNode startNode, CastleNode endNode)
        {
            var visited = new HashSet<CastleNode>();
            var shortestPath = BFS(startNode, endNode);

            return shortestPath;
        }

        private List<CastleNode> BFS(CastleNode startNode, CastleNode endNode)
        {
            Queue<List<CastleNode>> queue = new Queue<List<CastleNode>>();
            HashSet<CastleNode> visited = new HashSet<CastleNode>();

            queue.Enqueue(new List<CastleNode> { startNode });
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                List<CastleNode> currentPath = queue.Dequeue();
                CastleNode currentNode = currentPath.Last();

                if (currentNode == endNode)
                {
                    return currentPath;
                }

                foreach (var connectedNode in currentNode.connectedCastles)
                {
                    if (connectedNode != null && !visited.Contains(connectedNode))
                    {
                        if (startNode.gameObject.layer == currentNode.gameObject.layer || currentNode.gameObject.layer == LayerMask.NameToLayer("Between"))
                        {
                            visited.Add(connectedNode);
                            List<CastleNode> newPath = new List<CastleNode>(currentPath)
                            {
                                connectedNode
                            };
                            queue.Enqueue(newPath);
                        }
                    }
                }
            }
            // Если путь не найден
            return new List<CastleNode>();
        }

        private bool IsConnectedByLayer(CastleNode startNode, CastleNode connectedNode, CastleNode endNode)
        {
            if (startNode == endNode)
            {
                // Если стартовый и конечный узлы совпадают, считаем их связанными
                return true;
            }

            // Если промежуточный узел имеет такой же слой, как стартовый или конечный,
            // считаем их связанными
            return connectedNode.gameObject.layer == startNode.gameObject.layer;
        }

    }
}
