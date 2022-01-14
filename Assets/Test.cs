using UnityEngine;

sealed class Test : MonoBehaviour
{
    [SerializeField] TextAsset _source = null;

    void Start()
    {
        var header = Qoi.Plugin.ReadHeader(_source.bytes);
        Debug.Log($"{header.width} x {header.height}");
    }
}
