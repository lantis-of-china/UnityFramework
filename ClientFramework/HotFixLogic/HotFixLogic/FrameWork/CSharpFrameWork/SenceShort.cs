using UnityEngine;
using System.Collections;
using System.Threading;

public class SenceShort
{
    public static bool isFinish = false;
    /// <summary>
    /// 获取截屏路径
    /// </summary>
    /// <returns></returns>
    public static string GetScreenShortPath()
    {
        return Application.persistentDataPath + "/screenShort.png";
    }

    /// <summary>  
    /// Captures the screenshot2.  
    /// </summary>  
    /// <returns>The screenshot2.</returns>  
    /// 
    public static Texture2D CaptureScreenshot2Path(Rect rect, string Path)
    {
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(rect, 0, 0);

        screenShot.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();

        string filename = Path;

        System.IO.File.WriteAllBytes(filename, bytes);
        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
        return screenShot;
    }

    /// <summary>  
    /// Captures the screenshot2.  
    /// </summary>  
    /// <returns>The screenshot2.</returns>  
    public static Texture2D CaptureScreenshot2(Rect rect, string Path, string ScreenShotName)
    {
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(rect, 0, 0);

        screenShot.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();

        string filename = Path + "/" + ScreenShotName + ".png";

        System.IO.File.WriteAllBytes(filename, bytes);
        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
        return screenShot;
    }


    /// <param name="rect">Rect.截图的区域，左下角为o点</param>  
    public static Texture2D CaptureScreenshotAnsy(Rect rect, string path, MonoBehaviour mb)
    {
        isFinish = false;
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        mb.StartCoroutine(SaveIEnumerator(screenShot, path, rect));

        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
        return screenShot;
    }

    /// <param name="rect">Rect.截图的区域，左下角为o点</param>  
    public static Texture2D CaptureScreenshotAnsy2(Rect rect, string path, MonoBehaviour mb)
    {
        isFinish = false;
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        mb.StartCoroutine(SaveIEnumerator(screenShot, path, rect));

        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
        return screenShot;
    }

    static IEnumerator SaveIEnumerator(Texture2D screenShot, string path, Rect rect)
    {
        yield return new WaitForEndOfFrame();
        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(rect, 0, 0);
        yield return new WaitForEndOfFrame();
        screenShot.Apply();
        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
        isFinish = true;
    }
}
