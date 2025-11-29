using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Sprite pushedButtonSprite;

    private Image buttonImage;  //ボタンのイメージ
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //もしキャンセルボタンが押されたら
        if (Input.GetButton("Cancel"))
        {
            //アニメーションを再生
            PlaySwitchImageAnimation();
        }
    }

    //ボタンの切り替えアニメーション
    public void PlaySwitchImageAnimation()
    {
        animator.Play("SwitchImage_bt_b");
    }

    //ボタンの画像切り替え
    public void SwitchButtonImage()
    {
        buttonImage.sprite = pushedButtonSprite;
    }
}
