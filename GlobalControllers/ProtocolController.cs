using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MEC;
using Exiled.API.Features;
using System.Reflection;

namespace Scanner
{
	public class ProtocolController
	{
		private static Door GateA, GateB, CheckpointEZ, CheckpointA, CheckpointB;
		private static List<Door> allDoors;
		private static AlphaWarheadController alpha;
		private static AlphaWarheadNukesitePanel alphaPanel;

		public static bool LCZDecontWasActivated { get; private set; } = false;
		public static bool HCZDecontWasActivated { get; private set; } = false;
		public static bool AlphaActivated { get; private set; } = false;
		public static bool TeslaDeactivated { get; private set; } = false;

		private static bool LCZDecontOnline = false;
		private static bool HCZDecontOnline = false;
		private static bool LCZDecontLockdown = false;
		private static bool HCZDecontLockdown = false;

		public static ProtocolController INSTANCE;
		
		public ProtocolController()
		{
			allDoors = Map.Doors.ToList(); ;
			foreach (Door door in allDoors)
			{
				switch (door.DoorName)
				{
					case ("GATE_A"):
						GateA = door;
						break;
					case ("GATE_B"):
						GateB = door;
						break;
					case ("CHECKPOINT_ENT"):
						CheckpointEZ = door;
						break;
					case ("CHECKPOINT_LCZ_A"):
						CheckpointA = door;
						break;
					case ("CHECKPOINT_LCZ_B"):
						CheckpointB = door;
						break;
				}
			}
			alpha = AlphaWarheadController.Host;
			alphaPanel = UnityEngine.Object.FindObjectOfType<AlphaWarheadNukesitePanel>();
		}
		public void LczAndHczDecont()
		{
			HCZDecontOnline = true;
			LCZDecontOnline = true;
			Cassie.Message("P B 4 Protocol has been activated . activating light and heavy zone decontamination . 4 minutes remaining", false, false);

			LCZDecontWasActivated = true;
			HCZDecontWasActivated = true;

			Cassie.DelayedMessage("attention . 3 minutes remaining to light and heavy zone decontamination . allremaining", 60f, false, false);

			Cassie.DelayedMessage("attention . 2 minutes remaining to light and heavy zone decontamination . allremaining", 120f, false, false);

			Cassie.DelayedMessage("attention . 1 minute remaining to light and heavy zone decontamination . all checkpoints have been opend . allremaining", 180f, false, false);

			Timing.CallDelayed(180f, () =>
			{
				CheckpointA.SetStateWithSound(true);
				CheckpointB.SetStateWithSound(true);
				CheckpointEZ.SetStateWithSound(true);
				CheckpointA.Networklocked = true;
				CheckpointB.Networklocked = true;
				CheckpointEZ.Networklocked = true;
			});

			Cassie.DelayedMessage("attention . light and heavy zone decontamination started . all checkpoints have been closed . allremaining", 240f, false, false);

			Timing.CallDelayed(240f, () =>
			{
				CheckpointA.SetStateWithSound(false);
				CheckpointB.SetStateWithSound(false);
				CheckpointEZ.SetStateWithSound(false);
				HCZDecontOnline = false;
				LCZDecontOnline = true;
			});
		}

		public void LCZDecont()
		{
			LCZDecontOnline = true;
			Cassie.Message("P B 2 Protocol has been activated . activating light zone decontamination . 4 minutes remaining", false, false);

			LCZDecontWasActivated = true;

			Cassie.DelayedMessage("attention . 3 minutes remaining to light zone decontamination . allremaining", 60f, false, false);

			Cassie.DelayedMessage("attention . 2 minutes remaining to light zone decontamination . allremaining", 120f, false, false);

			Cassie.DelayedMessage("attention . 1 minute remaining to light zone decontamination . checkpoints a and b have been opend . allremaining", 180f, false, false);

			Timing.CallDelayed(180f, () =>
			{
				CheckpointA.SetStateWithSound(true);
				CheckpointB.SetStateWithSound(true);
				CheckpointA.Networklocked = true;
				CheckpointB.Networklocked = true;
			});

			Cassie.DelayedMessage("attention . light zone decontamination started . all checkpoints have been closed . allremaining", 240f, false, false);

			Timing.CallDelayed(240f, () =>
			{
				CheckpointA.SetStateWithSound(false);
				CheckpointB.SetStateWithSound(false);
				LCZDecontOnline = false;
			});
		}

		public void HCZDecont()
		{
			HCZDecontOnline = true;

			Cassie.Message("P B 3 Protocol has been activated . activating heavy zone decontamination . 4 minutes remaining", false, false);

			LCZDecontWasActivated = true;

			Cassie.DelayedMessage("attention . 3 minutes remaining to heavy zone decontamination . allremaining", 60f, false, false);

			Cassie.DelayedMessage("attention . 2 minutes remaining to heavy zone decontamination . allremaining", 120f, false, false);

			Cassie.DelayedMessage("attention . 1 minute remaining to heavy zone decontamination . entrance zone checkpoint has been opend . allremaining", 180f, false, false);

			Timing.CallDelayed(180f, () =>
			{
				CheckpointEZ.SetStateWithSound(true);
				CheckpointEZ.Networklocked = true;
			});

			Cassie.DelayedMessage("attention . light zone decontamination started . entrance zone checkpoint has been closed . allremaining", 240f, false, false);

			Timing.CallDelayed(240f, () =>
			{
				CheckpointEZ.SetStateWithSound(false);
				HCZDecontOnline = false;
			});
		}

		public bool IsDecontGoing()
		{
			return HCZDecontOnline || LCZDecontOnline;
		}
		public void NukeFacility(bool silent = false)
		{
			if (!AlphaActivated)
			{
				if (!silent)
				{
					Cassie.Message("P B 5 Protocol has been activated . activating alpha warhead . attention to all facility personnel . alpha warhead can not be deactivated", false, false);
					Timing.CallDelayed(16f, () =>
					{
						alpha.StartDetonation();
						alphaPanel.Networkenabled = true;

						Type t = alpha.GetType();
						FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

						foreach (FieldInfo fi in fields)
						{
							if (fi.Name == "_isLocked")
							{
								fi.SetValue(alpha, true);
							}
						}
					});
				}
				else
				{
					alpha.StartDetonation();
					alphaPanel.Networkenabled = true;

					Type t = alpha.GetType();
					FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

					foreach (FieldInfo fi in fields)
					{
						if (fi.Name == "_isLocked")
						{
							fi.SetValue(alpha, true);
						}
					}
				}
				AlphaActivated = true;
			}
		}

		public void StopNuke()
		{
			Type t = alpha.GetType();
			FieldInfo[] fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

			foreach (FieldInfo fi in fields)
			{
				if (fi.Name == "_isLocked")
				{
					fi.SetValue(alpha, false);
				}
			}

			alphaPanel.Networkenabled = false;

			alpha.CancelDetonation();
		}
		public void BlockAllDoors()
		{
			Cassie.Message("P L 3 Protocol has been activated . all doors have been lockdown for 45 seconds", false, false);
			foreach (Door door in allDoors)
			{
				door.SetStateWithSound(false);
				door.Networklocked = true;
			}
			Timing.CallDelayed(45f, UnBlockAllDoors);
		}
		private void UnBlockAllDoors()
		{
			foreach (Door door in allDoors)
			{
				door.Networklocked = false;
			}
		}
		public void BlockCheckpointsAndGates()
		{
			Cassie.Message("P L 2 Protocol has been activated . all gates and checkpoints have been lockdown for 2 minutes", false, false);

			GateA.SetStateWithSound(false);
			GateB.SetStateWithSound(false);
			GateA.Networklocked = true;
			GateB.Networklocked = true;

			if (!LCZDecontWasActivated)
			{
				CheckpointA.SetStateWithSound(false);
				CheckpointB.SetStateWithSound(false);
				CheckpointA.Networklocked = true;
				CheckpointB.Networklocked = true;
			}
			else
			{
				Cassie.Message("attention . checkpoints a and b can not be locked cause of light containment zone decontamination", false, false);
			}

			if (!HCZDecontWasActivated)
			{
				CheckpointEZ.SetStateWithSound(false);
				CheckpointEZ.Networklocked = true;
			}
			else
			{
				Cassie.Message("attention . entrance zone checkpoint can not be locked cause of heavy containment zone decontamination", false, false);
			}

			Timing.CallDelayed(120f, UnBlockCheckpoints);
			Timing.CallDelayed(120f, UnBlockGates);
		}
		public void BlockGates()
		{
			Log.Info("woah1");
			Cassie.Message("P L 1 Protocol has been activated . all gates have been lockdown for 2 minutes", false, false);
			Log.Info("woah2");
			Log.Info(GateA == null);
			GateA.SetStateWithSound(false);
			Log.Info("woah3");
			GateB.SetStateWithSound(false);
			Log.Info("woah4");
			GateA.Networklocked = true;
			Log.Info("woah5");
			GateB.Networklocked = true;
			Log.Info("woah6");

			Timing.CallDelayed(120f, UnBlockGates);
		}
		private void UnBlockGates()
		{
			GateA.Networklocked = false;
			GateB.Networklocked = false;
		}
		private void UnBlockCheckpoints()
		{
			if (!LCZDecontLockdown)
			{
				CheckpointA.Networklocked = false;
				CheckpointB.Networklocked = false;
			}
			if (!HCZDecontLockdown)
			{
				CheckpointEZ.Networklocked = false;
			}
		}

		public void Blackout(float duration, bool silent = false)
		{
			if (!silent)
			{
				Cassie.Message("P S 5 Protocol has been activated . all lights have been Disabled for 30 seconds", false, false);
			}
			Map.TurnOffAllLights(duration);

			TeslaDeactivated = true;
			Timing.CallDelayed(duration, () => TeslaDeactivated = false);
		}
	}
}
