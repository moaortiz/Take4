using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


[System.Serializable]public class Score
{
    public Score(string n, float t)
    {
        name = n;
        time = t;
    }
    public string name;
    public float time;
}
public class ScoreList : MonoBehaviour
{
    public List<Score> scores = new List<Score>();
    public string fileName;
    public GameObject input;

    // Start is called before the first frame update
    void Start()
    {
        if(File.Exists(fileName))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                while (true)
                {
                    try
                    {
                        string name = reader.ReadString();
                        float time = reader.ReadSingle();

                        scores.Add(new Score(name, time));
                    }
                    catch (EndOfStreamException e)
                    {
                        break;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewEntry()
    {
        //Get the name input from input field
        string name = input.GetComponent<TMP_InputField>().text;
        //Get GameData componenet
        //Time get current time and sub gamePlayStart
        float time = Time.time - GameData.gamePlayStart;

        scores.Add(new Score(name, time));
    }

    private void OnDestroy()
    {
        using (BinaryWriter write = new BinaryWriter(File.Open(fileName, FileMode.OpenOrCreate)))
        {
            foreach(Score score in scores)
            {
                write.Write(score.name);
                write.Write(score.time);
            }
        }
    }
}
