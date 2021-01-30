using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LantisURPBloom : ScriptableRendererFeature
{
	[System.Serializable]
	public class Setting
	{
		public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingTransparents;
        public string passTag = "LantisURPBloom";
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
    }

	public class CustomRenderPass : ScriptableRenderPass
	{
		private Setting setting;
        private RenderTextureDescriptor cameraTargetDescriptor;
        private RenderTargetIdentifier renderSource;
        private CommandBuffer commandBuffer;
        private KeyValuePair<int, RenderTargetIdentifier> temp1;
        private KeyValuePair<int, RenderTargetIdentifier> temp2;
        private List<KeyValuePair<int, RenderTargetIdentifier>> bloomTempList_1 = new List<KeyValuePair<int, RenderTargetIdentifier>>();
        private List<KeyValuePair<int, RenderTargetIdentifier>> bloomTempList_2 = new List<KeyValuePair<int, RenderTargetIdentifier>>();
        private int recordBloomTimes = 0;
        private int recordDownSample = 0;

        public CustomRenderPass(Setting setData)
		{
			this.setting = setData;
		}

		public void Setup(RenderTargetIdentifier source)
		{            
            this.renderSource = source;
        }

		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
            if (setting.material != null)
            {
                recordDownSample = setting.downSample;
                recordBloomTimes = setting.bloomTimes;
                ReleseMainTemp();

                if (commandBuffer == null || (setting.downSample != recordDownSample || setting.bloomTimes != recordBloomTimes))
                {
                    commandBuffer = CommandBufferPool.Get(setting.passTag);
                    cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
                    var tempId1 = Shader.PropertyToID("temp1");
                    var tempId2 = Shader.PropertyToID("temp2");
                    commandBuffer.GetTemporaryRT(tempId1, cameraTargetDescriptor, FilterMode.Bilinear);
                    commandBuffer.GetTemporaryRT(tempId2, cameraTargetDescriptor, FilterMode.Bilinear);
                    temp1 = new KeyValuePair<int, RenderTargetIdentifier>(tempId1,new RenderTargetIdentifier(tempId1));
                    temp2 = new KeyValuePair<int, RenderTargetIdentifier>(tempId2, new RenderTargetIdentifier(tempId2));

                    for (var i = 0; i < setting.bloomTimes; ++i)
                    {
                        var bloomHId = Shader.PropertyToID("bloomH");
                        var bloomVId = Shader.PropertyToID("bloomV");
                        commandBuffer.GetTemporaryRT(bloomHId, cameraTargetDescriptor, FilterMode.Bilinear);
                        commandBuffer.GetTemporaryRT(bloomVId, cameraTargetDescriptor, FilterMode.Bilinear);
                        bloomTempList_1.Add(new KeyValuePair<int, RenderTargetIdentifier>(bloomHId, new RenderTargetIdentifier(bloomHId)));
                        bloomTempList_2.Add(new KeyValuePair<int, RenderTargetIdentifier>(bloomVId, new RenderTargetIdentifier(bloomVId)));
                    }
                }

                commandBuffer.Blit(renderSource, temp1.Value);

                if (setting._hsvOpen)
                {
                    setting.material.SetFloat("_H", setting._h);
                    setting.material.SetFloat("_S", setting._s);
                    setting.material.SetFloat("_V", setting._v);
                    setting.material.SetFloat("_NormalValue", setting._normalValue);
                    setting.material.SetFloat("_NormalSeparate", setting._normalSeparate);
                    setting.material.SetFloat("_NormalIntensity", setting._normalIntensity);

                    if (!setting.isHsvEnd)
                    {
                        commandBuffer.Blit(temp1.Value, temp2.Value, setting.material, 3);

                        var rt_Change = temp2;
                        temp2 = temp1;
                        temp1 = rt_Change;
                    }
                }

                setting.material.SetVector("_colorThreshold", setting.colorThreshold);
                commandBuffer.Blit(temp1.Value, temp2.Value, setting.material, 0);
                var bloomTemp = temp2;

                for (var i = 0; i < setting.bloomTimes; ++i)
                {
                    var tempCur = bloomTempList_1[i];
                    setting.material.SetVector("_offsets", new Vector4(0, setting.samplerScale, 0, 0));
                    commandBuffer.Blit(bloomTemp.Value, tempCur.Value, setting.material, 1);
                    bloomTemp = tempCur;
                }

                for (var i = 0; i < setting.bloomTimes; ++i)
                {
                    var tempCur = bloomTempList_2[i];
                    setting.material.SetVector("_offsets", new Vector4(setting.samplerScale, 0, 0, 0));
                    commandBuffer.Blit(bloomTemp.Value, tempCur.Value, setting.material, 1);
                    bloomTemp = tempCur;
                }

                commandBuffer.SetGlobalTexture("_BlurTex", bloomTemp.Value);
                //setting.material.SetTexture("_BlurTex", bloomTemp);
                setting.material.SetVector("_bloomColor", setting.bloomColor);
                setting.material.SetFloat("_bloomFactor", setting.bloomFactor);
                                             
                if (setting._hsvOpen && setting.isHsvEnd)
                {
                    setting.material.SetInt("_roundOpen", 0);
                    commandBuffer.Blit(temp1.Value, temp2.Value, setting.material, 2);

                    setting.material.SetInt("_roundOpen", 1);
                    setting.material.SetVector("_roundCenter", setting._roundCenter);
                    setting.material.SetVector("_colorRound", new Color(1 - setting._roundColor.r, 1 - setting._roundColor.g, 1 - setting._roundColor.b));
                    setting.material.SetFloat("_roundIndeisty", setting._roundIndeisty);
                    setting.material.SetVector("_senceSizeHalf", new Vector3(cameraTargetDescriptor.width / 2, cameraTargetDescriptor.height / 2));
                    setting.material.SetFloat("_attenuation", setting._attenuation);
                    commandBuffer.SetGlobalTexture("_BlurTex", bloomTemp.Value);
                    //setting.material.SetTexture("_BlurTex", temp2);

                    commandBuffer.Blit(temp2.Value, renderSource, setting.material, 4);
                }
                else
                {
                    if (setting._roundOpen)
                    {
                        setting.material.SetInt("_roundOpen", 1);
                        setting.material.SetVector("_roundCenter", setting._roundCenter);
                        setting.material.SetVector("_colorRound", new Color(1 - setting._roundColor.r, 1 - setting._roundColor.g, 1 - setting._roundColor.b));
                        setting.material.SetFloat("_roundIndeisty", setting._roundIndeisty);
                        setting.material.SetVector("_senceSizeHalf", new Vector3(cameraTargetDescriptor.width / 2, cameraTargetDescriptor.height / 2));
                        setting.material.SetFloat("_attenuation", setting._attenuation);
                    }
                    else
                    {
                        setting.material.SetInt("_roundOpen", 0);
                    }

                    commandBuffer.Blit(bloomTemp.Value, renderSource, setting.material, 2);
                }

                context.ExecuteCommandBuffer(commandBuffer);
            }
        }

        public void ReleseMainTemp()
        {
            ReleseListTemp();

            if (commandBuffer != null)
            {
                commandBuffer.ReleaseTemporaryRT(temp1.Key);
                commandBuffer.ReleaseTemporaryRT(temp2.Key);
                CommandBufferPool.Release(commandBuffer);
                commandBuffer = null;
            }
        }

        public void ReleseListTemp()
        {
            if (commandBuffer != null)
            {
                if (bloomTempList_1 != null)
                {
                    for (var i = 0; i < bloomTempList_1.Count; ++i)
                    {
                        commandBuffer.ReleaseTemporaryRT(bloomTempList_1[i].Key);
                    }
                }

                if (bloomTempList_2 != null)
                {
                    for (var i = 0; i < bloomTempList_2.Count; ++i)
                    {
                        commandBuffer.ReleaseTemporaryRT(bloomTempList_2[i].Key);
                    }
                }
            }

            bloomTempList_1.Clear();
            bloomTempList_2.Clear();
        }
    }

    public Setting setting;
	public CustomRenderPass customRenderPass;

    public override void Create()
    {        
        if (setting == null)
        {
            setting = new Setting();            
        }

        if (customRenderPass == null && setting != null)
        {
            customRenderPass = new CustomRenderPass(setting);
        }
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (setting != null)
        {
            customRenderPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(customRenderPass);
        }
        else
        {
            throw new Exception("when add render passes time setting field is null");
        }
    }
}
