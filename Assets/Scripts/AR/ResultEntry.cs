using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using Button = UnityEngine.UI.Button;

public class ResultEntry : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI m_nameOfCust;
    [SerializeField]
    TextMeshProUGUI m_ID;
    [SerializeField]
    RawImage m_targetImage;
    [SerializeField]
    Button m_ProceedToScan;

    public void SetNameOfCust(string _nameOfCust)
    {
        m_nameOfCust.text = _nameOfCust;
    }
    public void SetID(string _id) { m_ID.text = _id; }

    public void SetImage(Texture2D _imgTexture)
    {
        m_targetImage.texture = _imgTexture;
        RectTransform rtTrans = m_targetImage.rectTransform;

        Vector2 rtSizeDelta = rtTrans.sizeDelta;
        rtSizeDelta.x = rtSizeDelta.y * (_imgTexture.width/_imgTexture.height);
        rtTrans.sizeDelta = rtSizeDelta;

        Vector3 pos = rtTrans.anchoredPosition;
        pos.x = 0.5f * rtSizeDelta.x;
        rtTrans.anchoredPosition = pos;
    }
    public Button GetProceedButton() { return m_ProceedToScan; }
}
