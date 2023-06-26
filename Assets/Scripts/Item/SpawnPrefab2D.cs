using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class SpawnPrefab2D : MonoBehaviour
{
    public List<GameObject> prefabs;
    public Tilemap tilemap;

    void Start()
    {
        SpawnPrefabsAtTilemap();
    }

    void SpawnPrefabsAtTilemap()
    {
        BoundsInt tilemapBounds = tilemap.cellBounds;

        int prefabCount = prefabs.Count;

        for (int i = 0; i < prefabCount; i++)
        {
            Vector3Int randomTilePosition = GetRandomTilePosition();
            if (randomTilePosition != Vector3Int.zero)
            {
                Vector3 spawnPosition = tilemap.CellToWorld(randomTilePosition) + tilemap.cellSize * 0.5f;
                Instantiate(prefabs[i], spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3Int GetRandomTilePosition()
    {
        BoundsInt tilemapBounds = tilemap.cellBounds;
        List<Vector3Int> tilePositions = new List<Vector3Int>();

        foreach (var position in tilemapBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                tilePositions.Add(position);
            }
        }

        if (tilePositions.Count > 0)
        {
            int randomIndex = Random.Range(0, tilePositions.Count);
            return tilePositions[randomIndex];
        }

        return Vector3Int.zero;
    }
}
