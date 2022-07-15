using UnityEngine;

namespace GraphicTree
{

    public class RectT
    {
        public float x;
        public float y;
        public float width;
        public float height;

        public RectT()
        {
            this.x = 0;
            this.y = 0;
            this.width = 120;
            this.height = 60;
        }

        public RectT(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public RectT Clone()
        {
            RectT rectT = new RectT();
            rectT.x = this.x;
            rectT.y = this.y;
            rectT.width = this.width;
            rectT.height = this.height;
            return rectT;
        }
    }

    public class RectTool
    {
        public static Rect RectTToRect(RectT rectT)
        {
            return new Rect(rectT.x, rectT.y, rectT.width, rectT.height);
        }

        public static RectT RectToRectT(Rect rect)
        {
            return new RectT(rect.x, rect.y, rect.width, rect.height);
        }
    }
}
