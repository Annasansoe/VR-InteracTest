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
        public int name;
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
            
        }
    } 

    void Fight()
    {
        currentCharacters[0].hp -= Mathf.Max(Random.Range(1, currentCharacters[1].damage + 1) - currentCharacters[0].armor, 0);
        currentCharacters[1].hp -= Mathf.Max(Random.Range(1, currentCharacters[0].damage + 1) - currentCharacters[1].armor, 0);
    }
}
