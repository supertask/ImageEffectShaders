using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImageEffect.Common;

namespace ImageEffect.Painting {
    [ExecuteInEditMode]
    public class KuwaharaOilPainting : ImageEffectBase
    {
        public bool debugColorfulFractal = false;
        private ColorfulFractal colorfulFractal;

        protected override void Start()
        {
            base.Start();
            //this.imageEffectMat = new Material(this.material);
            this.colorfulFractal = this.GetComponent<ColorfulFractal>();
        }

        protected override void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            this.material.SetTexture("_ColorfulFractalTex", this.colorfulFractal.ColorfulFractalTex);
            if (this.debugColorfulFractal)
                this.material.EnableKeyword("DEBUG_COLORFUL_FRACTAL");
            else
                this.material.DisableKeyword("DEBUG_COLORFUL_FRACTAL");

            Graphics.Blit(src, dst, this.material);
        }

    }
}
