using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class PythonInterfacer : MonoBehaviour
{
    void Start()
    {
        GetModel();
    }

    private void GetModel()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = @"/C cd Assets\Data&python save.py";
        process.StartInfo = startInfo;
        process.Start();
    }
}
