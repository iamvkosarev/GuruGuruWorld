using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap};
	public DrawMode drawMode;

	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int Seed = 50;
	public Vector2 offset;

	public bool autoUpdate;

	public TerrainData[] regions;

	public void GenerateMapOld() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, Seed, noiseScale, octaves, persistance, lacunarity, offset);

		Color[] colourMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions[i].height) {
						colourMap [y * mapWidth + x] = regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
		}
	}

	public Dictionary<TerrainType, List<int[]>> GenerateMap(out int width, out int hight)
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, Seed, noiseScale, octaves, persistance, lacunarity, offset);

		Dictionary<TerrainType, List<int[]>> result = new Dictionary<TerrainType, List<int[]>>();
		Dictionary<Color, TerrainType> colorDictionary = new Dictionary<Color, TerrainType>();
        foreach (var region in regions)
        {
			colorDictionary.Add(region.colour, region.TerrainType);
		}
		Color[] colourMap = new Color[mapWidth * mapHeight];
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						colourMap[y * mapWidth + x] = regions[i].colour;
						break;
					}
				}
				if(!result.ContainsKey(colorDictionary[colourMap[y * mapWidth + x]]))
                {

					result.Add(colorDictionary[colourMap[y * mapWidth + x]], new List<int[]>() { new int[2] { x - mapWidth/2, y- mapHeight /2} });
				}
                else
                {

					result[colorDictionary[colourMap[y * mapWidth + x]]].Add(new int[2] { x- mapWidth/2, y - mapHeight / 2 } );
				}
			}
		}
		width = mapWidth;
		hight = mapHeight;
		return result;
	}


	void OnValidate() {
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainData {
	public string name;
	public float height;
	public Color colour;
	public TerrainType TerrainType;
}

public enum TerrainType
{
	Wood,
	Bush,
	Grass,
	GrassPreRiver,
	Sand,
	River,
	PreRiver
}
