using UnityEngine;

public class ArrowColor : MonoBehaviour
{
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetArrowColor ( Color newColor )
    {
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        sr.GetPropertyBlock( propBlock );

        propBlock.SetColor("_PlayerColor", newColor);

        sr.SetPropertyBlock( propBlock );
    }
}
