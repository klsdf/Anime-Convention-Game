using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo
{

    public class ShadowScale : MonoBehaviour
    {

        public Transform lightSource;
        private SpriteRenderer shadowSprite;


        public float directionStrength =1.0f;
        public float baseScaleX = 1.0f;
        public float baseScaleY = 1.0f;

        public float distanceFactor = 1.0f;
        public float distanceFactor2 = 0.035f;


        private void Awake()
        {
            shadowSprite = GetComponent<SpriteRenderer>();

        }

        void Update()
        {


            if(lightSource==null)
            {return;}
            Vector3 direction = (transform.position - lightSource.position).normalized*directionStrength;
     

            float distance = Vector2.Distance(transform.position, lightSource.position);


            float scaleX = Mathf.Clamp(   Mathf.Abs( (distance / distanceFactor) * direction.x), 0.0f, 2.0f);
            float scaleY = Mathf.Clamp( Mathf.Abs( (distance / distanceFactor) * direction.y), 0.0f, 2.0f);


            scaleX = baseScaleX + scaleX;
            scaleY = baseScaleY + scaleY;

            transform.localScale = new Vector3(-scaleX, -scaleY, 1);
            transform.localPosition = direction * distance * distanceFactor2;


        }
    }


}


