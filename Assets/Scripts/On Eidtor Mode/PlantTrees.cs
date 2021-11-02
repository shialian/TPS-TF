using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlantTrees : MonoBehaviour
{
    public bool plantTrees = false;
    public Transform[] plantBlocks;
    public Transform parent;
    public Transform[] prefabs;

    private void Update()
    {
        if(plantTrees == false)
        {
            for(int i = 0; i < plantBlocks.Length; i++)
            {
                int index = Random.Range(0, prefabs.Length);
                float xzOffset = Random.Range(-0.3f, 0.3f);
                float randomScale = Random.Range(0.5f, 1.5f);
                Vector3 scale = new Vector3(randomScale, randomScale, randomScale);
                Vector3 offset = new Vector3(xzOffset, 2, xzOffset);
                if (index == 1 || index == 3)
                {
                    offset.y = 3;
                }
                Transform inst = Instantiate(prefabs[index], parent);
                inst.position = plantBlocks[i].position + offset;
                //inst.localScale = scale;
            }
            plantTrees = true;
        }
    }
}
