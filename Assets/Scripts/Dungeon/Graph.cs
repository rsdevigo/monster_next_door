using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public static List<Vector2Int> neighbour4directions = new List<Vector2Int>{

        new Vector2Int(0,1), 
        new Vector2Int(1,0), 
        new Vector2Int(0,-1), 
        new Vector2Int(-1,0) 
    };

    public static List<Vector2Int> neighbour8directions = new List<Vector2Int>()
    {
        new Vector2Int(0,1), // Cima
        new Vector2Int(1,0), // Direita
        new Vector2Int( 0,-1), // Baixo
        new Vector2Int(-1,0), // Esquerda
        new Vector2Int(1,1), // Cima - direita
        new Vector2Int(1,-1), // Direita - baixo
        new Vector2Int(-1,-1), // Baixo - esquerda
        new Vector2Int(-1,1) // Esquerda - cima
    };
    List<Vector2Int> graph;
    public Graph (IEnumerable<Vector2Int> vertices)
    {
        graph = new List<Vector2Int>(vertices);

    }
    public List<Vector2Int> GetNeighbour4directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbour4directions);
    }
    public List<Vector2Int> GetNeighbour8directions(Vector2Int startPosition)
    {
        return GetNeighbours(startPosition, neighbour8directions);
    }

    private List<Vector2Int> GetNeighbours(Vector2Int startPosition, List<Vector2Int> neighboursOffsetList)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        foreach( var neighbourDirection in neighboursOffsetList )
        {
            Vector2Int potetialNeighbour = startPosition + neighbourDirection;
            if(graph.Contains(potetialNeighbour))
            {
                neighbours.Add(potetialNeighbour);
            }
        }
        return neighbours;
    }
}
