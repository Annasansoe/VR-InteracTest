using UnityEngine;
using System.IO;

public static class CSVManager
{
    private static string reportDirectoryName = "Report";
    private static string reportFileName = "Anna";
    private static string reportSeparator = ",";

    private static string[] reportHeaders = new string[] 
    {
        
    };

    private static string[] reportHeadersSceneOne = new string[]
    {
        "Scene number",
        "Interaction method",
        "Interaction metaphor",
        "User Number",
        "Collected objects",
        "Objects fell",
        "Start time",
        "End time",
        "Timestamp",
        "Object Name",
        "Interaction Type"    
    };

    private static string[] reportHeadersSceneTwo = new string[8]
    {
        "Scene number",
        "Interaction method",
        "User Number",
        "Wrong Answer",
        "Correct Answer",
        "Total canc",
        "Start time",
        "End time"
    };

    private static string[] reportHeadersSceneThree = new string[7] 
    {
        "Scene number",
        "Interaction method",
        "User Number",
        "Scaled Objects",
        "Objects fell",
        "Start time",
        "End time"
    };

    

    private static string timeStampHeader = "time stamp";
    
    #region InteractionsDefaults

    public static void AppendToReport(string[] strings)
    {
        reportFileName = strings[0];
        //VerifyDirectory();
        VerifyFile();
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }

                finalString += strings[i];
            }

            //finalString += reportSeparator + GetTimeStamp();
            sw.WriteLine(finalString);
        }

    }


    public static void CreateReport()
    {
        if (reportFileName == "SceneOne.csv")
        {
            reportHeaders = reportHeadersSceneOne;

        }
       else if (reportFileName == "SceneTwo.csv")
        {
            reportHeaders = reportHeadersSceneTwo;

        }
        else if (reportFileName == "SceneThree.csv")
        {
            reportHeaders = reportHeadersSceneThree;

        }
        // VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
            {
                string finalString = "";
                for (int i = 0; i < reportHeaders.Length; i++)
                {
                    if (finalString != "")
                    {
                        finalString += reportSeparator;
                    }

                    finalString += reportHeaders[i];
                }

               // finalString += reportSeparator + timeStampHeader;
                sw.WriteLine(finalString);
            }
        
    }

    #endregion

    

    #region Operations
   /* static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }*/

    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file))
        {
            CreateReport();
        }
    }

    #endregion

    #region Queries
    /*static string GetDirectoryPath()
    {
        return Application.persistentDataPath + "/" + reportDirectoryName;
    }*/

    static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, reportFileName); 
    }

    /*static string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }*/
    #endregion
}
