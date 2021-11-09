using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Leap.Unity
{
    public class TextToSpeech : MonoBehaviour
    {
        public static bool TTSTrue;
        public static string TTSWord;

        private void Awake()
        {
            TTSTrue = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(TTSTrue)
            {
                TTSTrue = false;
                WindowsVoice.theVoice.speak(TTSWord);
            }
        }
    }
}