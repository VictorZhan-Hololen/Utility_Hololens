using System;
using System.Runtime.InteropServices;

public class VideoEncorder
{
    private IntPtr handle;

    public VideoEncorder()
    {
        handle = CreateVideoEncoder();
    }

    public int StartEncoder(int width, int height)
    {
        return StartEncoder(handle, width, height);
    }

    public int EncodeARGB(IntPtr inputdata, int inputsize, IntPtr outputdata, int timestamp)
    {
        return EncodeARGB(handle, inputdata, inputsize, outputdata, timestamp);
    }
    public int EncodeYUV420P(IntPtr inputdata, int inputsize, IntPtr outputdata, int timestamp)
    {
        return EncodeYUV420P(handle, inputdata, inputsize, outputdata, timestamp);
    }

    public int StopEncoder()
    {
        return StopEncoder(handle);
    }

    public bool DestroyVideoEncoder()
    {
        return DestroyVideoEncoder(handle);
    }

    ~VideoEncorder()
    {
        DestroyVideoEncoder(handle);
    }

    [DllImport("libVideoCodec")]
    private static extern int StartEncoder(IntPtr handle, int width, int height);

    [DllImport("libVideoCodec")]
    private static extern int EncodeARGB(IntPtr handle, IntPtr inputdata, int inputsize, IntPtr outputdata, int timestamp);

    [DllImport("libVideoCodec")]
    private static extern int EncodeYUV420P(IntPtr handle, IntPtr inputdata, int inputsize, IntPtr outputdata, int timestamp);

    [DllImport("libVideoCodec")]
    private static extern int StopEncoder(IntPtr handle);

    [DllImport("libVideoCodec")]
    private static extern IntPtr CreateVideoEncoder();

    [DllImport("libVideoCodec")]
    private static extern bool DestroyVideoEncoder(IntPtr handle);
}
