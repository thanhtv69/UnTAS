using UnityEngine;
using MelonLoader;
using UnTAS;
using System.Net;

[assembly: MelonInfo(typeof(Mod), "UnTAS", "0.0.2", "EnderEye")]

namespace UnTAS
{
    public class Mod : MelonMod
    {
        public static float prevTimeScale = 1f;
        public static float baseTimeScale = 1.3f;
        public static bool frozen = false;

        public static KeyCode SlowKey = KeyCode.F1;
        public static KeyCode FastKey = KeyCode.F2;
        public static KeyCode PauseKey = KeyCode.R;
        public static KeyCode ResetKey = KeyCode.M;

        public static void DrawFrozenText()
        {
            GUI.Label(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 150, 1000, 200), "<b><color=white><size=100>Frozen</size></color></b>");
        }
        
        public static void DrawWatermark()
        {
            //GUI.Label(new Rect((Screen.width / 2) - 120, Screen.height - 60, 1000, 200), "<b><color=red><size=20>Speed:" + baseTimeScale + "</size></color></b>");
            GUI.Label(new Rect(40, Screen.height - 60, 1000, 200), "<b><color=red><size=20>Speed:" + baseTimeScale + "</size></color></b>");
        }

        public override void OnInitializeMelon()
        {
            MelonEvents.OnGUI.Subscribe(DrawWatermark, 101);
        }

        public override void OnUpdate()
        {
            if(!frozen)
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
            }

            if(Input.GetKeyDown(PauseKey))
            {
                ToggleFreeze();
            }

            if(Input.GetKeyDown(ResetKey))
            {
                baseTimeScale = 1f;
                MelonLogger.Msg("Speed: " + baseTimeScale);
            }

            Time.timeScale = baseTimeScale;
        }

        private static void ToggleFreeze()
        {
            frozen = !frozen;

            if (frozen)
            {
                MelonEvents.OnGUI.Subscribe(DrawFrozenText, 100);
                prevTimeScale = baseTimeScale;
                baseTimeScale = 0f;
            }
            else
            {
                MelonEvents.OnGUI.Unsubscribe(DrawFrozenText);
                baseTimeScale = prevTimeScale;
            }
        }

        public override void OnDeinitializeMelon()
        {
            if (frozen)
            {
                ToggleFreeze();
            }
        }
    }
}
