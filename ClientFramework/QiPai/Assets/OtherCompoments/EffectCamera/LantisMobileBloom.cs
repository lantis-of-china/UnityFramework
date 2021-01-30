using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

[ExecuteAlways]
public class LantisMobileBloom : MonoBehaviour
{
    //�ֱ���
    [Header("���ͷֱ��ʱ���")]
    [Range(1, 5)]
    public int downSample = 1;
    [Header("������[����ģ����ʱ��ɢ�̶�]")]
    [Range(1, 3)]
    public int samplerScale = 1;
    [Header("������ȡ��ֵ[ǿ����ƫɫ]")]
    public Color colorThreshold = Color.gray;
    [Header("Bloom������ɫ")]
    public Color bloomColor = Color.white;
    [Header("Bloom���ʴ�С")]
    [Range(0.0f, 10.0f)]
    public float bloomFactor = 0.5f;
    [Header("Bloomǿ��")]
    [Range(0, 10)]
    public int bloomTimes = 2;



    [Header("�Ƿ�������")]
    public bool _roundOpen = false;
    [Header("������ɫ")]
    public Color _roundColor = Color.black;
    [Header("���Ƿ�Χ")]
    [Range(0.0f, 1.0f)]
    public float _roundIndeisty = 0.5f;
    [Header("������ֵ")]
    [Range(0.0f, 1.0f)]
    public float _attenuation = 0.5f;
    [Header("��������")]
    [SerializeField]
    public Vector2 _roundCenter = new Vector2(0.0f, 0.0f);


    [Header("�Ƿ�����ɫУ��")]
    public bool _hsvOpen = false;
    [Header("�Ƿ��Ƚ���У��")]
    public bool isHsvEnd = false;
    [Header("ɫ��")]
    [Range(-180.0f, 180.0f)]
    public float _h = 0.0f;
    [Header("���Ͷ�")]
    [Range(0.0f, 10.0f)]
    public float _s = 2.0f;
    [Header("����")]
    [Range(0.0f, 10.0f)]
    public float _v = 1.0f;
    [Header("����ǿ��")]
    [Range(0.0f, 0.5f)]
    public float _normalValue = 0.1f;
    [Header("���߽߱�")]
    [Range(0.0f, 3.0f)]
    public float _normalSeparate = 1.5f;
    [Header("������ֵ")]
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

                //��������RT�����ҷֱ��ʰ���downSameple����  
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




            //ֱ�ӽ�����ͼ�������ͷֱ��ʵ�RT�ϴﵽ���ֱ��ʵ�Ч��  
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

            //������ֵ��ȡ��������,ʹ��pass0���и�����ȡ  
            material.SetVector("_colorThreshold", colorThreshold);
            Graphics.Blit(temp1, temp2, material, 0);

            var bloomTemp = temp2;
            for (var i = 0; i < bloomTimes; ++i)
            {
                var tempCur = bloomTempList_1[i];
                ////��˹ģ��������ģ������������ʹ��pass1���и�˹ģ��  
                material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
                Graphics.Blit(bloomTemp, tempCur, material, 1);
                bloomTemp = tempCur;
            }

            for (var i = 0; i < bloomTimes; ++i)
            {
                var tempCur = bloomTempList_2[i];
                ////��˹ģ��������ģ������������ʹ��pass1���и�˹ģ��  
                material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
                Graphics.Blit(bloomTemp, tempCur, material, 1);
                bloomTemp = tempCur;
            }

            ////Bloom����ģ�����ͼ��ΪMaterial��Blurͼ����  
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
        //�ͷ������RT  
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