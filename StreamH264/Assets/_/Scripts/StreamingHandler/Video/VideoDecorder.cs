using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class VideoDecorder
{
    private IntPtr handle;

    public VideoDecorder()
    {
        handle = CreateVideoDecoder();
    }

    public int StartDecoder(int width, int height)
    {
        return StartDecoder(handle, width, height);
    }

    public int DecodeH264Frame(IntPtr inputdata, int inputsize, IntPtr outputdata)
    {
        return DecodeH264Frame(handle, inputdata, inputsize, outputdata);
    }

    public int StopDecoder()
    {
        return StopDecoder(handle);
    }

    public bool DestroyVideoDecoder()
    {
        return DestroyVideoDecoder(handle);
    }

    ~VideoDecorder()
    {
        DestroyVideoDecoder(handle);
    }

    [DllImport("libVideoCodec", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int StartDecoder(IntPtr handle, int width,int height);
    [DllImport("libVideoCodec", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int DecodeH264Frame(IntPtr handle, IntPtr inputdata, int inputsize, IntPtr outputdata);

    [DllImport("libVideoCodec", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int StopDecoder(IntPtr handle);

    [DllImport("libVideoCodec", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateVideoDecoder();

    [DllImport("libVideoCodec", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool DestroyVideoDecoder(IntPtr handle);
}
