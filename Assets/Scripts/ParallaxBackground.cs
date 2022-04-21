using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
   [SerializeField] private Vector2 parallaxEffectMultiplier;

   private Transform cameraTransform;
   private Vector3 lastCameraPosition;

   private float textureUnitSizeX;

   private void Awake()
   {
       // for saving camera position
       if (PlayerPrefs.GetInt("SavedCamera") == 1 && PlayerPrefs.GetInt("TimeToLoadCamera") == 1)
        {
            float cX = cameraTransform.transform.position.x;
            float cY = cameraTransform.transform.position.y;

            cX = PlayerPrefs.GetFloat("c_x");
            cY = PlayerPrefs.GetFloat("c_y");

            cameraTransform.transform.position = new Vector2(cX, cY);

            PlayerPrefs.SetInt("TimeToLoadCamera", 0);
            PlayerPrefs.Save();
        }
   }

   private void Start()
   {
       cameraTransform = Camera.main.transform;
       lastCameraPosition = cameraTransform.position;

       Sprite sprite = GetComponent<SpriteRenderer>().sprite;
       Texture2D texture = sprite.texture;

       textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
   }

   public void CameraPosSave()
    {
        PlayerPrefs.SetFloat("c_x", cameraTransform.transform.position.x);
        PlayerPrefs.SetFloat("c_y", cameraTransform.transform.position.y);
        PlayerPrefs.SetInt("SavedCamera", 1);
        PlayerPrefs.Save();
    }

    public void CameraPosLoad()
    {
        PlayerPrefs.SetInt("TimeToLoadCamera", 1);
        PlayerPrefs.Save();
    }

   private void FixedUpdate()
   {
       Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
       transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
       lastCameraPosition = cameraTransform.position;

       // this function doesn't seem to work 
       if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
       {
           Debug.Log("repeat!");
           float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
           transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
       }
   }

}
