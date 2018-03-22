using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Statistics : SingletonComponent<Statistics> {

    public struct Data
    {
        public float firstObstacleCheckpointTime;
        public float secondObstacleCheckpointTime;
        public float upstairsCheckpointTime;
        public float downstairsCheckpointTime;
    }

    public Data data;

    public bool allowDataCollection = true;

	void Start () {
        data = new Data();
	}
	
	void Update () {
		
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
        }
    }
}
