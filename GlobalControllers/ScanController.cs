using Exiled.API.Features;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scanner
{
	public class ScanController
	{
		private int SCP_Sur, SCP_ENT, SCP_LCZ, SCP_HCZ, SCP_Unk,
					D_Sur, D_ENT, D_LCZ, D_HCZ, D_Unk,
					SCI_Sur, SCI_ENT, SCI_LCZ, SCI_HCZ, SCI_Unk,
					CI_Sur, CI_ENT, CI_LCZ, CI_HCZ, CI_Unk;
		private bool CIDetected = false;

		public static ScanController INSTANCE;
		private void CountPlayers()
		{
			SCP_Sur = 0;
			SCP_ENT = 0;
			SCP_LCZ = 0;
			SCP_HCZ = 0;
			SCP_Unk = 0;

			D_Sur = 0;
			D_ENT = 0;
			D_LCZ = 0;
			D_HCZ = 0;
			D_Unk = 0;

			SCI_Sur = 0;
			SCI_ENT = 0;
			SCI_LCZ = 0;
			SCI_HCZ = 0;
			SCI_Unk = 0;

			CI_Sur = 0;
			CI_ENT = 0;
			CI_LCZ = 0;
			CI_HCZ = 0;
			CI_Unk = 0;
			foreach (Player player in Player.List)
			{
				
				if (player.Team == Team.SCP)
				{
					if (player.CurrentRoom.Name.Contains("HCZ"))
					{
						SCP_HCZ++;
					}
					else if (player.CurrentRoom.Name.Contains("EZ"))
					{
						SCP_ENT++;
					}
					else if (player.CurrentRoom.Name.Contains("LCZ"))
					{
						SCP_LCZ++;
					}
					else if (player.Position.y > 500)
					{
						SCP_Sur++;
					}
					else
					{
						SCP_Unk++;
					}
				}
				else if (player.Role == RoleType.ClassD)
				{
					if (player.CurrentRoom.Name.Contains("HCZ"))
					{
						D_HCZ++;
					}
					else if (player.CurrentRoom.Name.Contains("EZ"))
					{
						D_ENT++;
					}
					else if (player.CurrentRoom.Name.Contains("LCZ"))
					{
						D_LCZ++;
					}
					else if (player.Position.y > 500)
					{
						D_Sur++;
					}
					else
					{
						D_Unk++;
					}
				}
				else if (player.Role == RoleType.Scientist)
				{
					if (player.CurrentRoom.Name.Contains("HCZ"))
					{
						SCI_HCZ++;
					}
					else if (player.CurrentRoom.Name.Contains("EZ"))
					{
						SCI_ENT++;
					}
					else if (player.CurrentRoom.Name.Contains("LCZ"))
					{
						SCI_LCZ++;
					}
					else if (player.Position.y > 500)
					{
						SCI_Sur++;
					}
					else
					{
						SCI_Unk++;
					}
				}
				else if (player.Role == RoleType.ChaosInsurgency)
				{
					if (player.CurrentRoom.Name.Contains("HCZ"))
					{
						CI_HCZ++;
					}
					else if (player.CurrentRoom.Name.Contains("EZ"))
					{
						CI_ENT++;
					}
					else if (player.CurrentRoom.Name.Contains("LCZ"))
					{
						CI_LCZ++;
					}
					else if (player.Position.y > 500)
					{
						CI_Sur++;
					}
					else
					{
						CI_Unk++;
					}
				}
			}
		}
		public void SCPCassie()
		{
			CountPlayers();
			Cassie.Message("P S 1 Protocol has been activated . Scanning the facility for ScpSubjects . 30 seconds remaining", false, false);
			String start = ".g4 Scan completed . Attention to all security guards";
			if (SCP_Sur == 0 && SCP_ENT == 0 && SCP_LCZ == 0 && SCP_HCZ == 0 && SCP_Unk == 0)
			{
				Cassie.DelayedMessage(start + " . no ScpSubjects detected in the facility", 30f, false, false);
			}
			else
			{
				String sur, ent, lcz, hcz, unk;
				sur = "";
				ent = "";
				lcz = "";
				hcz = "";
				unk = "";
				if (SCP_Sur > 0)
				{
					sur = " . " + SCP_Sur + " SCP detected in Surface Zone";
				}
				if (SCP_ENT > 0)
				{
					ent = " . " + SCP_ENT + " SCP detected in Entrance Zone";
				}
				if (SCP_LCZ > 0)
				{
					lcz = " . " + SCP_LCZ + " SCP detected in Light Containment Zone";
				}
				if (SCP_HCZ > 0)
				{
					hcz = " . " + SCP_HCZ + " SCP detected in Heavy Containment Zone";
				}
				if (SCP_Unk > 0)
				{
					unk = " . " + SCP_Unk + " SCP detected in Unknown Zone";
				}
				Cassie.DelayedMessage(start+sur+ent+lcz+hcz+unk, 30f, false, false);
				
			}
		}
		public void PersonnelCassie()
		{
			CountPlayers();
			Cassie.Message("P S 4 Protocol has been activated . Scanning the facility for Facility personnel . 30 seconds remaining", false, false);
			String  d_sur = "", d_ent = "", d_lcz = "", d_hcz = "", d_unk ="",
					sci_sur = "", sci_ent = "", sci_lcz = "", sci_hcz = "", sci_unk = "",
					ci_sur = "", ci_ent = "", ci_lcz = "", ci_hcz = "", ci_unk = "";
			String start = ".g4 Scan completed . Attention to all security guards";
			String pause = " . . ";
			String NoClassD = "", NoScience = "";
			bool d_detected, sci_detected;

			d_detected = D_Sur > 0 || D_ENT > 0 || D_LCZ > 0 || D_HCZ > 0 || D_Unk > 0;
			sci_detected = SCI_Sur > 0 || SCI_ENT > 0 || SCI_LCZ > 0 || SCI_HCZ > 0 || SCI_Unk > 0;
			
			if (!d_detected)
			{
				NoClassD = " No Class D personnel detected in the facility";
			}
			if (!sci_detected)
			{
				NoScience = " No Science personnel detected in the facility";
			}

			

			if (D_Sur == 0 && D_ENT == 0 && D_LCZ == 0 && D_HCZ == 0 && D_Unk == 0 && SCI_Sur == 0 && SCI_ENT == 0 && SCI_LCZ == 0 && SCI_HCZ == 0 && SCI_Unk == 0 && CI_Sur == 0 && CI_ENT == 0 && CI_LCZ == 0 && CI_HCZ == 0 && CI_Unk == 0)
			{
				Cassie.DelayedMessage(start + " . no SCP Foundation personnel detected in the facility", 30f, false, false);
			}
			else
			{
				if (D_Sur > 0)
				{
					d_sur = " . " + D_Sur + " Class D personnel detected in Surface Zone";
				}
				if (D_ENT > 0)
				{
					d_ent = " . " + D_ENT + " Class D personnel detected in Entrance Zone";
				}
				if (D_LCZ > 0)
				{
					d_lcz = " . " + D_LCZ + " Class D personnel detected in Light Containment Zone";
				}
				if (D_HCZ > 0)
				{
					d_hcz = " . " + D_HCZ + " Class D personnel detected in Heavy Containment Zone";
				}
				if (D_Unk > 0)
				{
					d_unk = " . " + D_Unk + " Class D personnel detected in Unknown Zone";
				}

				if (SCI_Sur > 0)
				{
					sci_sur = " . " + SCI_Sur + " Science personnel detected in Surface Zone";
				}
				if (SCI_ENT > 0)
				{
					sci_ent = " . " + SCI_ENT + " Science personnel detected in Entrance Zone";
				}
				if (SCI_LCZ > 0)
				{
					sci_lcz = " . " + SCI_LCZ + " Science personnel detected in Light Containment Zone";
				}
				if (SCI_HCZ > 0)
				{
					sci_hcz = " . " + SCI_HCZ + " Science personnel detected in Heavy Containment Zone";
				}
				if (SCI_Unk > 0)
				{
					sci_unk = " . " + SCI_Unk + " Science personnel detected in Unknown Zone";
				}

				if (CIDetected)
				{
					if (CI_Sur > 0)
					{
						ci_sur = " . " + CI_Sur + " ChaosInsurgency detected in Surface Zone";
					}
					if (CI_ENT > 0)
					{
						ci_ent = " . " + CI_ENT + " ChaosInsurgency detected in Entrance Zone";
					}
					if (CI_LCZ > 0)
					{
						ci_lcz = " . " + CI_LCZ + " ChaosInsurgency detected in Light Containment Zone";
					}
					if (CI_HCZ > 0)
					{
						ci_hcz = " . " + CI_HCZ + " ChaosInsurgency detected in Heavy Containment Zone";
					}
					if (CI_Unk > 0)
					{
						ci_unk = " . " + CI_Unk + " ChaosInsurgency detected in Unknown Zone";
					}
				}


				if (!CIDetected && (CI_Sur > 0 || CI_ENT > 0 || CI_LCZ > 0 || CI_HCZ > 0 || CI_Unk > 0))
				{
					Cassie.DelayedMessage(start + pause + NoClassD + d_sur + d_ent + d_lcz + d_hcz + d_unk + pause + NoScience + sci_sur + sci_ent + sci_lcz + sci_hcz + sci_unk + pause + " . Emergency Alert . . .g5 Not authorized personnel has been spotted in the facility . Scanning . . .g2 Scan .g4 completed . ChaosInsurgency detected .  Activating 4 level of alarm . Priority code red . AllRemaining", 30f, false, false);
					CIDetected = true;
				}
				else
				{
					Cassie.DelayedMessage(start + pause + NoClassD + d_sur + d_ent + d_lcz + d_hcz + d_unk + pause + NoScience + sci_sur + sci_ent + sci_lcz + sci_hcz + sci_unk + pause + ci_sur + ci_ent + ci_lcz + ci_hcz + ci_unk, 30f, false, false);
				}
				
			}
		}
	}
}
