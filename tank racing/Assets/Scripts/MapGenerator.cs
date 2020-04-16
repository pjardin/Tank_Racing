using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject roadCorner;
	public GameObject roadStraight;
	//public LayerMask map;

	public Vector2Int mapSize;
	private int offset;

	void printMaze(string[,] maze)
	{

		string mazeSTR = "";
		for (int i = 0; i < mapSize.y; i++)
		{
			string line = "";
			for (int j = 0; j < mapSize.x; j++)
			{
				line += maze[j, i] ;
			}
			mazeSTR += line + "\n";
		}

		Debug.Log(mazeSTR);


	}

	void mazeGen()
	{

        //https://en.wikipedia.org/wiki/Maze_generation_algorithm#Randomized_Prim.27s_algorithm

		Random rnd = new Random();

		string[,] maze = new string[mapSize.x, mapSize.y];


        //first initilize the maze
		for (int i = 0; i < mapSize.x; i++)
		{
			string line = "";
			for (int j = 0; j < mapSize.y; j++)
			{
				maze[i, j] = "R";
			}

		}


        //now split it
		int col_start = 0;
		int col_end = mapSize.x;

		int col = Random.Range(col_start + 1, col_end - 1);

		int row_start = 0;
		int row_end = mapSize.y;

		int row = Random.Range(row_start + 1, row_end - 1);


		for (int i = 0; i < mapSize.y; i++)
		{
			maze [col, i] = "B";
		}

		for (int i = 0; i < mapSize.x; i++)
		{
			maze[i, row] = "B";
		}




		//three sides need to be deleted
		int skip = Random.Range(0, 4);// which side NOT to be delteted


		for (int i = 0; i < 4; i++)
		{
            if (skip == i)
			{
				continue;
			}
			int delB = 0;

			switch (i)
			{
				case 0:
					delB = Random.Range(col_start, col);
					maze[delB, row] = "R";
					break;
				case 1:
					delB = Random.Range(col + 1, col_end);
					maze[delB, row] = "R";
					break;
				case 2:
					delB = Random.Range(row_start, row);
					maze[col, delB] = "R";
                    break;
				default:
					delB = Random.Range(row + 1, row_end);
					maze[col, delB] = "R";
					// code block
					break;
			}

		}

		


		printMaze(maze);
		mazeTo3D(maze);
		Debug.Log("Generated Map");

	}

    void mazeTo3D(string[,] maze)
	{

		for (int i = 0; i < mapSize.y; i++)
		{
			for (int j = 0; j < mapSize.x; j++)
			{
				if (maze[j, i] == "R")
				{

					int count = 0;

					bool N = false;
					bool E = false;
					bool S = false;
					bool w = false;

					if (i + 1 < mapSize.y && maze[j, i + 1] == "R")
					{

						N = true;
						count++;
					}
                    if (i - 1 >= 0 && maze[j, i - 1] == "R")
					{

						S = true;
						count++;
					}

					if (j + 1 < mapSize.x && maze[j + 1, i] == "R")
					{

						E = true;
						count++;
					}

					if (j - 1 >= 0 && maze[j - 1, i] == "R")
					{
						E = true;
						count++;
					}

					Debug.Log(count);




				}



			}
		}


	}

	void Start() {
		mazeGen();
		offset = 30;
		//roadCorner.layer = 9; //map; //add "ground" tag to objects
		//roadStraight.layer = 9; //map; //add "ground" tag to objects
		GenerateMap();
	}

    void roadStraightX(Vector3 where)
	{
	
		for (int i = 0; i < 6; i++)
		{
			Instantiate(roadStraight, new Vector3(where.x * offset + i * 5 -10, where.y * offset, where.z * offset), Quaternion.identity);
		}
	}

	void roadStraightZ(Vector3 where)
	{
		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 270, 0);

		for (int i = 0; i < 6; i++)
		{
			Instantiate(roadStraight, new Vector3(where.x * offset, where.y * offset, where.z * offset + i * 5 -10), rot90);
		}
	}

	void corner(Vector3 where)
	{
		Quaternion rot = Quaternion.identity;

		Instantiate(roadCorner, new Vector3(where.x * offset -15 , where.y * offset, where.z * offset ), rot);

	}

	void corner270(Vector3 where)
	{
		Quaternion rot270 = Quaternion.identity;
		rot270.eulerAngles = new Vector3(0, 270, 0);

		Instantiate(roadCorner, new Vector3(where.x * offset , where.y * offset, where.z * offset - 15), rot270);

	}

	void corner90(Vector3 where)
	{
		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 90, 0);

		Instantiate(roadCorner, new Vector3(where.x * offset, where.y * offset, where.z * offset  + 15), rot90);

	}

	void corner180(Vector3 where)
	{
		Quaternion rot180 = Quaternion.identity;
		rot180.eulerAngles = new Vector3(0, 180, 0);

		Instantiate(roadCorner, new Vector3(where.x * offset + 15, where.y * offset, where.z * offset), rot180);

	}

	public void GenerateMap()
	{
		Debug.Log("Generated Map");

		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 90, 0);

		Quaternion rot180 = Quaternion.identity;
		rot180.eulerAngles = new Vector3(0, 180, 0);


		/*
		corner180(new Vector3(0, 0, 0));
		corner90(new Vector3(1, 0, 0));
		corner(new Vector3(1, 0, 1));
		corner270(new Vector3(0, 0, 1));
        */

		corner180(new Vector3(0, 0, 0));

		roadStraightX(new Vector3(1, 0, 0));
		roadStraightX(new Vector3(2, 0, 0));

		corner90(new Vector3(3, 0, 0));

		roadStraightZ(new Vector3(0, 0, 2));
		roadStraightZ(new Vector3(0, 0, 1));

		corner(new Vector3(3, 0, 3));

		roadStraightX(new Vector3(1, 0, 3));
		roadStraightX(new Vector3(2, 0, 3));

		corner270(new Vector3(0, 0, 3));

		roadStraightZ(new Vector3(3, 0, 2));
		roadStraightZ(new Vector3(3, 0, 1));
        



	}

}
