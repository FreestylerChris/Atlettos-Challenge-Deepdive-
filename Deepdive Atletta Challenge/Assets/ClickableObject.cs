using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;
    public int Switch;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Select()
    {
        rend.material.color = Color.green; // geselecteerde kleur
        Switch = 1;
    }

    public void Deselect()
    {
        rend.material.color = originalColor; // terug naar normaal
        Switch = 0;
    }
}
