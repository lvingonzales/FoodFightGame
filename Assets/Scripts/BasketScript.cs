using UnityEngine;

public class BasketScript : MonoBehaviour
{
    public Color basketColor;
    ArrowColor arrowColor;

    private void Awake()
    {
        arrowColor = GetComponent<ArrowColor>();
    }

    private void Start()
    {
        arrowColor.SetArrowColor(basketColor);
    }
}
