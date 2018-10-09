using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

	public static GenerationManager instance;

	public GameObject floorPrefab, PlayerPrefab;
	public GameObject PlayerInstance;

	public Interactbles Star, Enemy;

	public float distanceToNextFloor, distanceRequiredToCreateNextFloor;

	private GameObject[] AllFloors = new GameObject[2];
	private GameObject[] AllInteractables = new GameObject[2];

	private void Awake ()
	{
		instance = this;
	}

	private void Start () 
	{
		NewGame (false);
	}

	private void Update ()
	{
		if (PlayerInstance != null) 
		{
			if (Mathf.Abs(PlayerInstance.transform.position.x) - Mathf.Abs(AllFloors[1].transform.position.x) >= distanceRequiredToCreateNextFloor) 
			{
				CreateNewFloor ();
			}	
		}
			
	}

	private void CreateNewFloor () 
	{
		Transform[] childObjects = AllInteractables [0].GetComponentsInChildren<Transform> ();

		foreach (Transform child in childObjects) 
		{
			GameObject.Destroy (child.gameObject);
		}

		AllInteractables [0] = new GameObject ("Interactables Organizer 0");

		childObjects = AllInteractables [1].GetComponentsInChildren<Transform> ();

		foreach (Transform child in childObjects) 
		{
			child.SetParent (AllInteractables [0].transform);
		}

		AllInteractables [1] = new GameObject ("Interactables Organizer 1");

		GameObject.Destroy (AllFloors [0]);

		AllFloors [0] = AllFloors [1];

		AllFloors[1] = GameObject.Instantiate(floorPrefab, new Vector3 (AllFloors[0].transform.position.x + distanceToNextFloor, 0, 0), Quaternion.Euler(0, 0, 0));	

		GenerateInteractablesForFloor (AllFloors [1], Star, 1);
		GenerateInteractablesForFloor (AllFloors [1], Enemy, 1);
	}

	private void GenerateInteractablesForFloor (GameObject Floor, Interactbles Interactable, int interactablesIndex)
	{
		float newIntPosition = Floor.transform.position.x - Interactable.firstIntDistanceToFloor;

		for (int i = 0; i < Interactable.intsPerFloor; i++) 
		{
			GameObject aux = GameObject.Instantiate (Interactable.prefab, new Vector3 (newIntPosition, 0.5f, Random.Range(Interactable.intMinZ, Interactable.intMaxZ)), Quaternion.Euler (0, 0, 0));

			aux.transform.SetParent (AllInteractables [interactablesIndex].transform);

			newIntPosition += Interactable.distanceBetweenInts;
		}
	}		

	public void NewGame (bool Reset) 
	{
		if (Reset) 
		{	
			Transform[] childObjects = AllInteractables [0].GetComponentsInChildren<Transform> ();

			foreach (Transform child in childObjects) 
			{
				GameObject.Destroy (child.gameObject);
			}

			childObjects = AllInteractables [1].GetComponentsInChildren<Transform> ();

			foreach (Transform child in childObjects) 
			{
				GameObject.Destroy (child.gameObject);
			}

			GameObject.Destroy (AllFloors [0]);
			GameObject.Destroy (AllFloors [1]);	
		}

		AllInteractables [0] = new GameObject ("Interactables Organizer 0");
		AllInteractables [1] = new GameObject ("Interactables Organizer 1");

		PlayerInstance = GameObject.Instantiate (PlayerPrefab);

		Camera.main.GetComponent<CameraFollowPlayer> ().Player = PlayerInstance;

		AllFloors[0] = GameObject.Instantiate(floorPrefab, new Vector3 (PlayerInstance.transform.position.x + distanceRequiredToCreateNextFloor, 0, 0), Quaternion.Euler(0, 0, 0));
		AllFloors[1] = GameObject.Instantiate(floorPrefab, new Vector3 (AllFloors[0].transform.position.x + distanceToNextFloor, 0, 0), Quaternion.Euler(0, 0, 0));		

		GenerateInteractablesForFloor (AllFloors [0], Star, 0);
		GenerateInteractablesForFloor (AllFloors [1], Star, 1);
		GenerateInteractablesForFloor (AllFloors [0], Enemy, 0);
		GenerateInteractablesForFloor (AllFloors [1], Enemy, 1);

	}
}

[System.Serializable]
public struct Interactbles {

	public GameObject prefab;

	public float intMinZ, intMaxZ, distanceBetweenInts, firstIntDistanceToFloor, intsPerFloor;

}