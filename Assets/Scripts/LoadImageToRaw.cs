using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class LoadImageToRaw : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _progressText;
    public event Action onImageLoaded;

    public void Initialize(string id)
    {
        string _imageUrl = $"https://img.icons8.com/?size=100&id={id}&format=png&color=000000";
        StartCoroutine(DownloadingImage(_imageUrl));
    }

    private IEnumerator DownloadingImage(string url1)
    {
        string url = "https://i.pinimg.com/originals/4b/22/96/4b22966db450e1a77d19fc7ab07cf930.jpg";
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            request.SendWebRequest();
            while (request.isDone == false)
            {
                float percentage = request.downloadProgress * 100;
                _progressText.text = percentage.ToString("f2") + "%";
                yield return new WaitForEndOfFrame();
            }
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Не удалось загрузить картинку {request.error}");
            }
            else
            {
                Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
                _image.texture = downloadedTexture;
                onImageLoaded?.Invoke();
            }
        }
    }

    void Start()
    {
        //StartCoroutine(DownloadingImage(_imageUrl));
    }
}
