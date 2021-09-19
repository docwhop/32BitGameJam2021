using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickAudio;

    public void OnClick()
    {   
        AudioManager.Instance.PlayUi(clickAudio);
    }
}
