using UnityEngine;

using System.Collections;

using UnityEngine.UI;



[AddComponentMenu("UI/RawImagePlus")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RawImage))]

    public class RawImagePlus : MonoBehaviour, ICanvasRaycastFilter

    {

    public float alphaHitTestMinThreshold = 0.1f;



        private RawImage rawImage;


    private Texture2D _texture;
    void OnEnable()

    {

        rawImage = GetComponent<RawImage>();
       

    }
    private void Update()
    {
        _texture = rawImage.texture as Texture2D;




        if (_texture == null)
        {
            RenderTexture rt = rawImage.texture as RenderTexture;
            RenderTexture.active = rt;
            _texture = new Texture2D(rt.width, rt.height);
            //读取缓冲区像素信息
            _texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            _texture.Apply();

        }
    }



    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)

        {

            if (!enabled)

                return true;

        if (_texture == null)
            return true;

        Vector2 local;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, screenPoint, eventCamera, out local);



            Rect rect = rawImage.rectTransform.rect;



            // Convert to have lower left corner as reference point.

            local.x += rawImage.rectTransform.pivot.x * rect.width;

            local.y += rawImage.rectTransform.pivot.y * rect.height;



            Rect uvRect = rawImage.uvRect;

            float u = local.x / rect.width * uvRect.width + uvRect.x;

            float v = local.y / rect.height * uvRect.height + uvRect.y;



            try

            {

               

                    return _texture.GetPixelBilinear(u, v).a > alphaHitTestMinThreshold;

             

            }

            catch (UnityException e)

            {

                Debug.LogException(e);

                return true;

            }

        }

    }

