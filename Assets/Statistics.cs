﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Valve.VR.InteractionSystem;

public class Statistics : SingletonComponent<Statistics> {

    public struct Data
    {
        public float firstObstacleCheckpointTime;
        public float secondObstacleCheckpointTime;
        public float upstairsCheckpointTime;
        public float downstairsCheckpointTime;

        public int teleportCount;
        public int oobCount;
    }

    public Data data;

    public Player player;

    public bool allowDataCollection = true;

    private StreamWriter leftHandSW, rightHandSW, headSW;

	void Start () {
        data = new Data();

        leftHandSW = new StreamWriter("leftHand.txt");
        rightHandSW = new StreamWriter("rightHand.txt");
        headSW = new StreamWriter("head.txt");
	}
	
	void Update () {
        foreach(Hand hand in player.hands)
        {
            if(hand.GuessCurrentHandType() == Hand.HandType.Left)
            {
                leftHandSW.WriteLine((hand.transform.localPosition.x - player.hmdTransform.localPosition.x) + "," + (hand.transform.localPosition.y - player.hmdTransform.localPosition.y) + "," + (hand.transform.localPosition.y - player.hmdTransform.localPosition.y));
            }
            else if(hand.GuessCurrentHandType() == Hand.HandType.Right)
            {
                rightHandSW.WriteLine((hand.transform.localPosition.x - player.hmdTransform.localPosition.x) + "," + (hand.transform.localPosition.y - player.hmdTransform.localPosition.y) + "," + (hand.transform.localPosition.y - player.hmdTransform.localPosition.y));
            }
        }

        headSW.WriteLine(player.hmdTransform.forward.x + "," + player.hmdTransform.forward.y + "," + player.hmdTransform.forward.z);
	}

    public void Save()
    {
        using (StreamWriter sw = new StreamWriter("runTimes.txt"))
        {
            sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
            sw.WriteLine("firstObstacleCheckpointTime=" + data.firstObstacleCheckpointTime);
            sw.WriteLine("secondObstacleCheckpointTime=" + data.secondObstacleCheckpointTime);
            sw.WriteLine("upstairsCheckpointTime=" + data.upstairsCheckpointTime);
            sw.WriteLine("downstairsCheckpointTime=" + data.downstairsCheckpointTime);
            sw.WriteLine("teleportCount=" + data.teleportCount);
            sw.WriteLine("oobCount=" + data.oobCount);
        }

        leftHandSW.Flush();
        rightHandSW.Flush();
        headSW.Flush();
    }
}
