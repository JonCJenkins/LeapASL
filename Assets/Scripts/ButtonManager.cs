using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Leap.Unity
{
    public class ButtonManager : MonoBehaviour
    {
        public static string CurrentWord;

        public void Training()
        {
            SceneManager.LoadScene(1);
        }

        public void Guessing()
        {
            var go = EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                //HandData.path = "Assets/Data/" + go.name + ".txt";
                CurrentWord = "";
                SceneManager.LoadScene(2);
            }
            else
                Debug.Log("Current Selected Object is Null");
        }

        public void Home()
        {
            SceneManager.LoadScene(0);
        }

        public void Word()
        {
            var go = EventSystem.current.currentSelectedGameObject;
            if (go != null)
            {
                //HandData.path = "Assets/Data/" + go.name + ".txt";
                CurrentWord = go.name;
                Debug.Log(CurrentWord);
                SceneManager.LoadScene(2);
            }
            else
                Debug.Log("Current Selected Object is Null");

        }

        public void Alphabet()
        {
            SceneManager.LoadScene(3);
        }
    }
}