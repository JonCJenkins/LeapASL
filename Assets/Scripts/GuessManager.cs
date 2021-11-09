using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

namespace Leap.Unity
{
    public class GuessManager : MonoBehaviour
    {
        bool guessStatus = false;
        GameObject textObject, guessTextObject;
        public Text t, g;
        public static string handDataGuess;
        public static bool newGuess;
        private GuessRequester _guessRequester;
        System.Diagnostics.Process process;
        bool req;
        int IndexNum;
        string[] RecentGuesses;
        string LastGuess = "";
        string BestGuess = "";

        private void Awake()
        {
            if(ButtonManager.CurrentWord == "")
            {
                //Debug.Log("Guessing Initiated");
                guessStatus = true;
                req = true;
                GuessRequester.Guessing = true;
                RecentGuesses = new string[21];
                IndexNum = 0;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if(guessStatus == true)
            {
                textObject = GameObject.Find("Text");
                t = textObject.GetComponent<Text>();
                t.text = "Begin Signing";

                guessTextObject = GameObject.Find("GuessText");
                g = guessTextObject.GetComponent<Text>();
                g.text = "";

                //GuessingServer();
                _guessRequester = new GuessRequester();
            }
        }

        private void Update()
        {
            if(guessStatus == true)
            {
                if (req && newGuess)
                {
                    _guessRequester.Start();
                    //Debug.Log("First Guess being sent");
                    req = false;
                }

                if (GuessRequester.GotGuess)
                {
                    //Debug.Log(IndexNum%21);
                    RecentGuesses[IndexNum%21] = GuessRequester._Guess;
                    IndexNum++;

                    LastGuess = BestGuess;
                    BestGuess = RecentGuesses.GroupBy(v => v).OrderByDescending(g => g.Count()).First().Key;

                    string conc = "";
                    for(int i = 0; i < 21; i++)
                    {
                        conc = conc + RecentGuesses[i] + " ";
                    }

                    Debug.Log("Last Twenty Guesses: " + conc);
                    Debug.Log("Current Best Guess: " + BestGuess);
                    
                    if(LastGuess != BestGuess)
                    {
                        TextToSpeech.TTSWord = BestGuess;
                        TextToSpeech.TTSTrue = true;
                        g.text = g.text + " " + BestGuess;
                    }
                    
                    GuessRequester.GotGuess = false;
                    newGuess = false;
                    //Debug.Log("Guess Received from GuessReq");
                }
            }
        }

        

        private void OnDestroy()
        {
            if(guessStatus == true)
            {
                //Debug.Log("OnDestroy() called");
                GuessRequester.Guessing = false;
                process.Kill();
            }
        }

        private void GuessingServer()
        {
            process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C cd Assets\Data&python load.py";
            process.StartInfo = startInfo;
            process.Start();
            Debug.Log("Python Server Running");
        }
    }
}
