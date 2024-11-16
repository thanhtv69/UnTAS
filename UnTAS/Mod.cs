using UnityEngine;
using MelonLoader;
using UnTAS;
using System.Net;

[assembly: MelonInfo(typeof(Mod), "UnTAS", "0.0.2", "EnderEye")]

namespace UnTAS
{
    public class Mod : MelonMod
    {
        public static float baseTimeScale = 1f + 1f / 3f;

        public static KeyCode SlowKey = KeyCode.N; // Giảm tốc
        public static KeyCode FastKey = KeyCode.M; // Tăng tốc
        public static KeyCode ResetKey = KeyCode.R; // Reset tốc độ

        public static void DrawWatermark()
        {
            GUI.Label(new Rect(40, Screen.height - 60, 1000, 200), "<b><color=red><size=20>Speed:" + baseTimeScale + "</size></color></b>");
        }

        public override void OnInitializeMelon()
        {
            MelonEvents.OnGUI.Subscribe(DrawWatermark, 101);
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(SlowKey))
            {
                if (Time.timeScale > 0.1f)
                {
                    baseTimeScale -= 0.1f;
                    MelonLogger.Msg("Speed: " + baseTimeScale);
                }
            }

            if (Input.GetKeyDown(FastKey))
            {
                baseTimeScale += 0.1f;
                MelonLogger.Msg("Speed: " + baseTimeScale);
            }

            if (Input.GetKeyDown(ResetKey))
            {
                baseTimeScale = 1f + 1f / 3f;
                MelonLogger.Msg("Speed: " + baseTimeScale);
            }

            Time.timeScale = baseTimeScale;
        }

        public override void OnDeinitializeMelon()
        {
            // Không cần xử lý đóng băng, đã xoá logic liên quan
        }
    }
}
