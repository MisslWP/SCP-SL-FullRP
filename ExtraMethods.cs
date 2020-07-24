using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Mirror;
using RemoteAdmin;
using System;
using System.Linq;
using UnityEngine;

namespace Scanner
{
	// Token: 0x02000002 RID: 2
	public static class ExtraMethods
	{
		
		private static float scanCooldown = 120f, protocolCooldown = 300f;
		private static float lastScanTime = -scanCooldown;
		private static float lastProtocolTime = -protocolCooldown;
		public static void TryScanSCP(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ScanController.INSTANCE.SCPCassie();
				return;
			}
			if (GetAccessLevel(pl, ev) < 3) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastScanTime < scanCooldown)
			{
				ev.ReturnMessage = "Протоколы сканирования были недавно активированы, в данный момент активация невозможна";
				pl.Broadcast(5,"Протоколы сканирования были недавно активированы, в данный момент активация невозможна");
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastScanTime = Time.time;
				ScanController.INSTANCE.SCPCassie();
			}
		}
		public static void TryScanPersonnel(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ScanController.INSTANCE.PersonnelCassie();
				return;
			}
			if (GetAccessLevel(pl, ev) < 3) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastScanTime < scanCooldown)
			{
				ev.ReturnMessage = "Протоколы сканирования были недавно активированы, в данный момент активация невозможна";
				pl.Broadcast(5, "Протоколы сканирования были недавно активированы, в данный момент активация невозможна");
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastScanTime = Time.time;
				ScanController.INSTANCE.PersonnelCassie();
			}
		}
		public static void TryBlockGates(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				Log.Info("hmm ev == null");
				ProtocolController.INSTANCE.BlockGates();
				return;
			}
			if (GetAccessLevel(pl, ev) < 1) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastProtocolTime = Time.time;
				ProtocolController.INSTANCE.BlockGates();
			}

		}
		public static void TryBlockCheckpointAndGates(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.BlockCheckpointsAndGates();
				return;
			}
			if (GetAccessLevel(pl, ev) < 2) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastProtocolTime = Time.time;
				ProtocolController.INSTANCE.BlockCheckpointsAndGates();
			}

		}
		public static void TryBlockDoors(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.BlockAllDoors();
				return;
			}
			if (GetAccessLevel(pl, ev) < 4) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastProtocolTime = Time.time;
				ProtocolController.INSTANCE.BlockAllDoors();
			}

		}
		public static void TryLCZAndHCZDecontain(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.LczAndHczDecont();
				return;
			}
			if (GetAccessLevel(pl,ev) < 5) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				if (!ProtocolController.INSTANCE.IsDecontGoing())
				{
					ev.ReturnMessage = "Протокол успешно активирован";
					ProtocolController.INSTANCE.LczAndHczDecont();
					lastProtocolTime = Time.time;
				}
				else if (ProtocolController.HCZDecontWasActivated || ProtocolController.LCZDecontWasActivated)
				{
					ev.ReturnMessage = "Уже была запущена деконтаминация лёгкой или тяжёлой зоны, так что воспользуйтесь другим протоколом";
				}
				else
				{
					ev.ReturnMessage = "В данный момент уже происходит деконтаминация одной из Зон содержания";
				}
			}
		}
		public static void TryLCZDecontain(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.LCZDecont();
				return;
			}
			if (GetAccessLevel(pl, ev) < 4) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				
				if (!ProtocolController.INSTANCE.IsDecontGoing())
				{
					ev.ReturnMessage = "Протокол успешно активирован";
					lastProtocolTime = Time.time;
					ProtocolController.INSTANCE.LCZDecont();
				}
				else if (ProtocolController.LCZDecontWasActivated)
				{
					ev.ReturnMessage = "Уже была запущена деконтаминация лёгкой зоны, так что воспользуйтесь другим протоколом";
				}
				else
				{
					ev.ReturnMessage = "В данный момент уже происходит деконтаминация одной из Зон содержания";
				}
			}
		}
		public static void TryHCZDecontain(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.HCZDecont();
				return;
			}
			if (GetAccessLevel(pl, ev) < 4) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				if (!ProtocolController.INSTANCE.IsDecontGoing())
				{
					ev.ReturnMessage = "Протокол успешно активирован";
					lastProtocolTime = Time.time;
					ProtocolController.INSTANCE.HCZDecont();
				}
				else if (ProtocolController.HCZDecontWasActivated)
				{
					ev.ReturnMessage = "Уже была запущена деконтаминация тяжёлой зоны, так что воспользуйтесь другим протоколом";
				}
				else
				{
					ev.ReturnMessage = "В данный момент уже происходит деконтаминация одной из Зон содержания";
				}
			}
		}
		public static void TryNuke(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.NukeFacility();
				return;
			}
			if (GetAccessLevel(pl,ev) < 6) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else if (ProtocolController.AlphaActivated)
			{
				ev.ReturnMessage = "Данный протокол уже был активирован";
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastProtocolTime = Time.time;
				ProtocolController.INSTANCE.NukeFacility();
			}
		}
		public static void TryBlackout(Player pl, float duration, SendingConsoleCommandEventArgs ev = null)
		{
			if (ev == null)
			{
				ProtocolController.INSTANCE.Blackout(duration);
				return;
			}
			if (GetAccessLevel(pl, ev) < 4) { if (GetAccessLevel(pl) != -1) pl.Broadcast(5, "Вашего уровня допуска недостаточно для активации данного протокола"); ev.ReturnMessage = "Вашего уровня допуска недостаточно для активации данного протокола"; return; }
			if (Time.time - lastProtocolTime < protocolCooldown)
			{
				ev.ReturnMessage = "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд";
				pl.Broadcast(5, "Один из протоколов был недавно активирован, активация следующего протокола возможна через " + (protocolCooldown - Time.time + lastProtocolTime) + " секунд");
			}
			else
			{
				ev.ReturnMessage = "Протокол успешно активирован";
				lastProtocolTime = Time.time;
				ProtocolController.INSTANCE.Blackout(duration);
			}
		}
		private static int GetAccessLevel(Player pl, SendingConsoleCommandEventArgs ev = null)
		{
			if (pl.CurrentItem.uniq != 0 && pl.CurrentItem.id.IsKeycard())
			{
				if (pl.CurrentItem.id == ItemType.KeycardJanitor || pl.CurrentItem.id == ItemType.KeycardChaosInsurgency || pl.CurrentItem.id == ItemType.KeycardZoneManager || pl.CurrentItem.id == ItemType.KeycardScientist || pl.CurrentItem.id == ItemType.KeycardScientistMajor)
				{
					return 0;
				}
				else if (pl.CurrentItem.id == ItemType.KeycardSeniorGuard || pl.CurrentItem.id == ItemType.KeycardGuard)
				{
					return 1;
				}
				else if (pl.CurrentItem.id == ItemType.KeycardNTFLieutenant)
				{
					return 2;
				}
				else if (pl.CurrentItem.id == ItemType.KeycardContainmentEngineer)
				{
					return 3;
				}
				else if (pl.CurrentItem.id == ItemType.KeycardNTFCommander)
				{
					return 4;
				}
				else if (pl.CurrentItem.id == ItemType.KeycardFacilityManager)
				{
					return 5;
				}
				else if (pl.CurrentItem.id == ItemType.KeycardO5)
				{
					return 6;
				}
			}
			if (ev != null)
			{
				ev.ReturnMessage = "Возьмите ключ-карту в руки чтобы активировать протокол";
			}
			pl.Broadcast(5, "Возьмите ключ-карту в руки чтобы активировать протокол");
			return -1;
		}

		public static void SetPocketExits(int amount)
		{
			
			for (int i = 0; i < PocketProperties.teleports.Length; i++)
			{
				PocketProperties.teleports[i].SetType(PocketDimensionTeleport.PDTeleportType.Killer);
			}
			int num = 0;
			while (num < amount && ContainsKiller(PocketProperties.teleports))
			{
				int num2 = -1;
				while ((num2 < 0 || PocketProperties.teleports[num2].GetTeleportType() == PocketDimensionTeleport.PDTeleportType.Exit) && ContainsKiller(PocketProperties.teleports))
				{
					num2 = _random.Next(0, PocketProperties.teleports.Length);
				}
				PocketProperties.teleports[Mathf.Clamp(num2, 0, PocketProperties.teleports.Length - 1)].SetType(PocketDimensionTeleport.PDTeleportType.Exit);
				num++;
			}
		}
		private static bool ContainsKiller(PocketDimensionTeleport[] pdtps)
		{
			for (int i = 0; i < pdtps.Length; i++)
			{
				if (pdtps[i].GetTeleportType() == PocketDimensionTeleport.PDTeleportType.Killer)
				{
					return true;
				}
			}
			return false;
		}

		private static System.Random _random = new System.Random();

	}
}
