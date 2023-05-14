using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Minimap
{
    public class MinimapSetTextureRenderer : MonoBehaviour
    {
        [SerializeField] private RawImage _rawImage;

        public void Construct(RenderTexture texture) =>
            _rawImage.texture = texture;
    }
}