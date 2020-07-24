using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events;
using HarmonyLib;
using UnityEngine;

namespace Scanner
{

	public class ScanMod : Plugin<Config>
	{
		private Handlers.Server server;
		private Handlers.Player player;
		public static Config config;

		public override PluginPriority Priority { get; } = PluginPriority.Medium;

		public Harmony Harmony { get; private set; }

		public override void OnEnabled()
		{
			
			base.OnEnabled();

			Patch(); 

			config = Config;

			RegisterEvents();
		}

		public override void OnDisabled()
		{
			base.OnDisabled();

			foreach (GameObject gameObject in PlayerManager.players)
			{
				UnityEngine.Object.Destroy(gameObject.GetComponent<PlayerComponents.VirusController>());
			}

			Unpatch();

			UnregisterEvents();
		}

		private void RegisterEvents()
		{
			server = new Handlers.Server();
			player = new Handlers.Player();

			Exiled.Events.Handlers.Server.WaitingForPlayers += server.OnWaiting;
			Exiled.Events.Handlers.Server.SendingConsoleCommand += server.OnConsoleCommand;
			Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += server.OnCommand;
			Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += server.OnCommandExtented;

			Exiled.Events.Handlers.Scp106.CreatingPortal += player.OnCreatePortal;

			Exiled.Events.Handlers.Player.Shooting += player.OnShooting;
			Exiled.Events.Handlers.Player.Died += player.OnPlayerDie;
			Exiled.Events.Handlers.Player.Hurting += player.OnPlayerHurt;
			Exiled.Events.Handlers.Player.ItemDropped += player.OnItemDrop;
			Exiled.Events.Handlers.Player.PickingUpItem += player.OnItemPickup;
			Exiled.Events.Handlers.Player.Spawning += player.OnPlayerSpawn;
			Exiled.Events.Handlers.Player.EscapingPocketDimension += player.OnDimEscape;
			Exiled.Events.Handlers.Player.FailingEscapePocketDimension += player.OnDimDeath;
			Exiled.Events.Handlers.Player.TriggeringTesla += player.OnTriggerTesla;

		}

		private void UnregisterEvents()
		{
			Exiled.Events.Handlers.Server.WaitingForPlayers -= server.OnWaiting;
			Exiled.Events.Handlers.Server.SendingConsoleCommand -= server.OnConsoleCommand;
			Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= server.OnCommand;
			Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= server.OnCommandExtented;

			Exiled.Events.Handlers.Scp106.CreatingPortal -= player.OnCreatePortal;

			Exiled.Events.Handlers.Player.Shooting -= player.OnShooting;
			Exiled.Events.Handlers.Player.Died -= player.OnPlayerDie;
			Exiled.Events.Handlers.Player.Hurting -= player.OnPlayerHurt;
			Exiled.Events.Handlers.Player.ItemDropped -= player.OnItemDrop;
			Exiled.Events.Handlers.Player.PickingUpItem -= player.OnItemPickup;
			Exiled.Events.Handlers.Player.Spawning -= player.OnPlayerSpawn;
			Exiled.Events.Handlers.Player.EscapingPocketDimension -= player.OnDimEscape;
			Exiled.Events.Handlers.Player.FailingEscapePocketDimension -= player.OnDimDeath;
			Exiled.Events.Handlers.Player.TriggeringTesla -= player.OnTriggerTesla;

			server = null;
			player = null;
		}

		public void Patch()
		{
			Harmony = new Harmony("uncledrema.fullrp");

			Harmony.PatchAll();
		}

		public void Unpatch()
		{

			Harmony.UnpatchAll();

		}
	}
}
