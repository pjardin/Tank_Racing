using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject roadCorner;
	public GameObject roadStraight;
	public GameObject roadT;
	public GameObject roadEND;
	public GameObject roadFour;

	//public LayerMask map;

	public Vector2Int mapSize;
	private int offset;


	void roadStraightX(Vector3 where)
	{


		Instantiate(roadStraight, new Vector3(where.x * offset, where.y * offset, where.z * offset), Quaternion.identity);

	}

	void roadStraightZ(Vector3 where)
	{
		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 270, 0);


		Instantiate(roadStraight, new Vector3(where.x * offset, where.y * offset, where.z * offset), rot90);

	}

	void corner(Vector3 where)
	{
		Quaternion rot = Quaternion.identity;

		Instantiate(roadCorner, new Vector3(where.x * offset + 10, where.y * offset, where.z * offset + 10), rot);

	}

	void corner270(Vector3 where)
	{
		Quaternion rot270 = Quaternion.identity;
		rot270.eulerAngles = new Vector3(0, 270, 0);

		Instantiate(roadCorner, new Vector3(where.x * offset - 10, where.y * offset, where.z * offset + 10), rot270);

	}

	void corner90(Vector3 where)
	{
		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 90, 0);

		Instantiate(roadCorner, new Vector3(where.x * offset + 10, where.y * offset, where.z * offset - 10), rot90);

	}

	void corner180(Vector3 where)
	{
		Quaternion rot180 = Quaternion.identity;
		rot180.eulerAngles = new Vector3(0, 180, 0);

		Instantiate(roadCorner, new Vector3(where.x * offset - 10, where.y * offset, where.z * offset - 10), rot180);

	}

	void t(Vector3 where)
	{
		Quaternion rot = Quaternion.identity;

		Instantiate(roadT, new Vector3(where.x * offset, where.y * offset, where.z * offset + 10), rot);

	}

	void t270(Vector3 where)
	{
		Quaternion rot270 = Quaternion.identity;
		rot270.eulerAngles = new Vector3(0, 270, 0);

		Instantiate(roadT, new Vector3(where.x * offset - 10, where.y * offset, where.z * offset), rot270);

	}

	void t90(Vector3 where)
	{
		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 90, 0);

		Instantiate(roadT, new Vector3(where.x * offset + 10, where.y * offset, where.z * offset), rot90);

	}

	void t180(Vector3 where)
	{
		Quaternion rot180 = Quaternion.identity;
		rot180.eulerAngles = new Vector3(0, 180, 0);

		Instantiate(roadT, new Vector3(where.x * offset, where.y * offset, where.z * offset - 10), rot180);

	}

	void end(Vector3 where)
	{
		Quaternion rot = Quaternion.identity;

		Instantiate(roadEND, new Vector3(where.x * offset, where.y * offset, where.z * offset + 5), rot);

	}

	void end270(Vector3 where)
	{
		Quaternion rot270 = Quaternion.identity;
		rot270.eulerAngles = new Vector3(0, 270, 0);

		Instantiate(roadEND, new Vector3(where.x * offset - 5, where.y * offset, where.z * offset), rot270);

	}

	void end90(Vector3 where)
	{
		Quaternion rot90 = Quaternion.identity;
		rot90.eulerAngles = new Vector3(0, 90, 0);

		Instantiate(roadEND, new Vector3(where.x * offset + 5, where.y * offset, where.z * offset), rot90);

	}

	void end180(Vector3 where)
	{
		Quaternion rot180 = Quaternion.identity;
		rot180.eulerAngles = new Vector3(0, 180, 0);

		Instantiate(roadEND, new Vector3(where.x * offset, where.y * offset, where.z * offset - 5), rot180);

	}


	void fourWay(Vector3 where)
	{


		Instantiate(roadFour, new Vector3(where.x * offset, where.y * offset, where.z * offset), Quaternion.identity);

	}

	void printMaze(string[,] maze)
	{

		string mazeSTR = "";
		for (int i = mapSize.y - 1; i >= 0; i--)
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
					bool W = false;

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
						W = true;
						count++;
					}


                    if (count == 1)
					{
						if (N == true)
						{
							end180(new Vector3(j, 0, i));
						}
						else if (E == true)
						{
							end270(new Vector3(j, 0, i));
						}
						else if (S == true)
						{
							end(new Vector3(j, 0, i));
						}
						else
						{
							end90(new Vector3(j, 0, i));
						}
					}
					else if (count == 2)
					{
						if (N == true && S == true)
						{
							roadStraightZ(new Vector3(j, 0, i));
						}
						else if (E == true && W == true)
						{
							roadStraightX(new Vector3(j, 0, i));
						}
						else
						{

							if (W == true && S == true)
							{
								corner180(new Vector3(j, 0, i));
							}
							else if (E == true && S == true)
							{
								corner90(new Vector3(j, 0, i));
							}
							else if (N == true && E == true)
							{
								corner(new Vector3(j, 0, i));
							}
							else
							{
								corner270(new Vector3(j, 0, i));
							}
						}


					}
                    else if (count == 3)
					{
						if (W == true && E == true && S == true)
						{
							t(new Vector3(j, 0, i));
						}
						else if (N == true && E == true && S == true)
						{
							t270(new Vector3(j, 0, i));
						}
						else if (N == true && W == true && E == true)
						{
							t180(new Vector3(j, 0, i));
						}
						else
						{
							t90(new Vector3(j, 0, i));
						}
					} else
					{
						fourWay(new Vector3(j, 0, i));
					}


				}

				Debug.Log(j);
				Debug.Log(i);


			}
		}


	}

	void Start() {
		offset = 30;
		//roadCorner.layer = 9; //map; //add "ground" tag to objects
		//roadStraight.layer = 9; //map; //add "ground" tag to objects
		GenerateMap();
	}


	public void GenerateMap()
	{
		mazeGen();

		Debug.Log("Generated Map");

		/*
		corner180(new Vector3(0, 0, 0));
		corner90(new Vector3(1, 0, 0));
		corner(new Vector3(1, 0, 1));
		corner270(new Vector3(0, 0, 1));
        */
		/*
		corner(new Vector3(0, 0, 0));

		t(new Vector3(1, 0, 0));
		roadStraightX(new Vector3(2, 0, 0));

		corner270(new Vector3(3, 0, 0));

		t270(new Vector3(0, 0, 2));

		corner180(new Vector3(3, 0, 3));

		t180(new Vector3(1, 0, 3));
		roadStraightX(new Vector3(2, 0, 3));

		corner90(new Vector3(0, 0, 3));

		t90(new Vector3(3, 0, 2));
		roadStraightZ(new Vector3(3, 0, 1));


		end90(new Vector3(1, 0, 2));
		end270(new Vector3(2, 0, 2));

		end(new Vector3(1, 0, 4));
		end180(new Vector3(1, 0, -1));

		fourWay(new Vector3(0, 0, 1));
        */
		/*
		corner(new Vector3(0, 0, 0));

		corner90(new Vector3(0, 0, 1));

		corner180(new Vector3(1, 0, 1));

		corner270(new Vector3(1, 0, 0));
        */



	}

}
