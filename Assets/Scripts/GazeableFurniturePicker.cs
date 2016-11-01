using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using HoloToolkit.Unity;

public class GazeableFurniturePicker : MonoBehaviour {
    public Renderer rendererComponent;
    public GameObject FurnitureToSpawn;
    public AudioClip SelectSound;

    private string WorldAnchorId;
    private AudioSource source;

    [System.Serializable]
    public class PickedFurnitureCallback : UnityEvent<string>{ }

    public PickedFurnitureCallback OnGazedFurniture = new PickedFurnitureCallback();
    public PickedFurnitureCallback OnPickedFurniture = new PickedFurnitureCallback();

    private bool gazing = false;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

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
            WorldAnchorId = System.Guid.NewGuid().ToString();
            FurnitureToSpawn.GetComponent<TapToPlace>().SavedAnchorFriendlyName = "DeskAnchor-" + WorldAnchorId;
            var newFurniture = Instantiate(FurnitureToSpawn, newPos, new Quaternion(0,0,0,0));
            source.PlayOneShot(SelectSound);
        }
    }

    void Update()
    {
        if (!gazing) return;
    }
}
