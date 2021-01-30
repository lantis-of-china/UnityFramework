using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

[ExecuteAlways]
public class LantisMobileBloom : MonoBehaviour
{
    //分辨率
    [Header("降低分辨率倍数")]
    [Range(1, 5)]
    public int downSample = 1;
    [Header("采样率[决定模糊的时候发散程度]")]
    [Range(1, 3)]
    public int samplerScale = 1;
    [Header("高亮提取阈值[强度与偏色]")]
    public Color colorThreshold = Color.gray;
    [Header("Bloom泛光颜色")]
    public Color bloomColor = Color.white;
    [Header("Bloom画笔大小")]
    [Range(0.0f, 10.0f)]
    public float bloomFactor = 0.5f;
    [Header("Bloom强度")]
    [Range(0, 10)]
    public int bloomTimes = 2;



    [Header("是否开启暗角")]
    public bool _roundOpen = false;
    [Header("暗角颜色")]
    public Color _roundColor = Color.black;
    [Header("暗角范围")]
    [Range(0.0f, 1.0f)]
    public float _roundIndeisty = 0.5f;
    [Header("暗角阈值")]
    [Range(0.0f, 1.0f)]
    public float _attenuation = 0.5f;
    [Header("暗角中心")]
    [SerializeField]
    public Vector2 _roundCenter = new Vector2(0.0f, 0.0f);


    [Header("是否开启颜色校正")]
    public bool _hsvOpen = false;
    [Header("是否先进行校正")]
    public bool isHsvEnd = false;
    [Header("色相")]
    [Range(-180.0f, 180.0f)]
    public float _h = 0.0f;
    [Header("饱和度")]
    [Range(0.0f, 10.0f)]
    public float _s = 2.0f;
    [Header("明度")]
    [Range(0.0f, 10.0f)]
    public float _v = 1.0f;
    [Header("法线强度")]
    [Range(0.0f, 0.5f)]
    public float _normalValue = 0.1f;
    [Header("法线边界")]
    [Range(0.0f, 3.0f)]
    public float _normalSeparate = 1.5f;
    [Header("法线阈值")]
    [Range(0.0f, 5.0f)]
    public float _normalIntensity = 0.0f;


    public Material material = null;
    private RenderTexture temp1;
    private RenderTexture temp2;
    private List<RenderTexture> bloomTempList_1 = new List<RenderTexture>();
    private List<RenderTexture> bloomTempList_2 = new List<RenderTexture>();
    private int recordBloomTimes = 0;
    private int recordDownSample = 0;

    void Awake()
    {
    }

    void OnEnable()
    {
        ReleseMainTemp();
        ReleseListTemp();
        recordDownSample = -1;
        recordBloomTimes = -1;
    }

    void OnDisable()
    {
        ReleseMainTemp();
        ReleseListTemp();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material)
        {
            if (downSample != recordDownSample || bloomTimes != recordBloomTimes)
            {
                recordDownSample = downSample;
                recordBloomTimes = bloomTimes;
                ReleseMainTemp();
                ReleseListTemp();                

                //申请两块RT，并且分辨率按照downSameple降低  
                temp1 = RenderTexture.GetTemporary(source.width / downSample, source.height / downSample, 0, source.format);
                temp2 = RenderTexture.GetTemporary(source.width / downSample, source.height / downSample, 0, source.format);

                for (var i = 0; i < bloomTimes; ++i)
                {
                    var temp_1 = RenderTexture.GetTemporary(temp1.width / 2, temp1.height / 2, 0, source.format);
                    bloomTempList_1.Add(temp_1);

                    var temp_2 = RenderTexture.GetTemporary(temp1.width / 2, temp1.height / 2, 0, source.format);
                    bloomTempList_2.Add(temp_2);
                }
            }
            DiscardContents();




            //直接将场景图拷贝到低分辨率的RT上达到降分辨率的效果  
            Graphics.Blit(source, temp1);

            if (_hsvOpen)
            {
                material.SetFloat("_H", _h);
                material.SetFloat("_S", _s);
                material.SetFloat("_V", _v);
                material.SetFloat("_NormalValue", _normalValue);
                material.SetFloat("_NormalSeparate", _normalSeparate);
                material.SetFloat("_NormalIntensity", _normalIntensity);

                if (!isHsvEnd)
                {
                    Graphics.Blit(temp1, temp2, material, 3);

                    RenderTexture rt_Change = temp2;
                    temp2 = temp1;
                    temp1 = rt_Change;
                }
            }

            //根据阈值提取高亮部分,使用pass0进行高亮提取  
            material.SetVector("_colorThreshold", colorThreshold);
            Graphics.Blit(temp1, temp2, material, 0);

            var bloomTemp = temp2;
            for (var i = 0; i < bloomTimes; ++i)
            {
                var tempCur = bloomTempList_1[i];
                ////高斯模糊，两次模糊，横向纵向，使用pass1进行高斯模糊  
                material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
                Graphics.Blit(bloomTemp, tempCur, material, 1);
                bloomTemp = tempCur;
            }

            for (var i = 0; i < bloomTimes; ++i)
            {
                var tempCur = bloomTempList_2[i];
                ////高斯模糊，两次模糊，横向纵向，使用pass1进行高斯模糊  
                material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
                Graphics.Blit(bloomTemp, tempCur, material, 1);
                bloomTemp = tempCur;
            }

            ////Bloom，将模糊后的图作为Material的Blur图参数  
            material.SetTexture("_BlurTex", bloomTemp);
            material.SetVector("_bloomColor", bloomColor);
            material.SetFloat("_bloomFactor", bloomFactor);




            if (_hsvOpen && isHsvEnd)
            {
                //Bloom
                material.SetInt("_roundOpen", 0);
                Graphics.Blit(temp1, temp2, material, 2);

                material.SetInt("_roundOpen", 1);
                material.SetVector("_roundCenter", _roundCenter);
                material.SetVector("_colorRound", new Color(1 - _roundColor.r, 1 - _roundColor.g, 1 - _roundColor.b));
                material.SetFloat("_roundIndeisty", _roundIndeisty);
                material.SetVector("_senceSizeHalf", new Vector3(source.width / 2, source.height / 2));
                material.SetFloat("_attenuation", _attenuation);
                material.SetTexture("_BlurTex", temp2);
                //HSV 
                Graphics.Blit(source, destination, material, 4);
            }
            else
            {
                if (_roundOpen)
                {
                    material.SetInt("_roundOpen", 1);
                    material.SetVector("_roundCenter", _roundCenter);
                    material.SetVector("_colorRound", new Color(1 - _roundColor.r, 1 - _roundColor.g, 1 - _roundColor.b));
                    material.SetFloat("_roundIndeisty", _roundIndeisty);
                    material.SetVector("_senceSizeHalf", new Vector3(source.width / 2, source.height / 2));
                    material.SetFloat("_attenuation", _attenuation);
                }
                else
                {
                    material.SetInt("_roundOpen", 0);
                }
                //Bloom
                Graphics.Blit(source, destination, material, 2);
            }
        }
    }

    private void DiscardContents()
    {
        if (temp1 != null)
        {
            temp1.DiscardContents();
            temp2.DiscardContents();
        }

        for (var i = 0; i < bloomTempList_1.Count; ++i)
        {
            bloomTempList_1[i].DiscardContents();
        }
        for (var i = 0; i < bloomTempList_2.Count; ++i)
        {
            bloomTempList_2[i].DiscardContents();
        }
    }

    private void ReleseMainTemp()
    {
        //释放申请的RT  
        if (temp1 != null)
        {
            RenderTexture.ReleaseTemporary(temp1);
            RenderTexture.ReleaseTemporary(temp2);
            temp1 = null;
            temp2 = null;
        }
    }

    private void ReleseListTemp()
    {
        for (var i = 0;i < bloomTempList_1.Count; ++i)
        {
            RenderTexture.ReleaseTemporary(bloomTempList_1[i]);
        }
        for (var i = 0; i < bloomTempList_2.Count; ++i)
        {
            RenderTexture.ReleaseTemporary(bloomTempList_2[i]);
        }
        bloomTempList_1.Clear();
        bloomTempList_2.Clear();
    }
    
    private void OnDestroy()
    {

    }
}