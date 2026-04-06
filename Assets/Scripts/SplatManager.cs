using UnityEngine;

public class SplatManager : MonoBehaviour
{
    [Header("Assets")]
    public RenderTexture playerRT;
    public Material blotMat;
    public Texture2D blotSprite;

    [Header("Settings")]
    public int totalBlots = 6;

    void Start()
    {
        // 1. Initialize the canvas so it's transparent/empty at the start
        if (playerRT != null)
        {
            Graphics.SetRenderTarget(playerRT);
            GL.Clear(true, true, Color.clear);
            Graphics.SetRenderTarget(null); // Reset target to the screen
        }
    }
    public void AddSplat(Vector2 location)
    {
        if (playerRT == null || blotMat == null)
        {
            Debug.LogError("SplatHandler: Missing Render Texture or Material references!");
            return;
        }

        // 2. Pick a random blot index (0 to 5)
        int randomIndex = Random.Range(0, totalBlots);

        // 3. Update the Shader Properties
        // Ensure these strings match the 'Reference' names in your Shader Graph Blackboard
        blotMat.SetTexture("_MainTex", playerRT);
        blotMat.SetTexture("_SplatTex", blotSprite);
        blotMat.SetVector("_SplatPos", location);
        blotMat.SetFloat("_CellIndex", (float)randomIndex);

        // 4. The Blit Process (The "Stamp")
        // We need a temporary texture because we can't read and write to the same texture at once
        RenderTexture temp = RenderTexture.GetTemporary(playerRT.width, playerRT.height, 0, playerRT.format);

        // This runs the Painter Shader and saves the result to 'temp'
        Graphics.Blit(playerRT, temp, blotMat);

        // This copies the 'temp' result back into our permanent 'playerRT'
        Graphics.Blit(temp, playerRT);

        // 5. Cleanup
        RenderTexture.ReleaseTemporary(temp);

        // Optional: Force the texture to update if using URP
        playerRT.MarkRestoreExpected();
    }

    [ContextMenu("Test Random Splat")]
    public void TestSplat()
    {
        AddSplat(new Vector2(Random.value, Random.value));
    }
}
