using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using HoloToolkit.Unity;

public class GazeableFurniturePicker : MonoBehaviour {
    public Renderer rendererComponent;
    public GameObject FurnitureToSpawn;
    public AudioClip SelectSound;
    public AudioClip HoverSound;

    private string WorldAnchorId;
    private AudioSource source;

    [System.Serializable]
    public class PickedFurnitureCallback : UnityEvent<string>{ }

    public PickedFurnitureCallback OnGazedFurniture = new PickedFurnitureCallback();
    public PickedFurnitureCallback OnPickedFurniture = new PickedFurnitureCallback();

    private bool gazing = false;
    private GameObject justSpawnedFurniture;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
    }

    void OnGazeEnter()
    {
        gazing = true;
        source.PlayOneShot(HoverSound);
    }

    void OnGazeLeave()
    {
        gazing = false;
    }

    void OnSelect()
    {
        if (FurnitureToSpawn != null)
        {
            source.PlayOneShot(SelectSound);
            var newPos = Camera.main.transform.forward;
            newPos.y -= 0.8f;
            WorldAnchorId = System.Guid.NewGuid().ToString();
            justSpawnedFurniture = GameObject.Instantiate(FurnitureToSpawn, newPos, new Quaternion(0,0,0,0)) as GameObject;
            justSpawnedFurniture.GetComponent<TapToPlace>().SavedAnchorFriendlyName = "DeskAnchor-" + WorldAnchorId;
            justSpawnedFurniture.SetActive(false);
            Invoke("LateSelect", 1f);
        }
    }

    void LateSelect()
    {
        justSpawnedFurniture.SetActive(true);
        justSpawnedFurniture.SendMessage("OnSelect");
    }

    void Update()
    {
        if (!gazing) return;
    }
}
