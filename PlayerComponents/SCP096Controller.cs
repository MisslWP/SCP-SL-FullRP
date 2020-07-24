using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scanner
{
	class SCP096Controller : MonoBehaviour
	{
		private GameObject scpObject;
		private Player scpPlayer;
		private PlayableScps.Scp096 this096;
		public bool masked;
		public bool maskDamageTriggered = true;
		private void Start()
		{
			scpObject = this.gameObject;
			scpPlayer = Player.Get(scpObject);

			this096 = scpObject.GetComponent<PlayableScpsController>().CurrentScp as PlayableScps.Scp096;			
		}
		private void OnDisable()
		{
			UnityEngine.Object.Destroy(this);
		}

		private void Update()
		{
			if (masked == true)
			{
				this096.PreWindup(float.MaxValue);
			}
			else if (!maskDamageTriggered)
			{
				maskDamageTriggered = true;
				this096.PreWindup();
			}
		}
		public void SetMasked(bool @bool)
		{
			masked = @bool;
			if (@bool)
			{
				Log.Debug("Одели маску на скромника");
				scpPlayer.Broadcast(5,"На вас одели магнитную маску. Следуйте за людьми около вас для дальнейшей эвакуации");
				maskDamageTriggered = false;
			}
		}
	}
}
