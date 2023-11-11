using UnityEngine;
using System;

public class ScoreManagerHandsDG : MonoBehaviour
{
    public void BackToMenuHDG()
    {
        
        CSVManager.AppendToReport(GetReportLineDG());
        ScoreAreaHandsDG.indexTextOneHand++;
        ScoreAreaHandsDG.totScore = 0;
        ScoreAreaHandsDG.totScoreEnd = 0;
        ObjectResetPlaneForSceneOne.objectFell = 0;
        ScoreAreaHandsDG.interactionDataListStart.Clear();
        ScoreAreaHandsDG.interactionDataList.Clear();
     }

    public static string[] GetReportLineDG()
    {

        int i = 0;
        int j = 0;
        string[] returnable = new string[60];
        returnable[0] = "SceneOne.csv";
        returnable[1] = "Hands";
        returnable[2] = "Direct";
        returnable[3] = ScoreAreaHandsDG.indexTextOneHand.ToString();
        returnable[4] = ScoreAreaHandsDG.totScoreEnd.ToString();
        returnable[5] = ObjectResetPlaneForSceneOne.objectFell.ToString();
        returnable[6] = ScoreAreaHandsDG.dateTimeStart.ToString();
        returnable[7] = ScoreAreaHandsDG.dateTimeEnd.ToString();

        foreach (ScoreAreaHandsDG.InteractionData interaction in ScoreAreaHandsDG.interactionDataListStart)
        {
            i++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[7 + i] = interactionLine;
        }
        foreach (ScoreAreaHandsDG.InteractionData interaction in ScoreAreaHandsDG.interactionDataList)
        {
            j++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[7 + i + j] = interactionLine;
        }

        return returnable;
    }
}
