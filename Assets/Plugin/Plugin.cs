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
        var size = (System.UIntPtr)data.Length;
        Qoi.Header header;
        decode_header(data, size, out header);
        return header;
    }

    public static void Decode(byte[] data, byte[] output)
    {
        var data_size = (System.UIntPtr)data.Length;
        var output_size = (System.UIntPtr)output.Length;
        decode(data, data_size, output, output_size);
    }

    [DllImport(_dll)]
    public static extern void decode_header
      ([In] byte[] data, System.UIntPtr data_size, out Header output);

    [DllImport(_dll)]
    public static extern void decode
      ([In] byte[] data, System.UIntPtr data_size,
       [Out] byte[] output, System.UIntPtr output_size);
}

} // namespace Qoi
