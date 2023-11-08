using UnityEngine;
using System;

public class ScoreManagerHands : MonoBehaviour
{
    public void BackToMenuHands()
    {
        CSVManager.AppendToReport(GetReportLine());
        ScoreAreaHands.indexTextOneHand++;
        ScoreAreaHands.totScore = 0;
        ScoreAreaHands.totScoreEnd = 0;
        ObjectResetPlaneForSceneOne.objectFell = 0;
        ScoreAreaHands.interactionDataListStart.Clear();
        ScoreAreaHands.interactionDataList.Clear();
    }

    public static string[] GetReportLine()
    {

        int i = 0;
        int j = 0;
        string[] returnable = new string[60];
        returnable[0] = "SceneOne.csv";
        returnable[1] = "Hands";
        returnable[2] = "Raycasting";
        returnable[3] = ScoreAreaHands.indexTextOneHand.ToString();
        returnable[4] = ScoreAreaHands.totScoreEnd.ToString();
        returnable[5] = ObjectResetPlaneForSceneOne.objectFell.ToString();
        returnable[6] = ScoreAreaHands.dateTimeStart.ToString();
        returnable[7] = ScoreAreaHands.dateTimeEnd.ToString();

        foreach (ScoreAreaHands.InteractionData interaction in ScoreAreaHands.interactionDataListStart)
        {
            i++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[7 + i] = interactionLine;
        }
        foreach (ScoreAreaHands.InteractionData interaction in ScoreAreaHands.interactionDataList)
        {
            j++;
            string interactionLine = $"{interaction.Timestamp},{interaction.ObjectName},{interaction.InteractionType}";
            returnable[7 + i + j] = interactionLine;
        }

        return returnable;
    }
}
