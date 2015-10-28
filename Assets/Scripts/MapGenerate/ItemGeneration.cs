using UnityEngine;
using System.Collections;

public class ItemGeneration : MonoBehaviour {

	public Transform[] itemLevel1;
	public Transform[] itemLevel2;
	public Transform[] itemLevel3;
	private float l,h;

	void Awake() {
		Transform gameObject= GameObject.Find("MapManager").transform;
		l = gameObject.GetComponent<RandomMapGeneration>().lengthOfRoom;
		h = gameObject.GetComponent<RandomMapGeneration>().heightOfRoom;
		Debug.Log (l + " " + h);

	}
	// Use this for initialization
	void Start () {
		Vector3 roomPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		roomPosition.x = Random.Range (0, (l/2));
		roomPosition.y = -(h/3);

		Instantiate (itemLevel1[2], roomPosition, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
