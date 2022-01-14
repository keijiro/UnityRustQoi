using UnityEngine;

sealed class Test : MonoBehaviour
{
    [SerializeField] TextAsset _source = null;

    void Start()
    {
        var size = (System.UIntPtr)_source.bytes.Length;
        Qoi.Header header;
        Qoi.Plugin.read_header(_source.bytes, size, out header);

        Debug.Log($"{header.width} x {header.height}");
    }
}
