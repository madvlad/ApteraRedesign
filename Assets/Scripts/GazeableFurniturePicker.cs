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
            var newPos = Camera.main.transform.forward * 6f;
            WorldAnchorId = System.Guid.NewGuid().ToString();
            justSpawnedFurniture = GameObject.Instantiate(FurnitureToSpawn, newPos, new Quaternion(0,0,0,0)) as GameObject;
            justSpawnedFurniture.GetComponent<TapToPlace>().SavedAnchorFriendlyName = "DeskAnchor-" + WorldAnchorId;
            justSpawnedFurniture.SetActive(false);
            Invoke("LateSelect", 1f);
        }
    }

    void LateSelect()
    {
        var spatialMappingManager = SpatialMappingManager.Instance;
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
            30.0f, spatialMappingManager.LayerMask))
        {
            justSpawnedFurniture.transform.position = hitInfo.point;
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            justSpawnedFurniture.transform.rotation = toQuat;
        }

        justSpawnedFurniture.SetActive(true);
        justSpawnedFurniture.SendMessage("OnSelect");
    }

    void Update()
    {
        if (!gazing) return;
    }
}
