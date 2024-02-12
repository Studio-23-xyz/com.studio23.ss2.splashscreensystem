using TMPro;

[System.Serializable]
public class FontData
{
    public TMP_FontAsset FontAsset;
    public FontStyles FontStyle;

    public FontData(FontData data = null)
    {
        if(data == null) return;
        FontAsset = data.FontAsset;
        FontStyle = data.FontStyle;
    }

    public void UpdateFontDataToText(TextMeshProUGUI text)
    {
        if(FontAsset == null) return;
        text.font = FontAsset;
        text.fontStyle = FontStyle;
    }
}