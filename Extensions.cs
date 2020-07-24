using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using Mirror;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scanner
{
    public static class Extensions
    {
		public static void SetPlayerScale(this GameObject target, float x, float y, float z)
		{
			try
			{
				NetworkIdentity component = target.GetComponent<NetworkIdentity>();
				target.transform.localScale = new Vector3(1f * x, 1f * y, 1f * z);
				ObjectDestroyMessage objectDestroyMessage = default(ObjectDestroyMessage);
				objectDestroyMessage.netId = component.netId;
				foreach (GameObject gameObject in PlayerManager.players)
				{
					NetworkConnection connectionToClient = gameObject.GetComponent<NetworkIdentity>().connectionToClient;
					bool flag = gameObject != target;
					if (flag)
					{
						connectionToClient.Send<ObjectDestroyMessage>(objectDestroyMessage, 0);
					}
					object[] param = new object[]
					{
						component,
						connectionToClient
					};
					typeof(NetworkServer).InvokeStaticMethod("SendSpawnMessage", param);
				}
			}
			catch (Exception arg)
			{
				Log.Info(string.Format("Set Scale error: {0}", arg));
			}
		}
		public static bool HasMaskInInventory(this Player pl)
		{
			foreach (Inventory.SyncItemInfo item in pl.Inventory.items)
			{
				if (item.id == ItemType.WeaponManagerTablet && item.durability == 69) return true;
			}
			return false;
		}

		public static void MakeFakePocketEscape(this Player pl)
		{
			List<Vector3> tpPositions = new List<Vector3>();
			foreach (GameObject gameObject2 in GameObject.FindGameObjectsWithTag("PD_EXIT"))
			{
				tpPositions.Add(gameObject2.transform.position);
			}
			foreach(Vector3 pos in PocketProperties.newExits)
			{
				tpPositions.Add(pos + new Vector3(0, 1, 0));
			}
			
			Vector3 newPos = tpPositions[UnityEngine.Random.Range(0, tpPositions.Count)];
			Timing.CallDelayed(0.01f, () => pl.Position = newPos );

			Timing.CallDelayed(UnityEngine.Random.Range(2f, 20f), () => pl.CatchInPocket());
		}

		public static void MakeRealPocketEscape(this Player pl)
		{
			List<Vector3> tpPositions = new List<Vector3>();

			foreach (GameObject gameObject2 in GameObject.FindGameObjectsWithTag("PD_EXIT"))
			{
				tpPositions.Add(gameObject2.transform.position);
			}
			foreach (Vector3 pos in PocketProperties.newExits)
			{
				tpPositions.Add(pos + new Vector3(0,1,0));
			}

			Vector3 newPos = tpPositions[UnityEngine.Random.Range(0, tpPositions.Count)];
			Timing.CallDelayed(0.01f, () => pl.Position = newPos);
		}

		public static void CatchInPocket(this Player pl)
		{
			pl.Position = PocketProperties.pocketPos;
		}

		/*
		 *	0 - ничего особенного
		 *	1 - нижняя часть Интеркома
		 *	2 - кафетерий с ПК в ЛКЗ
		 *	3 - в серверной
		 *	4 - у панели Касси на улице
		 * */
		public static int inWhatSpecialRoom(this Player player)
		{
			Room room = player.CurrentRoom;
			if (room != null)
			{
				if (room.Name == "EZ_Intercom")
				{
					return 1;
				}
				else if (room.Name == "LCZ_Cafe (15)")
				{
					return 2;
				}
				else if (room.Name == "HCZ_Servers")
				{
					return 3;
				}
				else
				{
					if (player.Position.IsInRange(new Vector3(173, 980, 23), new Vector3(190, 1000, 38)))
					{
						return 4;
					}
				}
			}


			return 0;
		}
		public static bool IsInRange(this Vector3 thisPos, Vector3 firstPoint, Vector3 secondPoint)
		{
			return (thisPos.x > firstPoint.x && thisPos.x < secondPoint.x) && (thisPos.y > firstPoint.y && thisPos.y < secondPoint.y) && (thisPos.z > firstPoint.z && thisPos.z < secondPoint.z);
		}

		public static void SpawnItem(this ItemType item, Vector3 position, Vector3 rotation)
		{
			PlayerManager.localPlayer.GetComponent<Inventory>().SetPickup(item, -4.656647E+11f, position, Quaternion.Euler(rotation), 0, 0, 0);
		}

		public static RagdollManager manager;

		public static void SpawnRagdoll(this RoleType role, Vector3 position, Quaternion rotation, string nick, DamageTypes.DamageType damageType)
		{
			manager.SpawnRagdoll(position, rotation, Vector3.zero, (int)role, new PlayerStats.HitInfo(1000f, "", damageType, 0), false, "hubert@northwood", nick, 0);
		}

		public static void Infect(this Player player)
		{
			var component = player.GameObject.GetComponent<PlayerComponents.VirusController>();

			if (component != null) return;
			player.GameObject.AddComponent<PlayerComponents.VirusController>();
		}

	}
}
