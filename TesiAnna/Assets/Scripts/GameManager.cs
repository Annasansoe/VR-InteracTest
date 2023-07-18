using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CHARACTERTYPE
{
    _none = -1,
    john,
    ted,
    anya,
    zoe,
    lilith,
    andrew,
    melissa
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private CharacterData[] characterData;
    [SerializeField] private CHARACTERTYPE[] characters;
    private CharacterData[] currentCharacters;

    [System.Serializable]
    public struct CharacterData
    {
        public string name;
        public int hp;
        public int mp;
        public int damage;
        public int armor;
    }

    void Awake()
    {
        instance = this;
        currentCharacters = new CharacterData[characters.Length];

        for (int i = 0; i < currentCharacters.Length; i++)
        {
            currentCharacters[i] = characterData[(int)characters[i]];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Fight();
            CSVManager.AppendToReport(GetReportLine());
            Debug.Log("<color=magenta>Report updated in game successfully!</color>");
        }
    } 

    void Fight()
    {
        currentCharacters[0].hp -= Mathf.Max(Random.Range(1, currentCharacters[1].damage + 1) - currentCharacters[0].armor, 0);
        currentCharacters[1].hp -= Mathf.Max(Random.Range(1, currentCharacters[0].damage + 1) - currentCharacters[1].armor, 0);
    }

    string[] GetReportLine()
    {
        string[] returnable = new string[5];
        returnable[0] = currentCharacters[0].name + "vs" + currentCharacters[1].name;
        returnable[1] = currentCharacters[0].hp.ToString() + "vs" + currentCharacters[1].hp.ToString();
        returnable[2] = currentCharacters[0].mp.ToString() + "vs" + currentCharacters[1].mp.ToString();
        returnable[3] = currentCharacters[0].damage.ToString() + "vs" + currentCharacters[1].damage.ToString();
        returnable[4] = currentCharacters[0].armor.ToString() + "vs" + currentCharacters[1].armor.ToString();
        return returnable;

    }
}
