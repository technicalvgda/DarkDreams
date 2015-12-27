using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public string EnemyPrefab;
	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject enemy = Instantiate(Resources.Load("Enemies/"+ EnemyPrefab) as GameObject);
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
	}
	
	
}
