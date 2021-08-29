using System.Collections.Generic;
using UnityEngine;

public class LoadMap : MonoBehaviour
{
    [SerializeField] LoadData loadData;
    [SerializeField] GameObject mapItem;

    private GameObject map;
    private Transform transformMap;
    private SpriteRenderer spriteRender;
    private List<GameObject> gameObjectsMap;

    private void Awake()
    {
        loadData.DataIsLoad += CreatMap;
        gameObjectsMap = new List<GameObject>();
    }

    private void CreatMap()
    {
        map = new GameObject("Map");
        transformMap = map.GetComponent<Transform>();
        for (int i = 0; i < loadData.map.List.Length; i++)
        {
            Vector2 position = new Vector2(loadData.map.List[i].X, loadData.map.List[i].Y);
            mapItem = Instantiate(mapItem, position, Quaternion.identity) as GameObject;
            mapItem.GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("ResourcesTestingTaskJunior/" + loadData.map.List[i].Id, typeof(Sprite));
            mapItem.name = loadData.map.List[i].Id;
            mapItem.transform.SetParent(transformMap);
            gameObjectsMap.Add(mapItem);
            if (loadData.map.List[i].Width != 5.12f)
            {
                FixPositionSprite(i);
            }
        }

    }

    private void FixPositionSprite(int i)
    {
        Bounds bounds = gameObjectsMap[i].GetComponent<Renderer>().bounds;
        float extents = bounds.extents.x;
        bounds = gameObjectsMap[i - 1].GetComponent<Renderer>().bounds;
        float extentsNormal = bounds.extents.x;
        float distance = extentsNormal - extents;
        float normalPosition = loadData.map.List[i].X - distance;
        gameObjectsMap[i].transform.position = new Vector2(normalPosition, loadData.map.List[i].Y);

    }
}
