using UnityEngine;

public class MouseDetection : MonoBehaviour
{

    public int Switch;
        void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);

                // Example: highlight clicked object
                Renderer rend = hit.collider.GetComponent<Renderer>();
                if (rend != null)
                {
                    rend.material.color = Color.green;
                }
            }
        }
    }
}
