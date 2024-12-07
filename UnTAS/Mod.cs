using UnityEngine;
using MelonLoader;
using UnTAS;

[assembly: MelonInfo(typeof(Mod), "UnTAS", "0.0.2", "EnderEye")]

namespace UnTAS
{
    public class Mod : MelonMod
    {
        public static int n = 3; // Số bước để quay lại giá trị ban đầu
        public static float step = 1f / n; // Mỗi bước tăng giảm
        public static float baseTimeScale = 1f + step; // Tốc độ ban đầu

        public static KeyCode SlowKey = KeyCode.N; // Giảm tốc (tăng n)
        public static KeyCode FastKey = KeyCode.M; // Tăng tốc (giảm n)
        public static KeyCode ResetKey = KeyCode.R; // Reset tốc độ và n

        public static void DrawWatermark()
        {
            GUI.Label(new Rect(40, Screen.height - 60, 1000, 200),
                $"<b><color=red><size=20>Speed: {baseTimeScale:F2}, Steps: {n}</size></color></b>");
        }

        public override void OnInitializeMelon()
        {
            MelonEvents.OnGUI.Subscribe(DrawWatermark, 101);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(SlowKey))
            {
                n++; // Tăng số bước để giảm tốc độ thay đổi
                UpdateStep();
                MelonLogger.Msg($"Increased steps to {n}, Speed: {baseTimeScale:F2}");
            }

            if (Input.GetKeyDown(FastKey))
            {
                if (n > 1) // Đảm bảo n không nhỏ hơn 1
                {
                    n--; // Giảm số bước để tăng tốc độ thay đổi
                    UpdateStep();
                    MelonLogger.Msg($"Decreased steps to {n}, Speed: {baseTimeScale:F2}");
                }
            }

            if (Input.GetKeyDown(ResetKey))
            {
                n = 3; // Reset n về giá trị mặc định
                UpdateStep();
                baseTimeScale = 1f + step; // Reset tốc độ
                MelonLogger.Msg($"Reset steps to {n}, Speed: {baseTimeScale:F2}");
            }

            Time.timeScale = baseTimeScale;
        }

        private void UpdateStep()
        {
            step = 1f / n; // Cập nhật giá trị step
            baseTimeScale = 1f + step; // Cập nhật tốc độ
        }

        public override void OnDeinitializeMelon()
        {
            // Không cần xử lý đóng băng, đã xoá logic liên quan
        }
    }
}
