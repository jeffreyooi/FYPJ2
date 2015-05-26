using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	// Script to create a grid of tiles
	public GameObject thePathfinder;
	public GameObject thePathfinderRoot;
	public GameObject EnemySpawner;

	public GameObject node;
	public Tile tile;
	public int numberOfTilesColumn = 10;
	public int numberOfTilesRow = 9;
	public float tileSize = 1.0f;

	// Use this for initialization
	void Start () {
		if (WinGame.kills < 10) {
			CreateTiles ();
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void CreateTiles() {
		float xOffset = 0.0f;
		float yOffset = 0.0f;

		for (int y = 0; y < numberOfTilesRow;) {
			for (int x = 0; x < numberOfTilesColumn; ++x) {
				
				// Do offset after instantiating a tile
				xOffset += tileSize;
				
				// Reset x to the start and +1 to y to start next row
				if (x % numberOfTilesColumn == 0)
				{
					yOffset += tileSize;
					xOffset = 0;
					y++;

					//Create a node that serves as enemy spawn points
					GameObject newEnemySpawnNode = (GameObject)Instantiate (node, new Vector2(transform.position.x + tileSize*numberOfTilesColumn, transform.position.y + yOffset), Quaternion.identity);
					newEnemySpawnNode.name = "enemySpawn" + y.ToString();
					newEnemySpawnNode.transform.SetParent(thePathfinderRoot.transform);
					EnemySpawner.GetComponent<EnemySpawner>().spawnNodes[y-1] = newEnemySpawnNode;
				}
				
				Tile newTile = (Tile)Instantiate (tile, new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.identity);
				GameObject newNode = (GameObject)Instantiate (node, new Vector2(transform.position.x + xOffset, transform.position.y + yOffset), Quaternion.identity);
				//Assign the node to the tile
				newTile.PathNode = newNode;
				//Set the node as a child of the pathfinder root
				newNode.transform.SetParent(thePathfinderRoot.transform);
			}
		}

		//Scan and create links for the pathfinding graph
		thePathfinder.GetComponent<AstarPath>().Scan();
	}
}
