using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using HoloToolkit.Unity;

public class GazeableFurniturePicker : MonoBehaviour {
    public Renderer rendererComponent;
    public GameObject FurnitureToSpawn;

    private int WorldAnchorId = 0;

    [System.Serializable]
    public class PickedFurnitureCallback : UnityEvent<string>{ }

    public PickedFurnitureCallback OnGazedFurniture = new PickedFurnitureCallback();
    public PickedFurnitureCallback OnPickedFurniture = new PickedFurnitureCallback();

    private bool gazing = false;

    void OnGazeEnter()
    {
        gazing = true;
        Debug.Log(gazing);
    }

    void OnGazeLeave()
    {
        gazing = false;
        Debug.Log(gazing);
    }

    void OnSelect()
    {
        if (FurnitureToSpawn != null)
        {
            Debug.Log("Spawning...");
            var newPos = Camera.main.transform.forward;
            newPos.y -= 0.8f;
            WorldAnchorId++;
            FurnitureToSpawn.GetComponent<TapToPlace>().SavedAnchorFriendlyName = "DeskAnchor " + WorldAnchorId;
            var newFurniture = Instantiate(FurnitureToSpawn, newPos, new Quaternion(0,0,0,0));

        }
    }

    void Update()
    {
        if (!gazing) return;
    }
}
