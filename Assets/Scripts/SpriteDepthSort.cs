using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDepthSort : MonoBehaviour
{
    private SpriteRenderer sr;

    public Transform feetPoint;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        float y = feetPoint != null ? feetPoint.position.y : transform.position.y;
        sr.sortingOrder = Mathf.RoundToInt(-y * 100);
    }
}
