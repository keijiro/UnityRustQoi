using System;
using System.Runtime.InteropServices;

namespace Qoi {

public enum Colors { Srgb, SrgbLinA, Rgb, Rgba }

[StructLayout(LayoutKind.Sequential)]
public struct Header
{
    public uint width;
    public uint height;
    public Colors colors;
}

public static class Plugin
{
    #if !UNITY_EDITOR && PLATFORM_IOS
    const string _dll = "__Internal";
    #else
    const string _dll = "qoi";
    #endif

    public static Header DecodeHeader(byte[] data)
    {
        var size = (UIntPtr)data.Length;
        Qoi.Header header;
        if (!decode_header(data, size, out header))
            throw new ArgumentException("Failed to open file.");
        return header;
    }

    public static void Decode(byte[] data, byte[] output)
    {
        var data_size = (UIntPtr)data.Length;
        var output_size = (UIntPtr)output.Length;
        if (!decode(data, data_size, output, output_size))
            throw new ArgumentException("Failed to decode file.");
    }

    [DllImport(_dll)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool decode_header
      ([In] byte[] data, UIntPtr data_size, out Header output);

    [DllImport(_dll)]
    [return: MarshalAs(UnmanagedType.U1)]
    public static extern bool decode
      ([In] byte[] data, UIntPtr data_size,
       [Out] byte[] output, UIntPtr output_size);
}

} // namespace Qoi
