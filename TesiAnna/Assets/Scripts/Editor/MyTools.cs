using UnityEngine;
using UnityEditor;

public static class MyTools
{
    [MenuItem("MyTools/Add to Report %F1")]
    static void DEV_AppendToReport()
    {
        CSVManager.AppendToReport(
            new string[5]
            {
                "jonny",
                Random.Range(0,11).ToString(),
                Random.Range(0,11).ToString(),
                Random.Range(0,11).ToString(),
                Random.Range(0,11).ToString()
            });
        EditorApplication.Beep();
        Debug.Log("<color=green>Report updated successfully!</color>");
    }

    [MenuItem("MyTools/Reset Report %F12")]
    static void DEV_ResetToReport()
    {
        CSVManager.CreateReport();

        EditorApplication.Beep();

        Debug.Log("<color=yellow>The report has been reset successfully!</color>");
    }
}
