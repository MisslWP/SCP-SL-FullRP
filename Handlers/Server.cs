using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Scanner.PlayerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scanner.Handlers
{
    internal class Server
    {

		public void OnWaiting()
		{
			//Спавн 096 в своей камере
			if (ScanMod.config.enableSmallFeatures)
			{
				float rot096 = 0;
				foreach (Room room in Map.Rooms)
				{
					if (room.Name == "HCZ_457")
					{
						rot096 = room.Transform.rotation.eulerAngles.y;
						break;
					}
				}
				SpawnpointManager sm = UnityEngine.Object.FindObjectOfType<SpawnpointManager>();
				GameObject pos2 = sm.GetRandomPosition(RoleType.Scp096);
				if (rot096 == 0)
				{
					pos2.transform.position += new Vector3(4f, 0f, 0f);
				}
				else if (rot096 == 90)
				{
					pos2.transform.position -= new Vector3(0f, 0f, 4f);
				}
				else if (rot096 == 180)
				{
					pos2.transform.position -= new Vector3(4f, 0f, 0f);
				}
				else if (rot096 == 270)
				{
					pos2.transform.position += new Vector3(0f, 0f, 4f);
				}
			}
			//Инициализация для переработки 106
			if (ScanMod.config.enable106overhaul)
			{
				PocketProperties.teleports = UnityEngine.Object.FindObjectsOfType<PocketDimensionTeleport>();
			}
			//Появление новых предметов на карте
			if (ScanMod.config.enableNewItemSpawns)
			{
				Extensions.manager = UnityEngine.Object.FindObjectOfType<RagdollManager>();
				int modX, modZ;
				bool swap;

				foreach (Room room in Map.Rooms)
				{
					modX = 0;
					modZ = 0;
					swap = false;

					float angle = room.Transform.rotation.eulerAngles.y;
					if (angle < 10f && angle > -10f)
					{
						modX = 1;
						modZ = 1;
						swap = false;
					}
					else if (angle < 100f && angle > 80f)
					{
						modX = 1;
						modZ = -1;
						swap = true;
					}
					else if (angle < 190f && angle > 170f)
					{
						modX = -1;
						modZ = -1;
						swap = false;
					}
					else if (angle < 280f && angle > 260f)
					{
						modX = -1;
						modZ = 1;
						swap = true;
					}
					else Log.Error("Wrong Room rotation");

					switch (room.Name)
					{
						case "LCZ_173":
							{
								if (!swap)
								{
									RoleType.ClassD.SpawnRagdoll(new Vector3(room.Transform.position.x + 25 * modX, room.Transform.position.y + 20, room.Transform.position.z + 10 * modZ), Random.rotation, "MasterOfDmx", DamageTypes.Scp173);
									ItemType.KeycardJanitor.SpawnItem(new Vector3(room.Transform.position.x + 21f * modX, room.Transform.position.y + 20, room.Transform.position.z + 11 * modZ), Vector3.zero);
									ItemType.Painkillers.SpawnItem(new Vector3(room.Transform.position.x - 3.5f * modX, room.Transform.position.y + 18, room.Transform.position.z - 8f * modZ), Vector3.zero);
									ItemType.Radio.SpawnItem(new Vector3(room.Transform.position.x - 3.5f * modX, room.Transform.position.y + 18, room.Transform.position.z - 7.5f * modZ), Vector3.zero);
								}
								else
								{
									RoleType.ClassD.SpawnRagdoll(new Vector3(room.Transform.position.x + 10f * modX, room.Transform.position.y + 20, room.Transform.position.z + 25f * modZ), Random.rotation, "MasterOfDmx", DamageTypes.Scp173);
									ItemType.KeycardJanitor.SpawnItem(new Vector3(room.Transform.position.x + 11f * modX, room.Transform.position.y + 20, room.Transform.position.z + 21f * modZ), Vector3.zero);
									ItemType.Painkillers.SpawnItem(new Vector3(room.Transform.position.x - 8f * modX, room.Transform.position.y + 18, room.Transform.position.z - 3.5f * modZ), Vector3.zero);
									ItemType.Radio.SpawnItem(new Vector3(room.Transform.position.x - 7.5f * modX, room.Transform.position.y + 18, room.Transform.position.z - 3.5f * modZ), Vector3.zero);
								}
								break;
							}
						case "HCZ_457":
							{
								if (!swap)
								{
									RoleType.NtfLieutenant.SpawnRagdoll(new Vector3(room.Transform.position.x - 2 * modX, room.Transform.position.y + 2, room.Transform.position.z), Random.rotation, "Monsieur Mem", DamageTypes.Scp096);
								}
								else
								{
									RoleType.NtfLieutenant.SpawnRagdoll(new Vector3(room.Transform.position.x, room.Transform.position.y + 2, room.Transform.position.z - 2 * modZ), Random.rotation, "Monsieur Mem", DamageTypes.Scp096);
								}
								break;
							}
						case "EZ_Intercom":
							{
								if (!swap)
								{
									ItemType.Radio.SpawnItem(new Vector3(room.Transform.position.x - 3f * modX, room.Transform.position.y - 5, room.Transform.position.z + 6 * modZ), Vector3.zero);
								}
								else
								{
									ItemType.Radio.SpawnItem(new Vector3(room.Transform.position.x - 6f * modX, room.Transform.position.y - 5, room.Transform.position.z + 3f * modZ), Vector3.zero);
								}

								break;
							}
						case "LCZ_Toilets":
							{
								if (!swap)
								{
									ItemType.Adrenaline.SpawnItem(new Vector3(room.Transform.position.x - 1f * modX, room.Transform.position.y + 1f, room.Transform.position.z - 10.5f * modZ), Random.rotation.eulerAngles);
								}
								else
								{
									ItemType.Adrenaline.SpawnItem(new Vector3(room.Transform.position.x + 10.5f * modX, room.Transform.position.y + 1f, room.Transform.position.z + 1f * modZ), Random.rotation.eulerAngles);
								}
								break;
							}
						case "LCZ_914 (14)":
							{
								if (!swap)
								{
									ItemType.WeaponManagerTablet.SpawnItem(new Vector3(room.Transform.position.x, room.Transform.position.y + 1.5f, room.Transform.position.z - 10f * modZ), Vector3.zero);

									ItemType.Radio.SpawnItem(new Vector3(room.Transform.position.x + 2f * modX, room.Transform.position.y + 2f, room.Transform.position.z - 10f * modZ), Vector3.zero);
								}
								else
								{
									ItemType.WeaponManagerTablet.SpawnItem(new Vector3(room.Transform.position.x + 10f * modX, room.Transform.position.y + 1.5f, room.Transform.position.z), Vector3.zero);

									ItemType.Radio.SpawnItem(new Vector3(room.Transform.position.x - 10f * modX, room.Transform.position.y + 1.8f, room.Transform.position.z + 2f * modZ), Random.rotation.eulerAngles);
								}
								break;
							}
						case "HCZ_106":
							{
								if (!swap)
								{

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x + 33.5f * modX, room.Transform.position.y + 1f, room.Transform.position.z - 6.15f * modZ), Vector3.zero);

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x + 33.5f * modX, room.Transform.position.y + 1.5f, room.Transform.position.z - 6.15f * modZ), Vector3.zero);

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x + 33.5f * modX, room.Transform.position.y + 2f, room.Transform.position.z - 6.15f * modZ), Vector3.zero);

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x + 33.5f * modX, room.Transform.position.y + 2.5f, room.Transform.position.z - 6.15f * modZ), Vector3.zero);

									ItemType.GunUSP.SpawnItem(new Vector3(room.Transform.position.x + 33.5f * modX, room.Transform.position.y + 1f, room.Transform.position.z - 21.35f * modZ), Vector3.zero);
								}
								else
								{
									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x - 6.15f * modX, room.Transform.position.y + 1f, room.Transform.position.z + 33.5f * modZ), Vector3.zero);

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x - 6.15f * modX, room.Transform.position.y + 1.5f, room.Transform.position.z + 33.5f * modZ), Vector3.zero);

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x - 6.15f * modX, room.Transform.position.y + 2f, room.Transform.position.z + 33.5f * modZ), Vector3.zero);

									ItemType.GrenadeFlash.SpawnItem(new Vector3(room.Transform.position.x - 6.15f * modX, room.Transform.position.y + 2.5f, room.Transform.position.z + 33.5f * modZ), Vector3.zero);

									ItemType.GunUSP.SpawnItem(new Vector3(room.Transform.position.x - 21.35f * modX, room.Transform.position.y + 1f, room.Transform.position.z + 33.5f * modZ), Vector3.zero);
								}
								break;
							}
					}
				}
				ItemType.GunUSP.SpawnItem(new Vector3(37.5f, 990f, -34.5f), Vector3.zero);
				ItemType.Medkit.SpawnItem(new Vector3(37.5f, 990f, -33.5f), Vector3.zero);
			}
			//Инициализация для сканирования
			if (ScanMod.config.enableScanning)
			{ 
				ScanController.INSTANCE = new ScanController();
			}
			//Инициализация для протоколов и спец. команд
			if (ScanMod.config.enableProtocols || ScanMod.config.enableSuperCommands)
			{
				ProtocolController.INSTANCE = new ProtocolController();
			}

		}

		public void OnConsoleCommand(SendingConsoleCommandEventArgs ev)
		{
			string[] args = ev.Arguments.ToArray();
			foreach (string str in args)
			{
				str.ToLower();
			}

			string command = ev.Name.ToLower();

			switch (command)
			{
				case "ps1":
					if (ScanMod.config.enableScanning)
					{
						ExtraMethods.TryScanSCP(ev.Player, ev);
					}
					break;
				case "ps4":
					if (ScanMod.config.enableScanning)
					{
						ExtraMethods.TryScanPersonnel(ev.Player, ev);
					}
					break;
				case "ps5":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryBlackout(ev.Player, 30f, ev);
					}
					break;
				case "pl1":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryBlockGates(ev.Player, ev);
					}
					break;
				case "pl2":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryBlockCheckpointAndGates(ev.Player, ev);
					}
					break;
				case "pl3":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryBlockDoors(ev.Player, ev);
					}
					break;
				case "pb2":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryLCZDecontain(ev.Player, ev);
					}
					break;
				case "pb3":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryHCZDecontain(ev.Player, ev);
					}
					break;
				case "pb4":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryLCZAndHCZDecontain(ev.Player, ev);
					}
					break;
				case "pb5":
					if (ScanMod.config.enableProtocols)
					{
						ExtraMethods.TryNuke(ev.Player, ev);
					}
					break;
				case "106":
					{
						if (ScanMod.config.enable106overhaul)
						{
							if (args[0].Equals("help"))
							{
								ev.ReturnMessage = "Для SCP 106 доступны следующие команды:\n.106 help - вывод доступных команд\n.106 damage урон задержка и .106 damage default - для установления своего урона в КИ и возвращения его к стандартному";
							}
							else if (args[0].Equals("damage"))
							{
								if (args[1].Equals("default"))
								{
									PocketProperties.customDamageEnabled = false;
									PocketProperties.customDamage = 1f;
									PocketProperties.customDelay = 1f;
									ev.ReturnMessage = "Урон в КИ успешно изменён на стандартный";
								}
								else
								{
									if (float.TryParse(args[1], out float damage) && float.TryParse(args[2], out float delay))
									{
										PocketProperties.customDamage = damage;
										PocketProperties.customDelay = delay;
										PocketProperties.customDamageEnabled = true;
										ev.ReturnMessage = "Урон в КИ успешно изменён";
									}
									else
									{
										ev.ReturnMessage = "Ошибка. Введите команду в формате .106 damage урон задержка";
									}
								}
							}
							else if (args[0].Equals("escape"))
							{
								int exits;
								try
								{
									exits = int.Parse(args[2]);
								}
								catch
								{
									ev.ReturnMessage = "Ошибка. Введите команду в формате .106 escape кол-во выходов";
									break;
								}
								if (exits > -1 && exits < 9)
								{
									ExtraMethods.SetPocketExits(exits);
									ev.ReturnMessage = "Количество выходов из КИ установлено на " + exits;
								}
								else
								{
									ev.ReturnMessage = "Ошибка. Кол-во выходов должно быть от 0 до 8";
								}

							}
							else if (args[0].Equals("cycle"))
							{
								if (PocketProperties.cycledPocket)
								{
									ev.ReturnMessage = "Замена смерти на обманный побег в КИ отключена";
									PocketProperties.cycledPocket = false;
								}
								else
								{
									ev.ReturnMessage = "Замена смерти на обманный побег в КИ включена";
									PocketProperties.cycledPocket = true;
								}
							}
							else
							{
								ev.ReturnMessage = "Чтобы получить список команд введите .106 help";
							}
						}
						break;
					}
				case "173":
					{
						if (ScanMod.config.enableRPItems)
						{
							AdditionalPlayerAbilities abilities = ev.Player.GameObject.GetComponentInParent<AdditionalPlayerAbilities>();
							if (abilities == null)
							{
								ev.ReturnMessage = "Player doesn't have abilities component";
								Log.Error("Player doesn't have abilities component");
							}
							else
							{
								if (abilities.has173ammo)
								{
									abilities.Charge173Ammo();
									ev.ReturnMessage = "Вы зарядили спец-патрон для SCP 173";
									ev.Player.Broadcast(5, "Вы зарядили спец-патрон для SCP 173");
								}
								else
								{
									ev.ReturnMessage = "У вас нет спец-патрона для SCP 173";
									ev.Player.Broadcast(5, "У вас нет спец-патрона для SCP 173");
								}
							}
						}
						break;
					}
					
			}
		}

		public void OnCommand(SendingRemoteAdminCommandEventArgs ev) 
		{
			string[] args = ev.Arguments.ToArray();

			foreach (string str in args)
			{
				str.ToLower();
			}
			string command = ev.Name.ToLower();

			if (command == "givemask" && ScanMod.config.enable096Mask)
			{
				Exiled.API.Features.Player target = Exiled.API.Features.Player.Get(args[0]);
				if (target != null)
				{
					if (target.Inventory.items.Count < 8)
					{
						if (!target.HasMaskInInventory())
						{
							target.Inventory.AddNewItem(ItemType.WeaponManagerTablet, 69);
							target.Broadcast(5, "Вам была выдана маска для SCP 096");
							ev.CommandSender.RaReply("Игроку с id " + args[0] + " успешно выдана маска для SCP 096", true, true, string.Empty);
						}
						else
						{
							ev.CommandSender.RaReply("У данного игрока уже есть маска для SCP 096", false, true, string.Empty);
						}
					}
					else
					{
						ev.CommandSender.RaReply("В инвентаре данного игрока нет свободного места", false, true, string.Empty);
					}
				}
				else
				{
					ev.CommandSender.RaReply("Не существует игрока c id " + args[0], false, true, string.Empty);
				}
			}
			else if (command == "scan" && ScanMod.config.enableScanning)
			{
				if (args.Length == 0)
				{
					ev.CommandSender.RaReply("Введите команду в формате scan scp/humans", false, true, string.Empty);
				}
				else if (args[0] == "scp")
				{
					ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
					ExtraMethods.TryScanSCP(null);
				}
				else if (args[0] == "humans")
				{
					ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
					ExtraMethods.TryScanPersonnel(null);
				}
				else
				{
					ev.CommandSender.RaReply("Введите команду в формате scan scp/humans", false, true, string.Empty);
				}

			}
			else if (command == "protocol" && ScanMod.config.enableProtocols)
			{
				if (args.Length == 0)
				{
					ev.CommandSender.RaReply("Введите команду в формате protocol название протокола", false, true, string.Empty);
				}
				else
				{
					switch (args[0])
					{
						case "pl1":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryBlockGates(ev.Sender);
							break;
						case "pl2":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryBlockCheckpointAndGates(ev.Sender);
							break;
						case "pl3":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryBlockDoors(ev.Sender);
							break;
						case "pb2":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryLCZDecontain(ev.Sender);
							break;
						case "pb3":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryHCZDecontain(ev.Sender);
							break;
						case "pb4":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryLCZAndHCZDecontain(ev.Sender);
							break;
						case "pb5":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryNuke(ev.Sender);
							break;
						case "ps5":
							ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
							ExtraMethods.TryBlackout(ev.Sender, 30f);
							break;
						default:
							ev.CommandSender.RaReply("Введите команду в формате protocol название протокола", false, true, string.Empty);
							break;
					}
				}
			}
			else if (command == "infect" && ScanMod.config.enable008)
			{
				try
				{
					Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(args[0]);
					if (player != null)
					{
						player.Infect();
						ev.CommandSender.RaReply("Игрок " + args[0] + " успешно заражён", true, true, string.Empty);
					}
					else
					{
						ev.CommandSender.RaReply("На сервере нет игрока " + args[0], false, true, string.Empty);
					}
				}
				catch
				{
					ev.CommandSender.RaReply("Введите команду в формате infect id/nickname", false, true, string.Empty);
				}
			}


		}

		public void OnCommandExtented(SendingRemoteAdminCommandEventArgs ev)
		{
			if (ScanMod.config.enableSuperCommands)
			{
				string[] args = ev.Arguments.ToArray();

				foreach (string str in args)
				{
					str.ToLower();
				}
				string command = ev.Name.ToLower();

				if (command == "ud_help")
				{
					ev.CommandSender.RaReply("Доступные команды:\nud_detonate start/stop - начинает или останавливает детонацию Альфа-боеголовки. Вручную отменить её нельзя\nud_blackout duration - начинает блэкаут с заданной длительностью", true, true, string.Empty);
				}
				else if (command == "ud_blackout")
				{
					if (float.TryParse(args[0], out float duration))
					{
						ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
						ProtocolController.INSTANCE.Blackout(duration, true);
					}
					else
					{
						ev.CommandSender.RaReply("Введите команду в формате ud_blackout duration", false, true, string.Empty);
					}
				}
				else if (command == "ud_detonate")
				{
					if (args[0] == "start")
					{
						ProtocolController.INSTANCE.NukeFacility(true);
						ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
					}
					else if (args[0] == "stop")
					{
						ProtocolController.INSTANCE.StopNuke();
						ev.CommandSender.RaReply("Команда применена успешно", true, true, string.Empty);
					}
					else
					{
						ev.CommandSender.RaReply("Введите команду в формате ud_detonate start/stop", false, true, string.Empty);
					}

				}
				else if (command == "ud_encoder")
				{
					try
					{
						Exiled.API.Features.Player target = Exiled.API.Features.Player.Get(args[0]);
						if (target.GameObject.TryGetComponent<AdditionalPlayerAbilities>(out AdditionalPlayerAbilities abilities))
						{
							if (!abilities.scp096whitelisted)
							{
								abilities.scp096whitelisted = true;
								ev.CommandSender.RaReply("Игрок " + args[0] + " теперь не вызывает ярость у скромника", true, true, string.Empty);
							}
							else
							{
								abilities.scp096whitelisted = false;
								ev.CommandSender.RaReply("Игрок " + args[0] + " теперь вызывает ярость у скромника", true, true, string.Empty);
							}
						}
						else
						{
							ev.CommandSender.RaReply("Ошибка, на игроке отсутствует нужный компонент, эта ошибка не должна возникать", false, true, string.Empty);
						}
					}
					catch
					{
						ev.CommandSender.RaReply("Введите команду в формате ud_enconder id/nickname", false, true, string.Empty);
					}
				}
			}
		}
	}
}
