using UnityEngine;
using System.Collections;

public class BloodScreen : MonoBehaviour {

    public static BloodScreen _instance;
    public static BloodScreen Instance
    {
        get { return _instance; }
    }

    private UISprite uiSprite;
    private TweenAlpha tweenAlpha;

    void Awake()
    {
        _instance = this;
        uiSprite = this.GetComponent<UISprite>();
        tweenAlpha = this.GetComponent<TweenAlpha>();
        uiSprite.enabled = false;
    }
    //屏幕显示红色受伤的颜色
    public void Show()
    {
        uiSprite.enabled = true;
        //先把动画设置到初始状态,确保是从初始状态开始的

        tweenAlpha.ResetToBeginning();
        tweenAlpha.PlayForward();
    }
}
