using UnityEngine;

namespace M2Lib
{
    public static class SpriteExtension
    {
        public static Sprite ToSprite(this Texture2D from)
        {
            if (from == null)
            {
                Debug.Log("SpriteExtension.ToSprite: from is null.");
                return null;
            }
            return Sprite.Create(from, new Rect(0, 0, from.width, from.height), Vector2.zero);
        }
    }
}