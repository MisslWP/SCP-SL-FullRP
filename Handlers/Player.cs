using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Scanner.PlayerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;
using UnityEngine;
using MEC;

namespace Scanner.Handlers
{
    public class Player
    {
		public void OnShooting(ShootingEventArgs ev)
		{
			//Обработка выстрела спец-патроном по 173
			if (ScanMod.config.enableRPItems)
			{
				if (ev.Target == null) return;

				Exiled.API.Features.Player target = Exiled.API.Features.Player.Get(ev.Target);
				AdditionalPlayerAbilities abilities = ev.Shooter.GameObject.GetComponentInParent<AdditionalPlayerAbilities>();
				if (abilities == null)
				{
					Log.Error("Shooter doesn't have abilities component");
					return;
				}
				if (abilities.hasChargedAmmo)
				{
					abilities.Fire173Ammo();
					AdditionalPlayerAbilities targetAbilities = target.GameObject.GetComponentInParent<AdditionalPlayerAbilities>();
					if (targetAbilities == null)
					{
						Log.Error("173 doesn't have abilities component");
						return;
					}

					if (target.Role == RoleType.Scp173)
					{
						targetAbilities.DisableBy173Ammo();

					}
				}
			}
		}

		public void OnPlayerDie(DiedEventArgs ev)
		{
			//Удаление компонента SCP096Controller при смерти
			if (ScanMod.config.enable096Mask)
			{
				UnityEngine.Object.Destroy(ev.Target.GameObject.GetComponent<SCP096Controller>());
			}
			//Превращение в зомби при смерти, снятие компонента если умер зомби
			if (ScanMod.config.enable008)
			{
				if (ev.Target.GameObject.TryGetComponent<VirusController>(out VirusController virus))
				{
					if (!virus.isZombie())
					{
						ev.Target.SetRole(RoleType.Scp0492, true);
						virus.setZombie();
						virus.setStage(7);
					}
					else
					{
						UnityEngine.Object.Destroy(virus);
					}
				}
			}
		}

		public void OnPlayerHurt(HurtingEventArgs ev)
		{
			//Поломка маски при выстреле
			if (ScanMod.config.enable096Mask)
			{
				if (ev.Target.Role == RoleType.Scp096 && ev.Target.GameObject.GetComponent<SCP096Controller>() != null && (ev.DamageType == DamageTypes.Com15 || ev.DamageType == DamageTypes.E11StandardRifle || ev.DamageType == DamageTypes.Grenade || ev.DamageType == DamageTypes.Logicer || ev.DamageType == DamageTypes.Mp7 || ev.DamageType == DamageTypes.P90 || ev.DamageType == DamageTypes.Usp))
				{
					ev.Target.GameObject.GetComponent<SCP096Controller>().SetMasked(false);
				}
				else if (ev.Target.Role == RoleType.Scp096 && (ev.DamageType == DamageTypes.Com15 || ev.DamageType == DamageTypes.E11StandardRifle || ev.DamageType == DamageTypes.Grenade || ev.DamageType == DamageTypes.Logicer || ev.DamageType == DamageTypes.Mp7 || ev.DamageType == DamageTypes.P90 || ev.DamageType == DamageTypes.Usp))
				{
					Log.Error("Error! No SCP096Controller on SCP096");
				}
			}
			//Обработка входящего по 173 урона в зависимости от того, активен ли спец-патрон
			if (ScanMod.config.enableRPItems)
			{
				if (ev.Target.Role == RoleType.Scp173)
				{
					AdditionalPlayerAbilities abilities = ev.Target.GameObject.GetComponentInParent<AdditionalPlayerAbilities>();
					if (abilities == null)
					{
						Log.Error("173 doesn't have abilities component");
						return;
					}
					if (abilities.effects.GetEffect<CustomPlayerEffects.Ensnared>().Enabled)
					{
						if (ev.DamageType == DamageTypes.Grenade)
						{
							ev.Amount *= 5;
							return;
						}
						ev.Amount *= 2;
					}
					else
					{
						if (ev.DamageType.isWeapon)
						{
							ev.Amount = 0;
						}
						else if (ev.DamageType == DamageTypes.Tesla || ev.DamageType == DamageTypes.MicroHid)
						{
							ev.Amount /= 3;
						}
						else if (ev.DamageType == DamageTypes.Grenade)
						{
							ev.Amount *= 3;
						}
					}
				}
			}
			//Игнор урона от 207 (ускорение), передача вируса при укусе
			if (ScanMod.config.enable008)
			{
				if (ev.DamageType == DamageTypes.Scp207 && ev.Target.Role == RoleType.Scp0492 && ev.Target.GameObject.GetComponent<VirusController>() != null)
				{
					ev.Target.Health += ev.Amount;
				}
				if (ev.DamageType == DamageTypes.Scp0492 && ev.Attacker.GameObject.GetComponent<VirusController>() != null)
				{
					ev.Target.Infect();
				}
			}
		}

		public void OnItemPickup(PickingUpItemEventArgs ev)
		{

			//Обработка поднятия маски для SCP-096
			if (ScanMod.config.enable096Mask)
			{
				if (ev.Pickup.ItemId == ItemType.WeaponManagerTablet && ev.Pickup.info.durability == 69)
				{
					if (ev.Player.HasMaskInInventory())
					{
						ev.Player.Broadcast(2, "У вас уже есть маска для SCP 096");
						ev.IsAllowed = false;
					}
					else
					{
						ev.Player.Broadcast(2, "Вы подобрали маску для SCP 096");
					}
				}
			}
		}

		public void OnItemDrop(ItemDroppedEventArgs ev)
		{
			//Обработка выкидывания маски для SCP-096
			if (ScanMod.config.enable096Mask)
			{
				if (ev.Pickup.info.itemId == ItemType.WeaponManagerTablet && ev.Pickup.info.durability == 69)
				{
					Vector3 pos = ev.Pickup.position;
					Collider[] colliders = Physics.OverlapSphere(pos, 5f);
					foreach (Collider coll in colliders)
					{
						if (coll.gameObject.GetComponentInParent<CharacterClassManager>() != null && coll.gameObject.GetComponentInParent<CharacterClassManager>().CurRole.roleId == RoleType.Scp096)
						{
							var scp096 = coll.gameObject.GetComponentInParent<SCP096Controller>();
							scp096.SetMasked(true);
							ev.Player.Broadcast(5, "Вы одели маску на SCP 096");
							ev.Pickup.Delete();
							return;
						}
					}
					ev.Player.Broadcast(2, "Вы выкинули маску для SCP 096");
				}
			}
		}

		public void OnPlayerSpawn(SpawningEventArgs ev)
		{
			//Добавление AdditionalPlayerAbilities к игроку
			if (ScanMod.config.enableRPItems || ScanMod.config.enableSuperCommands)
			{
				if (ev.Player.GameObject.GetComponentInParent<AdditionalPlayerAbilities>() == null)
				{
					Timing.CallDelayed(0.01f, () => ev.Player.GameObject.AddComponent<AdditionalPlayerAbilities>());
				}
				Timing.CallDelayed(0.02f, () => ev.Player.GameObject.GetComponent<AdditionalPlayerAbilities>().markDirty());
			}
			//Рандомизация размера при спавне
			if (ScanMod.config.enableRandomSize)
			{
				if (ev.Player.GameObject.GetComponentInParent<CharacterClassManager>().IsHuman())
				{
					ev.Player.GameObject.SetPlayerScale(1 + UnityEngine.Random.Range(ScanMod.config.minXOffset, ScanMod.config.maxXOffset), 1 + UnityEngine.Random.Range(ScanMod.config.minYOffset, ScanMod.config.maxYOffset), 1 + UnityEngine.Random.Range(ScanMod.config.minZOffset, ScanMod.config.maxZOffset));
				}
				else if (ev.RoleType != RoleType.Scp0492)
				{
					ev.Player.GameObject.SetPlayerScale(1, 1, 1);
				}
			}
			//Добавление SCP096Controller при спавне
			if (ScanMod.config.enable096Mask)
			{
				if (ev.RoleType == RoleType.Scp096)
				{
					if (ev.Player.GameObject.GetComponent<SCP096Controller>() == null)
					{
						Timing.CallDelayed(1f, delegate { ev.Player.GameObject.AddComponent<SCP096Controller>(); });
					}
				}
				else
				{
					UnityEngine.Object.Destroy(ev.Player.GameObject.GetComponent<SCP096Controller>());
				}
			}
		}

		public void OnDimEscape(EscapingPocketDimensionEventArgs ev)
		{
			//Реальный побег из измерения
			if (ScanMod.config.enable106overhaul)
			{
				ev.Player.MakeRealPocketEscape();
				ev.IsAllowed = false;
			}
		}

		public void OnDimDeath(FailingEscapePocketDimensionEventArgs ev)
		{
			//Фейковый побег из измерения, если активен
			if (ScanMod.config.enable106overhaul)
			{
				if (PocketProperties.cycledPocket)
				{
					ev.Player.MakeFakePocketEscape();
					ev.IsAllowed = false;
				}
			}
		}

		public void OnCreatePortal(CreatingPortalEventArgs ev)
		{
			//Добавление порталов в качестве точек выхода
			if (ScanMod.config.enable106overhaul)
			{
				if (PocketProperties.newExits.Count == ScanMod.config.additionalExitsCap)
				{
					PocketProperties.newExits.RemoveAt(0);
				}
				PocketProperties.newExits.Add(ev.Position);
			}
		}

		public void OnTriggerTesla(TriggeringTeslaEventArgs ev)
		{
			//Выключение тесла-ворот при блэкауте
			if (ScanMod.config.enableProtocols || ScanMod.config.enableSuperCommands)
			{
				if (ProtocolController.TeslaDeactivated)
				{
					ev.IsTriggerable = false;
				}
			}
		}

		public void OnUsedMedicalItem(UsedMedicalItemEventArgs ev)
		{
			//Лечение вируса при помощи SCP 500
			if (ScanMod.config.enable008)
			{
				if (ev.Item == ItemType.SCP500 && ev.Player.GameObject.TryGetComponent<VirusController>(out VirusController virus))
				{
					virus.OnCured();
				}
			}
		}
	}
}
