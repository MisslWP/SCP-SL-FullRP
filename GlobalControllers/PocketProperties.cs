using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scanner
{
	static class PocketProperties
	{

		public static bool customDamageEnabled = false;

		public static float customDamage = 0f;

		public static float customDelay = 1f;

		public static bool cycledPocket = false;

		public static PocketDimensionTeleport[] teleports;

		public static List<Vector3> newExits = new List<Vector3>();

		public static readonly Vector3 pocketPos = new Vector3(0, -1998, 0);
	}
}
