using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkySreenUtils : MonoBehaviour
{

    public enum ScreenSizeType
    {
        Size_16_9,
        Size_4_3
    }

    public class ScreenStandard
    {
        public float width;
        public float height;
        public ScreenSizeType type;

        public ScreenStandard (ScreenSizeType type)
        {
            string  temp = type.ToString();
            string[]  temps = temp.Split('_');
            this.width = float.Parse( temps[1]);
            this.height = float.Parse( temps[2]);;
            this.type = type;
        }
    }


    private static List<ScreenStandard> screenStandards = new List<ScreenStandard> (new ScreenStandard[] {
        new ScreenStandard (ScreenSizeType.Size_16_9),
        new ScreenStandard (ScreenSizeType.Size_4_3)
    });

    private static ScreenStandard defaultSize = new ScreenStandard (ScreenSizeType.Size_16_9);

    public static ScreenStandard GetNearestSize ()
    {
        float ratio = Screen.width * 1f / Screen.height;
        float factor = 0;
        ScreenStandard result = defaultSize;
        foreach (ScreenStandard size in screenStandards) {
            float tempRatio = (size.width / size.height) / ratio;
            if (tempRatio > 1) {
                tempRatio = 1 / tempRatio;
            }
            
            if (tempRatio > factor) {
                factor = tempRatio;
                result = size;
            }
        }
        return result;
    }

    public static ScreenSizeType GetScreenSizeType(){
        return GetNearestSize ().type;
    }

}
