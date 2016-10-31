using UnityEngine;
using System.Collections;

public class FurnitureSpawner : MonoBehaviour {

    public GameObject Furniture;

    public void SetFurniture(GameObject Furniture)
    {
        this.Furniture = Furniture;
    }

	public void CreateNewFurniture()
    {
        if (Furniture != null)
        {
            var newFurniture = Instantiate(Furniture);
            newFurniture.transform.position = new Vector3(0, 0, 0);
        }
    }
}
