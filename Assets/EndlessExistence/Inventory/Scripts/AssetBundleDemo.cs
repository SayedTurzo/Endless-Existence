using System.IO;
using UnityEngine;

public class AssetBundleDemo : MonoBehaviour
{
    public string assetBundleName; // Set this in the Inspector

    private void Start()
    {
        LoadAssetBundle();
    }

    private void LoadAssetBundle()
    {
        string assetBundlePath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");

        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);

        if (assetBundle != null)
        {
            // Load and use assets from the Asset Bundle
            // ...

            assetBundle.Unload(false); // Unload the Asset Bundle when done
        }
        else
        {
            Debug.LogError("Failed to load Asset Bundle");
        }
    }
}
