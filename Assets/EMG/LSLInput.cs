using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;

public class LSLInput : MonoBehaviour
{
    public EMGAbilityController emgAbilityController;

    private string StreamType = "EMBody RMS zero latency";
    private string StreamType2 = "EMBody RMS aggegrated";
    private string StreamType3 = "EMBody";
    public float scaleInput = 0.1f;

    StreamInfo[] streamInfos1, streamInfos2, streamInfos3;
    StreamInlet streamInlet1, streamInlet2, streamInlet3;

    float[] sample1, sample2;
    string[] sample3;
    private int channelCount1 = 0, channelCount2 = 0, channelCount3 = 0;

    private bool foundStream1, foundStream2, foundStream3;

    public bool IsEMGInitialized => foundStream1 && foundStream2 && foundStream3;

    public bool log = false;

    public static Dictionary<string, int> ClassificationDict = new Dictionary<string, int>()
    {
        { "LEFT", 0},
        { "RIGHT", 1},
        { "BOTH", 2},
        { "NULL_CLASS", -1},
    };


    void Start()
    {
        streamInfos1 = LSL.LSL.resolve_stream("name", StreamType, 1, 5.0);
        if (streamInfos1.Length > 0)
        {
            Debug.Log("found 1 stream");
            streamInlet1 = new StreamInlet(streamInfos1[0]);
            channelCount1 = streamInlet1.info().channel_count();
            streamInlet1.open_stream();
            Debug.Log("opened 1 stream");
            foundStream1 = true;
        }
        else
        {
            Debug.Log("stream 1 not found");
        }

        streamInfos2 = LSL.LSL.resolve_stream("name", StreamType2, 1, 5.0);
        if (streamInfos2.Length > 0)
        {
            Debug.Log("found 2 stream");
            streamInlet2 = new StreamInlet(streamInfos2[0]);
            channelCount2 = streamInlet2.info().channel_count();
            streamInlet2.open_stream();
            Debug.Log("opened 2 stream");
            foundStream2 = true;
        }
        else
        {
            Debug.Log("stream 2 not found");
        }

        streamInfos3 = LSL.LSL.resolve_stream("name", StreamType3, 1, 5.0);
        if (streamInfos3.Length > 0)
        {
            Debug.Log("found 3 stream");
            streamInlet3 = new StreamInlet(streamInfos3[0]);
            channelCount3 = streamInlet3.info().channel_count();
            streamInlet3.open_stream();
            Debug.Log("opened 3 stream");
            foundStream3 = true;
        }
        else
        {
            Debug.Log("stream 3 not found");
        }

        StartCoroutine(LateStart(1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        emgAbilityController.simulateEMGSignal = !IsEMGInitialized;
    }
    void Update()
    {
        if (streamInlet1 == null)
        {
            //Debug.Log("stream1 not found");
        }

        if (streamInlet1 != null)
        {
            sample1 = new float[channelCount1];
            double lastTimeStamp = streamInlet1.pull_sample(sample1, 0.00001f);
            if (lastTimeStamp != 0.0)
            {
                //Process("Type 1", sample1, lastTimeStamp);
                //while ((lastTimeStamp = streamInlet1.pull_sample(sample1, 0.0f)) != 0)
                //{
                //    Debug.Log("sending Type 1");
                //    Process("Type 1", sample1, lastTimeStamp);
                //}
            }
        }

        if (streamInlet2 == null)
        {
            //Debug.Log("stream2 not found");
        }

        if (streamInlet2 != null)
        {
            sample2 = new float[channelCount2];
            double lastTimeStamp = streamInlet2.pull_sample(sample2, 0.00001f);
            if (lastTimeStamp != 0.0)
            {
                Process("Type 2", sample2, lastTimeStamp);
                while ((lastTimeStamp = streamInlet2.pull_sample(sample2, 0.0f)) != 0)
                {
                    Debug.Log("sending Type 2");
                    Process("Type 2", sample2, lastTimeStamp);
                }
            }
        }

        if (streamInlet3 == null)
        {
            //Debug.Log("stream3 not found");
        }

        if (streamInlet3 != null)
        {
            sample3 = new string[channelCount3];
            double lastTimeStamp = streamInlet3.pull_sample(sample3, 0.00001f);
            if (lastTimeStamp != 0.0)
            {
                ProcessClassification("Type 3", sample3, lastTimeStamp);
                while ((lastTimeStamp = streamInlet3.pull_sample(sample3, 0.0f)) != 0)
                {
                    Debug.Log("sending Type 3");
                    ProcessClassification("Type 3", sample3, lastTimeStamp);
                }
            }
        }
    }

    private string lastClassification = "NULL_CLASS";

    void ProcessClassification(string type, string[] newSample, double timeStamp)
    {
        lastClassification = newSample[0];
    }

    void Process(string type, float[] newSample, double timeStamp)
    {
        float sum = 0f;
        for (int i = 0; i < newSample.Length; i++)
        {
            newSample[i] = newSample[i] * scaleInput;
            sum += newSample[i];
        }
        if (log)
        {
            Debug.Log(lastClassification + ": " + sum);
        }

        int index = 0;
        if (ClassificationDict.TryGetValue(lastClassification, out index))
        {
            emgAbilityController.ProcessEmgSignal(type, index, sum, timeStamp);
        }
    }
}