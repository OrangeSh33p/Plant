using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
	public List<Chunk> rootPrefabs;
	public List<Chunk> branchPrefabs;
}
