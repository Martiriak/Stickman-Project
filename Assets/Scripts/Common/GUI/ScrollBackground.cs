using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stickman
{
    public class ScrollBackground : MonoBehaviour
    {
        public float scrollSpeed = 0.5f;
        private Material mat;

        void Start(){
            mat = GetComponent<Renderer>().material;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 offset = new Vector2(Time.time * scrollSpeed, 0);
            mat.SetTextureOffset("_MainTex", offset);
        }
    }
}
