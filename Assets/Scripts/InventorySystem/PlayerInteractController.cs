using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    private GameObject currentInteractable;
    private bool canInteract = false;

    [Header("Interaction")]
    [Tooltip("交互提示UI")]
    public GameObject interactionTips;

    void Start()
    {
        interactionTips.SetActive(false);
    }

    public void SetInteractObject(GameObject obj)
    {
        currentInteractable = obj;
        canInteract = true;
        interactionTips.SetActive(true);
    }


    public void ClearInteractObject()
    {
        currentInteractable = null;
        canInteract = false;
        interactionTips.SetActive(false);
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.GetComponent<InteractObjBase>().Interact();
        }
        
    }
}