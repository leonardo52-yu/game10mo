    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public List<ScoreEntry> topScores = new List<ScoreEntry>();

 [System.Serializable]
    public class ScoreEntry
    {
        public int score;
        public string name;

        public ScoreEntry(int score, string name)
        {
            this.score = score;
            this.name = name;
        }
    }

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Este objeto persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
