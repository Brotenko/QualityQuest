using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

/// <summary>
/// Class to generate a QrCode.
/// </summary>
public class QRCode : MonoBehaviour
{
    private Texture2D encoded;
    [Tooltip("Put a picture of a RawImage")]
    public RawImage connectionPanel;
    [Tooltip("Put a picture of a RawImage")]
    public RawImage pausePanel;

    /// <summary>
    /// Creates a qrCode.
    /// </summary>
    /// <param name="textForEncoding">The text that is encoded to a qrCode.</param>
    /// <param name="width">The width of the qrCode.</param>
    /// <param name="height">the height of the qrCode.</param>
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
    /// Method to fill the RawImage with the QrCode.
    /// </summary>
    /// <param name="url">The url to be converted to qrCode.</param>
    /// <param name="a"></param>
    public void GenerateQRCode(string url, QrCodeType qrCodeType)
    {
        
        switch (qrCodeType)
        {
            case QrCodeType.QrCodeConnect:
                encoded = new Texture2D(256, 256);

                
                if (url != null)
                {
                    var color32 = Encode(url, encoded.width, encoded.height);
                    encoded.SetPixels32(color32);
                    encoded.Apply();
                    connectionPanel.GetComponent<RawImage>().texture = encoded;
                }
                break;
            case QrCodeType.QrCodePause:
                encoded = new Texture2D(256, 256);

                if (url != null)
                {
                    var color32 = Encode(url, encoded.width, encoded.height);
                    encoded.SetPixels32(color32);
                    encoded.Apply();
                    pausePanel.GetComponent<RawImage>().texture = encoded;
                }
                break;
            default:
                break;
        }
    }
}