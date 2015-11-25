using UnityEngine;
using System.Collections;
using System;
public class Archer : CharacterBase {
	public GameObject ArcherPrefab;

	// Use this for initialization
	void Start () {
		var cloneOfArcher = ((GameObject)Instantiate(ArcherPrefab, new Vector3(), Quaternion.Euler(new Vector3()))).GetComponent<Archer>();
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
