using UnityEngine;

static class QoiHelper
{
    public static bool IsAlphaEnabled(Qoi.Colors colors)
      => colors == Qoi.Colors.SrgbLinA || colors == Qoi.Colors.Rgba;

    public static int GetBytePerPixel(Qoi.Colors colors)
      => IsAlphaEnabled(colors) ? 4 : 3;

    public static TextureFormat GetTextureFormat(Qoi.Colors colors)
    {
        switch (colors)
        {
            case Qoi.Colors.Srgb     : return TextureFormat.RGB24;
            case Qoi.Colors.SrgbLinA : return TextureFormat.RGBA32;
            case Qoi.Colors.Rgb      : return TextureFormat.RGB24;
            case Qoi.Colors.Rgba     : return TextureFormat.RGBA32;
        }
        return TextureFormat.RGBA32;
    }
}

sealed class Test : MonoBehaviour
{
    [SerializeField] TextAsset _source = null;

    void Start()
    {
        var header = Qoi.Plugin.DecodeHeader(_source.bytes);
        Debug.Log($"{header.width} x {header.height} - {header.colors}");

        var size = header.width * header.height *
          QoiHelper.GetBytePerPixel(header.colors);

        var buffer = new byte[size];
        Qoi.Plugin.Decode(_source.bytes, buffer);

        var texture = new Texture2D
          ((int)header.width, (int)header.height,
           QoiHelper.GetTextureFormat(header.colors), false);

        texture.LoadRawTextureData(buffer);
        texture.Apply();

        GetComponent<Renderer>().material.mainTexture = texture;
        transform.localScale =
          new Vector3((float)header.width / header.height, 1, 1);
    }
}
