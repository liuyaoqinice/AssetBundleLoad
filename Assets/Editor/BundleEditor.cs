using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class BundleEditor 
    {
        [MenuItem("Tools/打包")]
        private static void Build()
        {
            BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath,
                BuildAssetBundleOptions.ChunkBasedCompression, UnityEditor.EditorUserBuildSettings.activeBuildTarget);
            
            AssetDatabase.Refresh();
        }
    }
}
