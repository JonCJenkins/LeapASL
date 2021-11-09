using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;

namespace Leap.Unity
{
    public class TextManager : MonoBehaviour
    {
        Text cw;
        string output;

        private void Awake()
        {
            cw = this.GetComponent<Text>();

            if (ButtonManager.CurrentWord == "")
            {
                cw.text = "";
            }
            else
            {
                cw.text = "Sign " + ButtonManager.CurrentWord + " 10 Times to Train";
            }
        }

        void Start()
        {

        }

        public void RunScript()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C cd Assets\Data&python load.py";
            process.StartInfo = startInfo;
            process.Start();
            output = process.StandardOutput.ReadToEnd();
        }
    }
}