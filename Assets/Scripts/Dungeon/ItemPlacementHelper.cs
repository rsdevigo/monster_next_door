using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPlacementHelper
{
  Dictionary<PlacementType,HashSet<Vector2Int>> tileByTipe = new Dictionary<PlacementType, HashSet<Vector2Int>>();

    HashSet<Vector2Int> roomFloorNoCorridor;

    public ItemPlacementHelper (HashSet<Vector2Int> roomFloor,HashSet<Vector2Int> roomFloorNoCorridor)
    {
        Graph graph = new Graph(roomFloor);
        this.roomFloorNoCorridor = roomFloorNoCorridor;

        foreach (var position in roomFloorNoCorridor) 
        {
            int neighboursCount8Dir = graph.GetNeighbour8directions(position).Count;
            PlacementType type = neighboursCount8Dir < 8 ? PlacementType.NearWall : PlacementType.OpenSpace;
            if(tileByTipe.ContainsKey(type)==false)
            {
                tileByTipe[type] = new HashSet<Vector2Int>();
            }
            if (type == PlacementType.NearWall && graph.GetNeighbour4directions(position).Count < 4)
                continue;
            tileByTipe[type].Add(position);
        
        }
    }

    public Vector2? GetItemPlacementPosition(PlacementType placementType, int interationsMax, Vector2Int size, bool addOffset)
    {
        int itemArea = size.x*size.y;
        if (tileByTipe[placementType].Count < itemArea)
        {
            return null;
        }
        int interation = 0;
        while (interation< interationsMax)
        {
            interation++;
            int index = Random.Range(0, tileByTipe[placementType].Count);
            Vector2Int position= tileByTipe[placementType].ElementAt(index);
            if (itemArea > 1) 
            {
                var (result, placementPositions) = PlaceBigItem(position, size, addOffset);

                if (result == false)
                    continue;

                tileByTipe[placementType].ExceptWith(placementPositions);
                tileByTipe[PlacementType.NearWall].ExceptWith(placementPositions);
            }
            else
            {
                tileByTipe[placementType].Remove(position);
            }
            return position;
        }
        return null;
    }

    private(bool, List<Vector2Int>) PlaceBigItem(
        Vector2Int originPosition,Vector2Int size, bool addOffset)
    {
        List<Vector2Int> positions = new List<Vector2Int>() { originPosition };
        int maxX = addOffset ? size.x + 1: size.x;
        int maxY = addOffset ? size.y + 1: size.y;
        int minX = addOffset ? size.x - 1 : 0;
        int minY = addOffset ? size.y - 1 : 0;

        for (int row = minX; row <= maxX; row++)
        {
            for (int col = minY; col < maxY; col++)
            {
                if(col == 0 && row == 0)
                {
                    continue;
                }
                Vector2Int newPosToCheck = new Vector2Int(originPosition.x + row, originPosition.y + col);
                if(roomFloorNoCorridor.Contains(newPosToCheck)==false)
                {
                    return (false, positions);
                }
                positions.Add(newPosToCheck);
            }
        }
        return (true,positions);
    }
}
public enum PlacementType
{ 
    OpenSpace,
    NearWall

}


