using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ImageUtil
{
    public static Color32[] Flip(WebCamTexture texture)
    {
        return Flip(texture.GetPixels32(), texture.width, texture.height);
    }

    public static Color32[] Flip(Color32[] original, int w, int h)
    {
        Color32[] flipped = new Color32[original.Length];

        for (int i = 0; i < h; i++)
        {
            Array.Copy(original, i * w, flipped, (h - i - 1) * w, w);
        }

        return flipped;
    }

    public static void ToColor32(ref Color32[] colors, IntPtr framePtr, int width, int height)
    {
        int size = width * height;
        if (colors == null || colors.Length != size)
        {
            colors = new Color32[size];
        }

        GCHandle handle;
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptrColors = handle.AddrOfPinnedObject();
            unsafe
            {
                Buffer.MemoryCopy(framePtr.ToPointer(), ptrColors.ToPointer(), size * 4, size * 4);
            }
        }
        finally
        {
            handle.Free();
        }
    }

    public static void ToColor32(ref Color32[] colors, byte[] frameRaw)
    {
        if (colors == null || (colors.Length != frameRaw.Length / 4))
        {
            colors = new Color32[frameRaw.Length / 4];
        }

        GCHandle handle;
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptrColors = handle.AddrOfPinnedObject();
            Marshal.Copy(frameRaw, 0, ptrColors, frameRaw.Length);
        }
        finally
        {
            handle.Free();
        }
    }

    public static byte[] ToByteArray(Color32[] colors)
    {
        if (colors == null || colors.Length == 0)
        {
            return null;
        }

        int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));
        int length = lengthOfColor32 * colors.Length;
        byte[] bytes = new byte[length];

        GCHandle handle = default(GCHandle);
        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptrColors = handle.AddrOfPinnedObject();
            Marshal.Copy(ptrColors, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle))
            {
                handle.Free();
            }
        }

        return bytes;
    }

    public static byte[] GetBytes(IntPtr ptr, int length)
    {
        byte[] bytes = new byte[length];
        Marshal.Copy(ptr, bytes, 0, length);
        return bytes;
    }
}
