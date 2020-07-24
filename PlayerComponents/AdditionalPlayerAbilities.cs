using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scanner.PlayerComponents
{
    class AdditionalPlayerAbilities : MonoBehaviour
    {
		GameObject thisObject;
		Player thisPlayer;
		public PlayerEffectsController effects
		{
			get
			{
				return thisPlayer.ReferenceHub.playerEffectsController;
			}
		}

		public bool has173ammo { get; private set; }
		public bool hasChargedAmmo { get; private set; }

		public bool scp096whitelisted = false;
		public bool isHuman { 
			get
			{
				return thisPlayer.ReferenceHub.characterClassManager.IsHuman();
			}
		}

		private void Start()
		{
			thisObject = this.gameObject;
			thisPlayer = Player.Get(thisObject);
		}
		private void OnDisable()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void Update()
		{

		}

		public void Charge173Ammo()
		{
			if (has173ammo)
			{
				has173ammo = false;
				hasChargedAmmo = true;
			}
		}

		public void Fire173Ammo()
		{
			if (hasChargedAmmo)
			{
				hasChargedAmmo = false;
			}
		}

		public void DisableBy173Ammo()
		{
			if (thisPlayer.Role == RoleType.Scp173)
			{
				effects.EnableEffect<CustomPlayerEffects.Ensnared>(120f);
			}
		}

		public void markDirty()
		{
			has173ammo = (thisPlayer.Role == RoleType.NtfCommander || thisPlayer.Role == RoleType.NtfScientist);
		}
	}
}
