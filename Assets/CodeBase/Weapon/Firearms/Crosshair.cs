using CodeBase.StaticData.Items.WeaponItems.FirearmsWeapon;
using UnityEngine;
using static UnityEngine.Screen;

namespace CodeBase.Weapon.Firearms
{
    public class Crosshair
    {
        private readonly Vector2 CenterSrceen = new Vector2(width * 0.5f, height * 0.5f);

        private CrosshairConfig _config;
        private float _currentCrosshairSize;
        private Texture2D _texture;

        public void ResetSettings(CrosshairConfig crosshairConfig)
        {
            _config = crosshairConfig;
            InitTexture();
        }

        public void OnGUI() =>
            DrawCrosshair();

        public void UpdateSize(float size) =>
            _currentCrosshairSize = size + _config.MinSize;

        public void Freeze() =>
            CleanTexture();

        public void UnFreeze() =>
            InitTexture();

        private void DrawCrosshair()
        {
            DrawTexture(CenterSrceen.x - _currentCrosshairSize, CenterSrceen.y, _config.Lenght, _config.Width); //Left
            DrawTexture(CenterSrceen.x + _currentCrosshairSize, CenterSrceen.y, _config.Lenght, _config.Width); //Right

            DrawTexture(CenterSrceen.x, CenterSrceen.y - _currentCrosshairSize, _config.Width, _config.Lenght); //Up
            DrawTexture(CenterSrceen.x, CenterSrceen.y + _currentCrosshairSize, _config.Width, _config.Lenght); //Down
        }

        private void DrawTexture(float positionX, float positionY, float height, float width)
        {
            float centerX = positionX - (height / 2);
            float centerY = positionY - (width / 2);
            Rect position = new Rect(centerX, centerY, height, width);
            GUI.DrawTexture(position, _texture, ScaleMode.StretchToFill);
        }

        private void InitTexture()
        {
            _texture = new Texture2D(1, 1);
            _texture.SetPixel(1, 1, new Color(_config.Color.r, _config.Color.g, _config.Color.b));
            _texture.Apply();
        }

        private void CleanTexture()
        {
            _texture = new Texture2D(1, 1);
            _texture.SetPixel(1, 1, Color.clear);
            _texture.Apply();
        }
    }
}