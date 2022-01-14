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

    [DllImport(_dll)]
    public static extern void read_header
      ([In] byte[] data, System.UIntPtr size, out Header header);
}

} // namespace Qoi
