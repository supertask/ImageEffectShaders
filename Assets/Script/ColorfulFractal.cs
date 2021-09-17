using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


using ImageEffect.Common;

namespace ImageEffect.Painting
{
    [ExecuteInEditMode]
    public class ColorfulFractal : MonoBehaviour
    {
		public int lod = 8;
		public Color[] colors = new Color[]{ new Color(1, 0, 0, 0), new Color(0, 1, 0, 0), new Color(0, 0, 1, 0), new Color(0, 0, 0, 1) };
		public Vector4 tiling = new Vector4(5, 5, 60, 0);

		private Matrix4x4 colorMatrix;
        public RenderTexture ColorfulFractalTex { get; private set; }
        private float timeShiftScale = 0.001f;
		public Material material;

        public static class ShaderIDs
        {
            public static int ColorMatrix = Shader.PropertyToID("_ColorMatrix");
            public static int FractalTiling = Shader.PropertyToID("_FractalTiling");
            public static int TimeShift = Shader.PropertyToID("_TimeShift");
        }

        //protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
		public void Update()
        {
            //this.material.SetFloat("_");

			//Assert.IsTrue(this.material != null, "GenrativeFractal's genFractMat is not assigned a material.");
            
            Assert.IsTrue(this.lod >= 0, "Change LOD. lod >= 0");
			var width = Screen.width >> this.lod;
			var height = Screen.height >> this.lod;
			if (ColorfulFractalTex == null || ColorfulFractalTex.width != width || ColorfulFractalTex.height != height) 
			{
				Debug.Log(string.Format("Init RenderTexture {0}x{1}", width, height));
				Release();
				ColorfulFractalTex = new RenderTexture(width, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
				ColorfulFractalTex.filterMode = FilterMode.Bilinear;
				ColorfulFractalTex.wrapMode = TextureWrapMode.Clamp;
				ColorfulFractalTex.name = "GenerativeFractal RTex";
			}

            for(int i = 0; i < 4; i++)
            {
                this.colorMatrix.SetColumn(i, this.colors[i]);
            }
            this.material.SetFloat(ShaderIDs.TimeShift, Time.time * timeShiftScale);
			this.material.SetMatrix(ShaderIDs.ColorMatrix, this.colorMatrix);
			this.material.SetVector(ShaderIDs.FractalTiling, this.tiling);
            
            //Graphics.Blit(src, dst, this.material);
            Graphics.Blit(null, this.ColorfulFractalTex, this.material);
            //Graphics.Blit(this.ColorfulFractalTex, destination);
        }
        
		void OnDestroy() 
		{
			this.Release();
		}

		public void Release() 
		{
			if (ColorfulFractalTex != null) 
			{
				ColorfulFractalTex.Release();
				ColorfulFractalTex = null;
			}
		}        
    }
}
