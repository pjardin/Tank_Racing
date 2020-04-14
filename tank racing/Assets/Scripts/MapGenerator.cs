using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject roadCorner;
	public GameObject roadStraight;

	public Vector2 mapSize;
	private int offset;

	void Start() {
		offset = 30;

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
