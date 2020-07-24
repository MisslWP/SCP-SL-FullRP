using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scanner.PlayerComponents
{
	public class VirusController : MonoBehaviour
	{
		private float _nextCycle = 0f;
		private float zombieTime;
		private float infectionTime;
		private bool zombie = false;
		public int _stage;

		public static DamageTypes.DamageType Virus = new DamageTypes.DamageType("Virus");

		private ReferenceHub thisHub;

		private PlayerEffectsController thisEffects;

		private Player thisPlayer;
		private void Start()
		{
			thisPlayer = Player.Get(this.gameObject);
			thisHub = thisPlayer.ReferenceHub;
			thisEffects = thisHub.GetComponentInParent<PlayerEffectsController>();
			_stage = 0;
			infectionTime = Time.time;
			Log.Info(infectionTime);
		}
		public void OnInfected()
		{

		}
		public void OnCured()
		{
			thisEffects.DisableEffect<CustomPlayerEffects.Concussed>();
			thisEffects.DisableEffect<CustomPlayerEffects.Deafened>();
			thisEffects.DisableEffect<CustomPlayerEffects.Blinded>();
			thisEffects.DisableEffect<CustomPlayerEffects.Exhausted>();
			thisEffects.DisableEffect<CustomPlayerEffects.Panic>();
			thisEffects.DisableEffect<CustomPlayerEffects.Ensnared>();
			thisEffects.DisableEffect<CustomPlayerEffects.Disabled>();
			thisPlayer.Broadcast(5, "Вы излечились от SCP-008");
			UnityEngine.Object.Destroy(this);

		}
		// Token: 0x06000006 RID: 6 RVA: 0x0000212D File Offset: 0x0000032D
		private void OnDisable()
		{
		}

		public int GetStage()
		{
			return _stage;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		private void Update()
		{
			if (_stage == 0)
			{
				incStage();
				thisEffects.EnableEffect<CustomPlayerEffects.Panic>(180);
			}
			else if (infectionTime - Time.time > 30 && _stage == 1)
			{
				incStage();
				thisPlayer.Broadcast(5, "Вы чувсвтвуете слабость");
				thisEffects.EnableEffect<CustomPlayerEffects.Exhausted>(150);

			}
			else if (infectionTime - Time.time > 60 && _stage == 2)
			{
				incStage();
				thisPlayer.Broadcast(5, "У вас кружится голова");
				thisEffects.EnableEffect<CustomPlayerEffects.Concussed>(120);

			}

			else if (infectionTime - Time.time > 90 && _stage == 3)
			{
				incStage();
				thisPlayer.Broadcast(5, "Как же плохо...");
				thisEffects.EnableEffect<CustomPlayerEffects.Disabled>(90);

			}
			else if (infectionTime - Time.time > 120 && _stage == 4)
			{
				incStage();
				thisPlayer.Broadcast(5, "Мир вокруг вас затихает...");
				thisEffects.EnableEffect<CustomPlayerEffects.Deafened>(60);
			}
			else if (infectionTime - Time.time > 150 && _stage == 5)
			{
				incStage();
				thisPlayer.Broadcast(5, "Мир вокруг вас исчезает...");
				thisEffects.EnableEffect<CustomPlayerEffects.Blinded>(30);
			}
			else if (infectionTime - Time.time > 170 && _stage == 6)
			{
				incStage();
				thisEffects.EnableEffect<CustomPlayerEffects.Ensnared>(15);
				thisPlayer.Broadcast(5, "Вы не в силах сопротивляться вирусу");
			}
			else if ((_stage == 7 && zombie) || (infectionTime - Time.time > 180 && _stage == 7))
			{
				incStage();
				if (!zombie)
				{
					thisPlayer.SetRole(RoleType.Scp0492, true);
					setZombie();
				}
				thisPlayer.Broadcast(5, "Вы больше не человек");
				thisPlayer.MaxHealth = 300;
				thisPlayer.Health = 300;
				zombieTime = Time.time;
			}
			else if (Time.time - zombieTime > 60 && _stage == 8 && thisPlayer.Role == RoleType.Scp0492)
			{
				incStage();
				thisPlayer.Broadcast(5, "Вы становитесь сильнее");
				thisPlayer.MaxHealth += 300;
				thisPlayer.Health += 300;
			}
			else if (Time.time - zombieTime > 120 && _stage == 9 && thisPlayer.Role == RoleType.Scp0492)
			{
				incStage();
				thisPlayer.Broadcast(5, "Вы становитесь сильнее");
				thisPlayer.MaxHealth += 300;
				thisPlayer.Health += 300;
			}
			else if (Time.time - zombieTime > 180 && _stage == 10 && thisPlayer.Role == RoleType.Scp0492)
			{
				incStage();
				thisPlayer.Broadcast(5, "Вы становитесь сильнее");
				thisEffects.EnableEffect<CustomPlayerEffects.Scp207>(120);
				thisPlayer.MaxHealth += 300;
				thisPlayer.Health += 300;
			}
			else if (Time.time - zombieTime > 240 && _stage == 11 && thisPlayer.Role == RoleType.Scp0492)
			{
				incStage();
				thisPlayer.Broadcast(5, "Вы становитесь сильнее");
				thisEffects.EnableEffect<CustomPlayerEffects.Visuals939>(60);
				thisPlayer.MaxHealth += 300;
				thisPlayer.Health += 300;
			}
			else if (Time.time - zombieTime > 285 && _stage == 12 && thisPlayer.Role == RoleType.Scp0492)
			{
				incStage();
				thisPlayer.Broadcast(5, "Ваша плоть начала разлагаться");
				thisEffects.EnableEffect<CustomPlayerEffects.Bleeding>(45);
				thisPlayer.MaxHealth += 300;
				thisPlayer.Health += 300;
			}
			else if (Time.time - zombieTime > 300 && _stage == 13 && thisPlayer.Role == RoleType.Scp0492)
			{
				incStage();
				thisPlayer.Broadcast(5, "Вы умираете от разложения");
				thisEffects.EnableEffect<CustomPlayerEffects.Disabled>(30);
				_nextCycle = Time.time;
			}
			else if (_stage == 14 && thisPlayer.Role == RoleType.Scp0492)
			{
				if (Time.time < _nextCycle) return;
				_nextCycle += 0.5f;
				thisHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(0.1f * thisPlayer.MaxHealth, "Rotting from Virus", VirusController.Virus, -1), thisHub.gameObject);
			}
		}
		public void incStage()
		{
			_stage++;
		}
		public void setStage(int num)
		{
			_stage = num;
		}
		public void setZombie()
		{
			zombie = true;

		}
		public bool isZombie()
		{
			return zombie;
		}
		
	}
}
