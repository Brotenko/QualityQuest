using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRCode : MonoBehaviour
{


    private Texture2D encoded;
    [Tooltip("Put a picture of a rawimage")]
    public RawImage connectionPanel;
    [Tooltip("Put a picture of a rawimage")]
    public RawImage pausePanel;
    
    /// <summary>
    /// Create a QR code
    /// </summary>
    /// <param name="textForEncoding"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    /// <summary>
    /// Change the last url
    /// </summary>
    /// <param name="url"></param>
    public void GenerateQRCode(string url, int a)
    {
        var textForEncoding = url;
        switch (a)
        {
            case 1:
                encoded = new Texture2D(256, 256);

                
                if (textForEncoding != null)
                {
                    var color32 = Encode(textForEncoding, encoded.width, encoded.height);
                    encoded.SetPixels32(color32);
                    encoded.Apply();
                    connectionPanel.GetComponent<RawImage>().texture = encoded;
                }
                break;
            case 2:
                encoded = new Texture2D(256, 256);

                if (textForEncoding != null)
                {
                    var color32 = Encode(textForEncoding, encoded.width, encoded.height);
                    encoded.SetPixels32(color32);
                    encoded.Apply();
                    pausePanel.GetComponent<RawImage>().texture = encoded;
                }
                break;
        }
    }
}