﻿using DxLibDLL;

namespace MyLib
{
    // 入力クラス
    public static class Input
    {
        static int prevState; // 1フレーム前の状態
        static int currentState; // 現在の状態

        // 初期化。最初に1回だけ呼んでください。
        public static void Init()
        {
            prevState = 0;
            currentState = 0;
        }

        // 最新の入力状況に更新する処理。
        // 毎フレームの最初に（ゲームの処理より先に）呼んでください。
        public static void Update()
        {
            prevState = currentState;
            currentState = DX.GetJoypadInputState(DX.DX_INPUT_KEY_PAD1);
        }

        // ボタンが押されているか？
        public static bool GetButton(int buttonId)
        {
            // 今ボタンが押されているかどうかを返却
            return (currentState & buttonId) != 0;
        }

        // ボタンが押された瞬間か？
        public static bool GetButtonDown(int buttonId)
        {
            // 今は押されていて、かつ1フレーム前は押されていない場合はtrueを返却
            return ((currentState & buttonId) & ~(prevState & buttonId)) != 0;
        }

        // ボタンが離された瞬間か？
        public static bool GetButtonUp(int buttonId)
        {
            // 1フレーム前は押されていて、かつ今は押されている場合はtrueを返却
            return ((prevState & buttonId) & ~(currentState & buttonId)) != 0;
        }
    }
}