using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public void Start()
    {
        GetComponent<TriggerZone>().OnEnterEvent.AddListener(InsideTrash);
    }

    // Called when a trash object enters the trash can
    public void InsideTrash(GameObject trash)
    {
        // Destroy the trash object
        Destroy(trash);
    }
}
