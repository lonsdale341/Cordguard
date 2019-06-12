using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabListJT : MonoBehaviour {
    // Lookup dictionaries, for quickly finding the prefab, given a name.

    // List of all the prefabs that contain menu screens for UI.  Populated
    // via the Unity inspector.  Similar to the prefab list, these get
    // processed into a dictionary at runtime to make lookups easier.
    public MenuEntryJT[] menuScreens;
    public AssetEntryJT[] AssetsArrange;
    [HideInInspector]
    public Dictionary<string, GameObject> menuLookup;
    [HideInInspector]
    public Dictionary<string, GameObject> assetLookup;

    void Awake()
    {
        menuLookup = new Dictionary<string, GameObject>();
        foreach (MenuEntryJT entry in menuScreens)
        {
            menuLookup[entry.name] = entry.prefab;
        }
        assetLookup = new Dictionary<string, GameObject>();
        foreach (AssetEntryJT entry in AssetsArrange)
        {
            assetLookup[entry.name] = entry.prefab;
        }
       
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public struct MenuEntryJT
{
    public MenuEntryJT(string name, GameObject prefab)
    {
        this.name = name;
        this.prefab = prefab;
    }
    public string name;
    public GameObject prefab;
}
[System.Serializable]
public struct AssetEntryJT
{
    public AssetEntryJT(string name, GameObject prefab)
    {
        this.name = name;
        this.prefab = prefab;
    }
    public string name;
    public GameObject prefab;
}
