using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ImageEffect.Common;

namespace ImageEffect.Painting {
    [ExecuteInEditMode]
    public class Painting : ImageEffectBase  {

        //private Material imageEffectMat;

        protected override void Start() {
            base.Start();
            //this.imageEffectMat = new Material(this.material);
        }

        protected override void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            //this.material.SetFloat("_");
            Graphics.Blit(src, dst, this.material);
        }

    }
}
