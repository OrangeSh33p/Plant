using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A bit of the plant (root, branch, leaf...)
public class Chunk : MonoBehaviour {
	public float childAngleRange;
	public enum PossibleChildren {BRANCH, ROOT, ALL}
	public PossibleChildren possibleChild;
	public Tip tip;
	public int amountOfGrowths; //The amount of times this chunk is allowed to grow a child
	public Chunk parent; //The chunk that spawned this chunk
	public int generation;

	//SHORTCUTS
	GameManager gm { get { return GameManager.Instance; } }
	
	//List of all the places where chunks can grow
	private static List<Chunk> _blossoms;
	public static List<Chunk> blossoms { 
		get { 
			if (_blossoms == null) _blossoms = new List<Chunk>(); 
			return _blossoms; 
		} 
	}


	void Awake () {
		for (int i=0;i<amountOfGrowths;i++)
			blossoms.Add(this);
	}

	void OnDestroy () {
		blossoms.RemoveAll(chunk => chunk==this);
	}

	public static Chunk RandomChunk () {
		return blossoms[Random.Range(0,blossoms.Count)];
	}

	public Chunk RandomChild () { //Returns a chunk prefab this chunks is allowed to grow
		if (possibleChild==PossibleChildren.ROOT) return gm.rootPrefabs[Random.Range(0,gm.rootPrefabs.Count)];
		else return gm.branchPrefabs[Random.Range(0,gm.branchPrefabs.Count)];
	}

	public void Grow (Chunk childPrefab) {
		Chunk child = Instantiate (
			childPrefab,
			tip.transform.position,
			tip.transform.rotation * Quaternion.Euler(0,0,Random.Range(-childAngleRange,childAngleRange)),
			transform
		).GetComponent<Chunk>();

		child.parent = this;
		child.generation = generation+1;

		blossoms.Remove(this);
	}
}
