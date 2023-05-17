using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
  [SerializeField]
  private Tilemap flootTileMap,wallTileMap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
          wallInnerCornerDownLeft, wallInnerCornerDownRigth, wallDiagonalCornerDownRigth, wallDiagonalCornerDownLeft,
          wallDiagonalCornerUpRigth, wallDiagonalCornerUpLeft, wallTile;

  public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions){
    PaintTiles(floorPositions,flootTileMap,floorTile);
  }
  private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap,TileBase tile){
    foreach( var position in positions ){
        PaintSingleTile(tilemap,tile,position);
    }

  }
  private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position){
    var tilePosition = tilemap.WorldToCell((Vector3Int)position);
    tilemap.SetTile(tilePosition,tile);

  }
  public void Clear(){
    flootTileMap.ClearAllTiles();
    wallTileMap.ClearAllTiles();
  }
    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallByteTypes.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallByteTypes.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallByteTypes.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallByteTypes.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallByteTypes.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallByteTypes.wallTile.Contains(typeAsInt))
        {
            tile = wallTile;
        }
        if (tile != null)
        {
            PaintSingleTile(wallTileMap, tile, position);

        }
       
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallByteTypes.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRigth;
        }
        else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRigth;
        }
        else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRigth;
        }
        else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallByteTypes.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
        {
            PaintSingleTile(wallTileMap, tile, position);

        }
    }
}
