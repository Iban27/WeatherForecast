using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdditionalInformationView
{
    private TextMeshProUGUI _additionalInfo;

    public AdditionalInformationView(TextMeshProUGUI additionalInfo)
    {
        _additionalInfo = additionalInfo;
    }

    public void UpdateAdditionalInfoText(string additionalInfoText)
    {
        _additionalInfo.text = additionalInfoText;
    }
}
