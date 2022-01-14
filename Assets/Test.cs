using UnityEngine;

sealed class Test : MonoBehaviour
{
    [SerializeField] TextAsset _source = null;

    void Start()
    {
        var header = Qoi.Plugin.DecodeHeader(_source.bytes);
        Debug.Log($"{header.width} x {header.height} - {header.colors}");

        var buffer = new byte[header.width * header.height * 3];
        Qoi.Plugin.Decode(_source.bytes, buffer);

        var texture = new Texture2D((int)header.width, (int)header.height,
                                    TextureFormat.RGB24, false);
        texture.LoadRawTextureData(buffer);
        texture.Apply();

        GetComponent<Renderer>().material.mainTexture = texture;
    }
}
