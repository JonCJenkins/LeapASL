using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Leap.Unity
{
    public class PythonInterfacer : MonoBehaviour
    {
        string savePath;

        public void GetModel()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "cmd.exe";
            savePath = @"/C cd " + Application.streamingAssetsPath + @"&python save.py";
            print(savePath);
            startInfo.Arguments = savePath;
            //startInfo.Arguments = @"/C cd Assets\Data&python save.py";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}