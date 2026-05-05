using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;

public class BuildScript
{
    public static void BuildWindows()
    {
        string path = "build/StandaloneWindows64/TFTGame.exe";

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = GetScenes(),
            locationPathName = path,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.None,
        };

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result != BuildResult.Succeeded)
        {
            throw new System.Exception("Build failed");
        }
    }

    private static string[] GetScenes()
    {
        int sceneCount = EditorBuildSettings.scenes.Length;
        string[] scenes = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        return scenes;
    }
}
