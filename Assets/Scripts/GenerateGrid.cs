using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour {

	public GameObject Node;
	public Vector2 mapSize; // nodes
	public float margin;

	float nodeSizeX;
	float nodeSizeY;

	float newPosX = 0;
	float newPosY = 0;

	void Start(){
		CreateGrid ();
	}


	void CreateGrid(){
		nodeSizeX = Node.transform.localScale.x;
		nodeSizeY = Node.transform.localScale.y;
	

		for (int i = 0; i < mapSize.y; i++) {
			for (int j = 0;	j < mapSize.x; j++) {
				newPosX += margin + nodeSizeX/2;

				Vector2 newPos = new Vector2 (newPosX, newPosY);

				GameObject go = (GameObject)GameObject.Instantiate (Node, newPos + (Vector2)transform.position , Quaternion.identity);

				go.transform.SetParent (this.transform);

				newPosX += nodeSizeX/2;
			}
			newPosX = 0;
			newPosY += margin + nodeSizeY;
		}
		transform.position = transform.position + Vector3.forward * 10f;

	}
}
