using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class UniversalSyncer : AssetPostprocessor
{
    // 1. 저장 시 수정된 파일만 복사 (자동)
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string path in importedAssets)
        {
            if (path.EndsWith(".cs") && !path.Contains("UniversalSyncer"))
            {
                string content = File.ReadAllText(path);
                string fileName = Path.GetFileName(path);
                EditorGUIUtility.systemCopyBuffer = $"--- UPDATED FILE: {fileName} ---\n{content}";
                Debug.Log($"<color=yellow>[Gemini Sync]</color> {fileName} 내용이 복사되었습니다.");
            }
        }
    }

    // 2. 전체 프로젝트 코드 복사 (수동 메뉴)
    [MenuItem("Gemini/Export All Scripts")]
    public static void ExportAll()
    {
        string path = Application.dataPath + "/Scripts";
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"// Full Context: {System.DateTime.Now}");

        foreach (string file in Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories))
        {
            if (file.Contains("UniversalSyncer")) continue;
            sb.AppendLine($"\n--- FILE: {Path.GetFileName(file)} ---\n{File.ReadAllText(file)}");
        }
        EditorGUIUtility.systemCopyBuffer = sb.ToString();
        Debug.Log("<color=cyan>[Gemini Export]</color> 전체 코드가 클립보드에 복사되었습니다.");
    }
}