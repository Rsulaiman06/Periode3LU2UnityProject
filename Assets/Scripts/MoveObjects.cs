using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
public class MoveObjects : MonoBehaviour
{
    public List<GameObject> prefabs;
    public GameObject panel;
    public GameObject prefabContainer;

    public void CreateGameObjectFromClick(int prefabIndex)
    {
        var well = Instantiate(prefabs[prefabIndex], prefabContainer.transform);
        well.GetComponent<Objectsdragging>().moveObjects = this;
    }

    public void onmouseklick()
    {
        Debug.Log("Mouse Clicked");
    }

    //public GameObject[] GetPrefabWithTag()
    //{
    //    string[] prefabId = { "CloneArmor", "CloneRing", "CloneAxe", "CloneGoldCups" };
    //    GameObject[] prefabs = new GameObject[prefabId.Length];


    //    for (int i = 0; i < prefabs.Length; i++)
    //    {
    //        var prefab = GameObject.FindGameObjectsWithTag(prefabId[i]);
    //        if (prefab != null)
    //        {
    //            prefabs[i] = prefab[i];
    //        }

    //    }
    //    if (prefabs.Length > 0)
    //    {
    //        return prefabs;
    //    }
    //    return new GameObject[0];
    //}

    //public List<Object2D> ConvertToModel()
    //{
    //    GameObject[] prefabs = GetPrefabWithTag();
    //    List<Object2D> models = new List<Object2D>();

    //    foreach (var prefab in prefabs)
    //    {
    //        RectTransform rectTransform = prefab.GetComponent<RectTransform>();
    //        if (rectTransform != null)
    //        {
    //            string prefabId = prefab.tag;
    //            float positionX = rectTransform.anchoredPosition.x;
    //            float positionY = rectTransform.anchoredPosition.y;
    //            float scaleX = rectTransform.localScale.x;
    //            float scaleY = rectTransform.localScale.y;
    //            float rotationZ = rectTransform.localEulerAngles.z;
    //            SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
    //            int sortingLayer = spriteRenderer != null ? spriteRenderer.sortingLayerID : 0;

    //            models.Add(new Object2D
    //            {
    //                environmentId = environmentIdString,
    //                prefabId = prefabId,
    //                positionX = positionX,
    //                positionY = positionY,
    //                scaleX = scaleX,
    //                scaleY = scaleY,
    //                rotationZ = rotationZ,
    //                sortingLayer = sortingLayer
    //            });
    //        }
    //    }

    //    return models;
    //}
}
