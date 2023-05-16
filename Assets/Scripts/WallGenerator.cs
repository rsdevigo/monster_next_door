using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
   public static void CreateWalls(HashSet<Vector2Int> floorPositions,TileMapVisualizer tileMapVisualizer)
    {
        var basicWallPositions = FindeWAllsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallsPositions = FindeWAllsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tileMapVisualizer, basicWallPositions,floorPositions);
        CreateCornerWalls(tileMapVisualizer, cornerWallsPositions, floorPositions);
    }

    private static void CreateCornerWalls(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> cornerWallsPositions,
        HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallsPositions)
        {
            string neighbourBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition))
                {
                    neighbourBinaryType += "1";
                }
                else
                {
                    neighbourBinaryType += "0";
                }
            }
            tileMapVisualizer.PaintSingleCornerWall(position,neighbourBinaryType);
        }
    }
    private static void CreateBasicWalls(TileMapVisualizer tileMapVisualizer, HashSet<Vector2Int> basicWallsPositions,
       HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallsPositions)
        {
            string neighbourBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighbourBinaryType += "1";
                }
                else
                {
                    neighbourBinaryType += "0";
                }
            }
            tileMapVisualizer.PaintSingleBasicWall(position, neighbourBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindeWAllsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions= new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
