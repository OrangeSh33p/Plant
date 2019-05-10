using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {
	void Start () {
		StartCoroutine (Grow());
	}

	IEnumerator Grow () {
		while (true) {
			Chunk randomChunk = Chunk.RandomChunk();
			if (randomChunk) Grow (randomChunk.RandomChild(), randomChunk);
			yield return new WaitForSeconds (0.5f);
		}
	}

	void Grow (Chunk what, Chunk where) {
		where.Grow(what);
	}
}
