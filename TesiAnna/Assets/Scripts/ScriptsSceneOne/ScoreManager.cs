using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public void BackToMenu()
    {
        CSVManager.AppendToReport(GetReportLine());
        ScoreArea.indexText++;
        ScoreArea.totScore = 0;
        ScoreArea.totScoreEnd = 0;
        ObjectResetPlaneForSceneOne.objectFell = 0;
        ScoreArea.interactionDataListStart.Clear();
        ScoreArea.interactionDataList.Clear();
    }

    public static string[] GetReportLine()
    {

        int i = 0;
        int j = 0;
        string[] returnable = new string[60];
        returnable[0] = "SceneOne.csv";
        returnable[1] = "Controllers";
        returnable[2] = "Raycasting";
        returnable[3] = ScoreArea.indexText.ToString();
        returnable[4] = ScoreArea.totScoreEnd.ToString();
        returnable[5] = ObjectResetPlaneForSceneOne.objectFell.ToString();
        returnable[6] = ScoreArea.dateTimeStart.ToString();
        returnable[7] = ScoreArea.dateTimeEnd.ToString();

        foreach (ScoreArea.InteractionData interaction in ScoreArea.interactionDataListStart)
        {
            i++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[7 + i] = interactionLine;
        }
        foreach (ScoreArea.InteractionData interaction in ScoreArea.interactionDataList)
        {
            j++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[7 + i + j] = interactionLine;
        }

        return returnable;
    }
}
