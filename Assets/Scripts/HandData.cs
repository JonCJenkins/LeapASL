using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using System;
using System.IO;
/*
namespace Leap.Unity
{
    public class HandData : MonoBehaviour
    {
        GameObject CapHandL, CapHandR, DontSignLeft, DontSignRight; //Hand gameobjects and hand visibility monitors

        Hand handRight, handLeft; //Hand objects

        Leap.Vector leftV, rightV, //Palm position
            leftFingerPos, rightFingerPos, //Finger tip positions
            leftSpeed, rightSpeed; //Left and right hand speed

        Vector3 leftVec, rightVec, //Palm Location
            leftFingerVec, rightFingerVec; //Used to store finger tip location

        string handData, leftData, rightData; //Hand data strings

        float leftPitch, rightPitch,
            leftYaw, rightYaw,
            leftRoll, rightRoll,
            leftSpeedM, rightSpeedM;

        float handDist, //Distance between palms
            thumbLDist, thumbRDist, //distance from finger tips to palms for each digit on each hand
            indexLDist, indexRDist,
            middleLDist, middleRDist,
            ringLDist, ringRDist,
            pinkyLDist, pinkyRDist,
            leftConf, rightConf; //Hand Confidence

        bool indexExtL, indexExtR,//Check if each finger is extended
            thumbExtL, thumbExtR,
            middleExtL, middleExtR,
            ringExtL, ringExtR,
            pinkyExtL, pinkyExtR;

        public static string path = "Assets/Data/HandData.csv"; //Default data path

        int i = 0; //Integer that changes based on if both hands have been found yet

        List<Finger> fingersL; //Left finger list
        List<Finger> fingersR; //Right finger lsit

        bool newFile = false; //Bool to check if new file needs to be created

        void Awake()
        {
            if(File.Exists(path))
            {
                FileStream fs = File.OpenWrite(path);
            }
            else
            {
                FileStream fs = File.Create(path);
                newFile = true;
            }

            DontSignLeft = GameObject.Find("DontSignLeft");
            DontSignRight = GameObject.Find("DontSignRight");
            
        }

        void Start()
        {
            if(newFile)
            {
                WriteString("Class,LeftX,LeftY,LeftZ,LeftThumb,LeftIndex,LeftMiddle,LeftRing,LeftPinky,leftSpeed,leftConf,leftPitch,LeftYaw,LeftRoll,leftThumbExt,leftIndexExt,leftMiddleExt,leftRingExt,leftPinkyExt,RightX,RightY,RightZ,RightThumb,RightIndex,RightMiddle,RightRing,RightPinky,rightSpeed,rightConf,rightPitch,rightYaw,rightRoll,rightThumbExt,rightIndexExt,rightMiddleExt,rightRingExt,rightPinkyExt,HandDist");
            }
        }
        
        void FixedUpdate()
        {
            if(CapHandL == null || CapHandR == null)
            {
                CapHandL = GameObject.Find("CapsuleHandLeft");
                CapHandR = GameObject.Find("CapsuleHandRight");
                return;
            }
            else if(i == 0)
            {
                i = 1;
                DontSignLeft.SetActive(false);
                DontSignRight.SetActive(false);
            }
            
            handLeft = CapHandL.GetComponent<CapsuleHand>().GetLeapHand();
            handRight = CapHandR.GetComponent<CapsuleHand>().GetLeapHand();

            leftV = handLeft.PalmPosition;
            rightV = handRight.PalmPosition;
            leftVec = new Vector3(leftV.x, leftV.y, leftV.z);
            rightVec = new Vector3(rightV.x, rightV.y, rightV.z);
            handDist = Vector3.Distance(leftVec, rightVec);

            fingersL = handLeft.Fingers;
            fingersR = handRight.Fingers;

            leftSpeed = handLeft.PalmVelocity;
            rightSpeed = handRight.PalmVelocity;
            leftSpeedM = leftSpeed.Magnitude;
            rightSpeedM = leftSpeed.Magnitude;

            leftConf = handLeft.Confidence;
            rightConf = handRight.Confidence;

            leftPitch = handLeft.Direction.Pitch;
            rightPitch = handRight.Direction.Pitch;
            leftYaw = handLeft.Direction.Yaw;
            rightYaw = handRight.Direction.Yaw;
            leftRoll = handLeft.PalmNormal.Roll;
            rightRoll = handRight.PalmNormal.Roll;

            for (int i = 0; i < 5; i++)
            {
                leftFingerPos = fingersL[i].TipPosition;
                rightFingerPos = fingersR[i].TipPosition;
                leftFingerVec = new Vector3(leftFingerPos.x, leftFingerPos.y, leftFingerPos.z);
                rightFingerVec = new Vector3(rightFingerPos.x, rightFingerPos.y, rightFingerPos.z);
                switch (i)
                {
                    case 0:
                        thumbLDist = Vector3.Distance(leftFingerVec, leftVec);
                        thumbRDist = Vector3.Distance(leftFingerVec, rightVec);
                        thumbExtL = fingersL[i].IsExtended;
                        thumbExtR = fingersR[i].IsExtended;
                        break;
                    case 1:
                        indexLDist = Vector3.Distance(leftFingerVec, leftVec);
                        indexRDist = Vector3.Distance(leftFingerVec, rightVec);
                        indexExtL = fingersL[i].IsExtended;
                        indexExtR = fingersR[i].IsExtended;
                        break;
                    case 2:
                        middleLDist = Vector3.Distance(leftFingerVec, leftVec);
                        middleRDist = Vector3.Distance(leftFingerVec, rightVec);
                        middleExtL = fingersL[i].IsExtended;
                        middleExtR = fingersR[i].IsExtended;
                        break;
                    case 3:
                        ringLDist = Vector3.Distance(leftFingerVec, leftVec);
                        ringRDist = Vector3.Distance(leftFingerVec, rightVec);
                        ringExtL = fingersL[i].IsExtended;
                        ringExtR = fingersR[i].IsExtended;
                        break;
                    case 4:
                        pinkyLDist = Vector3.Distance(leftFingerVec, leftVec);
                        pinkyRDist = Vector3.Distance(leftFingerVec, rightVec);
                        pinkyExtL = fingersL[i].IsExtended;
                        pinkyExtR = fingersR[i].IsExtended;
                        break;
                }
            }

            print(leftSpeedM);

            leftData = 
                leftV.x + ","
                + leftV.y + ","
                + leftV.z + ","
                + thumbLDist + ","
                + indexLDist + ","
                + middleLDist + ","
                + ringLDist + ","
                + pinkyLDist + ","
                + leftSpeedM + ","
                + leftConf + ","
                + leftPitch + ","
                + leftYaw + ","
                + leftRoll + ","
                + thumbExtL + ","
                + indexExtL + ","
                + middleExtL + ","
                + ringExtL + ","
                + pinkyExtL + ",";
            
            rightData = 
                rightV.x + ","
                + rightV.y + ","
                + rightV.z + ","
                + thumbRDist + ","
                + indexRDist + ","
                + middleRDist + ","
                + ringRDist + ","
                + pinkyRDist + ","
                + rightSpeedM + ","
                + rightConf + ","
                + rightPitch + ","
                + rightYaw + ","
                + rightRoll + ","
                + thumbExtR + ","
                + indexExtR + ","
                + middleExtR + ","
                + ringExtR + ","
                + pinkyExtR + ",";
            
            if (CapHandL.activeSelf == false)
            {
                DontSignLeft.SetActive(true);
                leftData = ",,,,,,,,,,,,,,,,,,";
                //print("Left DUPE");
            }
            else
            {
                DontSignLeft.SetActive(false);
            }

            if(CapHandR.activeSelf == false)
            {
                DontSignRight.SetActive(true);
                rightData = ",,,,,,,,,,,,,,,,,,";
                //print("Right DUPE");
            }
            else
            {
                DontSignRight.SetActive(false);
            }

            if((CapHandL.activeSelf == false) && (CapHandR.activeSelf == false))
            {
                return;
            }

            handData =
                ButtonManager.CurrentWord + ","
                + leftData
                + rightData
                + handDist;

            WriteString(handData);
        }

        void WriteString(string data)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(data);
            writer.Close();
        }
    }
}
*/
