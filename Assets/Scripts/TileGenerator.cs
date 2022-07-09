using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

public class TileGenerator : MonoBehaviour
{

    
    [Range(0,100)]
    public int inichance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;

    [Range(1, 16)]
    public int tilepxcoeff;

    private int[,] terrainMap;
    public Vector3Int tMapSize;

    public Tilemap topMap;
    public Tilemap bottomMap;
    public Tile topTile;
    public Tile bottomTile;

    int width;
    int height;

    
    public void doSim(int numR)
    {
        clearMaps(false);
        width = tMapSize.x;
        height = tMapSize.y;

        if(terrainMap == null)
        {
            terrainMap = new int[width, height];
            initPos();
        }

        for (int i = 0; i < numR; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(terrainMap[x,y] == 1)
                {
                    topMap.SetTile(new Vector3Int((-x + width / 2) / tilepxcoeff, (-y + height / 2) / tilepxcoeff, 0), topTile);
                    
                }
                bottomMap.SetTile(new Vector3Int((-x + width / 2) / tilepxcoeff, (-y + height / 2) / tilepxcoeff, 0), bottomTile);
            }

        } 
            }

    private int[,] genTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighbors;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbors = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighbors += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighbors++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighbors < deathLimit) newMap[x, y] = 0;

                    else
                    {
                        newMap[x, y] = 1;

                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighbors > birthLimit) newMap[x, y] = 1;

                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }
        return newMap;
    }

    public void initPos()
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < inichance ? 1 : 0;
            }
        }
    }

    [Button("Clear Simulation")]
    public void cmap()
    {
        clearMaps(true);
    }

    [Button("Start Simulation")]
    public void dsim()
    {
        doSim(numR);
    }

    [Button("Save Map")]
    public void saveMap()
    {
        string savename = "tilemap_g_" + Random.Range(0, 1000);
        var mf = GameObject.Find("Grid");

        if(mf)
        {
            var savepath = "Assets/" + savename + ".prefab";
            if(PrefabUtility.SaveAsPrefabAsset(mf, savepath))
            {
                EditorUtility.DisplayDialog("Tilemap Saved", "Tilemap Saved under: " + savepath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT Saved", "Tilemap NOT Saved under: " + savepath, "Continue");
            }
        }
    }

    public void clearMaps(bool complete)
    {
        topMap.ClearAllTiles();
        bottomMap.ClearAllTiles();
        if (complete)
        {
            terrainMap = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
