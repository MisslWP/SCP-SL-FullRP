using HarmonyLib;
using Mirror;
using PlayableScps;
using PlayableScps.Messages;
using Scanner.PlayerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoScanning.Patches
{
    [HarmonyPatch(typeof(PlayableScps.Scp096), nameof(PlayableScps.Scp096.ParseVisionInformation))]
    class ProjectEncoder
    {
        public static bool Prefix(PlayableScps.Scp096 __instance, VisionInformation info)
        {
			PlayableScpsController playableScpsController;
			bool whitelisted = info.Source.GetComponent<AdditionalPlayerAbilities>() != null && info.Source.GetComponent<AdditionalPlayerAbilities>().scp096whitelisted;
			Exiled.API.Features.Log.Info("Патчим агр скромника, результат: " + whitelisted);
			if (!info.Looking || !info.RaycastHit || !info.RaycastResult.transform.gameObject.TryGetComponent<PlayableScpsController>(out playableScpsController) || playableScpsController.CurrentScp == null || playableScpsController.CurrentScp != __instance || whitelisted)
			{
				return false;
			}
			float delay = (1f - info.DotProduct) / 0.25f * (Vector3.Distance(info.Source.transform.position, info.Target.transform.position) * 0.1f);
			if (!__instance.Calming)
			{
				__instance.AddTarget(info.Source);
			}
			if (__instance.CanEnrage && info.Source != null)
			{
				__instance.PreWindup(delay);
				if (NetworkServer.active)
				{
					NetworkServer.SendToAll<Scp096TriggerMessage>(new Scp096TriggerMessage(info.Target, info.Source), 0);
				}
			}

			return false;
        }
    }
}
