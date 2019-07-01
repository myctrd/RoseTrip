using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequestTest : MonoBehaviour {

    UnityWebRequest request;

	// Use this for initialization
	void Start () {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        
        using (UnityWebRequest www = UnityWebRequest.Get("http://vasapi.meinan.com/resource/not_packages/texture/story/bg/bg002a.jpg"))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                int width = 1920;
                int height = 1080;
                byte[] results = www.downloadHandler.data;
                Texture2D texture = new Texture2D(width, height);
                texture.LoadImage(results);
                yield return new WaitForSeconds(0.01f);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                transform.GetComponent<Image>().sprite = sprite;
                yield return new WaitForSeconds(0.01f);
                Resources.UnloadUnusedAssets();
            }
        }
          //image
        /*
        using (UnityWebRequest www = UnityWebRequest.Get("http://vasapi.meinan.com/resource/not_packages/texture/story/bg/to0801.jpg"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                int width = 1920;
                int height = 1080;
                byte[] results = www.downloadHandler.data;
                Texture2D texture = new Texture2D(width, height);
                texture.LoadImage(results);
                transform.GetComponent<RawImage>().texture = texture;
            }
        }
        */  //rawimage
    }
}
