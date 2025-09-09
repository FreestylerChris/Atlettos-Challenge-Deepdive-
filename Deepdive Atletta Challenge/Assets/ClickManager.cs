using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private ClickableObject lastSelected;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Linkermuisknop
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ClickableObject clicked = hit.collider.GetComponent<ClickableObject>();

                if (clicked != null)
                {
                    // Vorige deselecteren
                    if (lastSelected != null && lastSelected != clicked)
                    {
                        lastSelected.Deselect();
                    }

                    // Nieuwe selecteren
                    clicked.Select();
                    lastSelected = clicked;
                }
            }
        }
    }
}
