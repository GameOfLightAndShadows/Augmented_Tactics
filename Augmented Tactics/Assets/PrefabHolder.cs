using UnityEngine;
using System.Collections;

public class PrefabHolder : MonoBehaviour {
    public static PrefabHolder instance;
    public GameObject BASE_TILE_PREFAB;

    public void Awake()
    {
        instance = this;
    }
}
