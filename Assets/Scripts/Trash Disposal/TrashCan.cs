using UnityEngine;

public class TrashCan : MonoBehaviour
{
    // Called when a trash object enters the trash can
    public void InsideTrash(GameObject trash)
    {
        // Destroy the trash object
        Destroy(trash);
    }
}
