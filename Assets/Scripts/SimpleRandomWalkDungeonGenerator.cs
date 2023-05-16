using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;


   protected override void RunProceduralGeneration(){
    HashSet<Vector2Int> floorPosition = RunRandomWalk(randomWalkParameters,startposition);
    tileMapVisualizer.Clear();
    tileMapVisualizer.PaintFloorTiles(floorPosition);
    WallGenerator.CreateWalls(floorPosition, tileMapVisualizer);

   }
   protected HashSet<Vector2Int> RunRandomWalk( SimpleRandomWalkSO parameters, Vector2Int position){
    var currentPosition= position;
    HashSet<Vector2Int> floorPosition= new HashSet<Vector2Int>();
    for (int i =0; i < randomWalkParameters.iterations; i++){
        var path= ProceduralGeneration.SimpleRandomWalk(currentPosition, randomWalkParameters.walkLength);
        // uni a caminhada com a pos do piso, n deixando ter duplicatas
        floorPosition.UnionWith(path);
       
        if(randomWalkParameters.startRandomlyEachIterations)
            {
            currentPosition=floorPosition.ElementAt(Random.Range(0,floorPosition.Count));
        }
    }
    return floorPosition;
   }
}
