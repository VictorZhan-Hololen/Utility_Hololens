using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音源格式：PCM, 16bit 8000 hz (PCM不需解碼)
/// </summary>
public class AudioDecodeHandler
{
    private AudioSource audioSource;
    private AudioClip audioClip;

    private float[] audioFloatData;


    public AudioDecodeHandler(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    /// <summary> 播放串流聲音資料 </summary>
    public void Play(byte[] rawData)
    {
        int audioSize = rawData.Length - 1;
        byte[] audioByteData = new byte[audioSize];

        Array.Copy(rawData, 1, audioByteData, 0, audioSize);
        audioFloatData = PCM2Floats(audioByteData);
        Debug.Log($"Lenght:{rawData.Length} / {audioByteData.Length} / {audioFloatData.Length}");
        // ByteArrayToFloatArray(audioByteData, audioSize,ref audioFloatData);
    }
    /// <summary> byte[] to float[] </summary>
    private float[] PCM2Floats(byte[] bytes)
    {
        float max = (float)System.Int16.MinValue;
        float[] samples = new float[bytes.Length / 2];

        for (int i = 0; i < samples.Length; i++)
        {
            short int16sample = System.BitConverter.ToInt16(bytes, i * 2);
            samples[i] = (float)int16sample / max;
        }

        return samples;
    }

    private float[] PCM2Floats2(byte[] bytes)
    {
        float max = -(float)System.Int32.MinValue;
        float[] samples = new float[bytes.Length / 2];
        for (int i = 0; i < samples.Length; i++)
        {
            int int16sample = System.BitConverter.ToInt32(bytes, i * 2);
            samples[i] = (float)int16sample / max;
        }

        return samples;
    }

   

    public int position = 0;
    public int samplerate = 44100;
    public float frequency = 16;
    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate);
            position++;
            count++;
        }
    }
    void OnAudioSetPosition(int newPosition)
    {
        position = newPosition;
    }

    private int channel = 1;
    private int hz = 8000;

    public void OnUpdate()
    {
        if (audioFloatData != null)
        {
            if (audioClip == null)
            {
                // audioClip = AudioClip.Create("StreamingSound", audioFloatData.Length, 1, 8000, true, OnAudioRead, OnAudioSetPosition);
                audioClip = AudioClip.Create("StreamingSound", audioFloatData.Length * channel, channel, hz, false);
                audioSource.clip = audioClip;
                // audioClip = AudioClip.Create("StreamingSound", audioFloatData.Length, 1, 8000, false);
            }
            audioClip.SetData(audioFloatData, 0);
            audioSource.Play();
           // audioClip.UnloadAudioData();
        }
    }

    public static int ByteArrayToFloatArray(byte[] byteArray, int byteArray_length, ref float[] resultFloatArray)

    {
        if (resultFloatArray == null || resultFloatArray.Length != (byteArray_length / 2))

            resultFloatArray = new float[byteArray_length / 2];


        int arrIdx = 0;

        for (int i = 0; i < byteArray_length; i += 2)

            resultFloatArray[arrIdx++] = BytesToFloat(byteArray[i], byteArray[i + 1]);



        return resultFloatArray.Length;

    }



    static float BytesToFloat(byte firstByte, byte secondByte)

    {
        return (float)((short)((int)secondByte << 8 | (int)firstByte)) / 32768f;

    }

    private static float[] Convert16BitByteArrayToAudioClipData(byte[] source, int headerOffset, int dataSize)
    {
        int wavSize = BitConverter.ToInt32(source, headerOffset);
        headerOffset += sizeof(int);
        Debug.AssertFormat(wavSize > 0 && wavSize == dataSize, "Failed to get valid 16-bit wav size: {0} from data bytes: {1} at offset: {2}", wavSize, dataSize, headerOffset);

        int x = sizeof(Int16); // block size = 2
        int convertedSize = wavSize / x;

        float[] data = new float[convertedSize];

        Int16 maxValue = Int16.MaxValue;

        int offset = 0;
        int i = 0;
        while (i < convertedSize)
        {
            offset = i * x + headerOffset;
            data[i] = (float)BitConverter.ToInt16(source, offset) / maxValue;
            ++i;
        }

        Debug.AssertFormat(data.Length == convertedSize, "AudioClip .wav data is wrong size: {0} == {1}", data.Length, convertedSize);

        return data;
    }

    public void OnDestory()
    {
    }
}
