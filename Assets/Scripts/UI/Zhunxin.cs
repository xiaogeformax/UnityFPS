using UnityEngine;
using System.Collections;

public class Zhunxin : MonoBehaviour
{

    public Texture2D texture;

    public int size = 20;
    void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2 - size * 0.5f, Screen.height / 2 - size * 0.5f, size, size);

        GUI.DrawTexture(rect, texture);
    }
}
