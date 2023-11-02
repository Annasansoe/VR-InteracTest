using UnityEngine;
using System;

public class ScoreManagerDG : MonoBehaviour
{
    public void BackToMenuDG()
    {
        ScoreAreaDG.totScoreEnd = ScoreAreaDG.totScore;
        ScoreAreaDG.dateTimeEnd = DateTime.Now;
        CSVManager.AppendToReport(GetReportLine());
        ScoreAreaDG.indexText++;
        ScoreAreaDG.totScore = 0;
        ObjectResetPlaneForSceneOne.objectFell = 0;
    }

    public static string[] GetReportLine()
    {

        int i = 0;
        int j = 0;
        string[] returnable = new string[60];
        returnable[0] = "SceneOne.csv";
        returnable[1] = "Controllers";
        returnable[2] = "Direct ";
        returnable[3] = ScoreAreaDG.indexText.ToString();
        returnable[4] = ScoreAreaDG.totScoreEnd.ToString();
        returnable[5] = ObjectResetPlaneForSceneOne.objectFell.ToString();
        returnable[6] = ScoreAreaDG.dateTimeStart.ToString();
        returnable[7] = ScoreAreaDG.dateTimeEnd.ToString();

        foreach (ScoreAreaDG.InteractionData interaction in ScoreAreaDG.interactionDataListStart)
        {
            i++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[6 + i] = interactionLine;
        }
        foreach (ScoreAreaDG.InteractionData interaction in ScoreAreaDG.interactionDataList)
        {
            j++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[6 + i + j] = interactionLine;
        }

        return returnable;
    }
}
