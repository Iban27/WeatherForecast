using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;
using Utils;

public class LoadImageToRaw : MonoBehaviour
{
    [SerializeField] private RawImage _image;
    [SerializeField] private TextMeshProUGUI _progressText;
    public event Action onImageLoaded;
    private int attemptCount = 0;

    public void Initialize(string url)
    {
        StartCoroutine(DownloadingImage(url));
    }

    public void InitializeIcon(string id)
    {
        string imageUrl = $"https://img.icons8.com/?size=100&id={id}&format=png&color=000000";
        StartCoroutine(DownloadingImage(imageUrl));
    }

    private IEnumerator DownloadingImage(string url)
    {
        _progressText.alpha = 0;
        if (ImageCache.TryLoadTexture(url, TimeSpan.FromSeconds(0.1), out Texture2D texture))
        {
            _image.texture = texture;
            onImageLoaded?.Invoke();
            Debug.Log("1");
        }
        else
        {
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
                    Debug.LogError($"Не удалось загрузить изображение {request.error}");
                    yield return new WaitForSeconds(1);
                    StartCoroutine(DownloadingImage(url));
                    attemptCount++;
                    if (attemptCount > 5)
                    {
                        Debug.Log("Не удалось загрузить изображение, попытки исчерпаны");
                        onImageLoaded?.Invoke();
                        Debug.Log("2");
                        yield break;
                    }
                }
                else
                {
                    Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(request);
                    Debug.Log("Пытаюсь загрузить" + url);
                    _image.texture = downloadedTexture;
                    ImageCache.SaveTexture(url, downloadedTexture);
                    onImageLoaded?.Invoke();
                    Debug.Log("3");
                }
            }
        }
    }
}
