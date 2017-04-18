using System;
using System.Collections;
using UnityEngine;

using DT.Game.Battle.Players;
using DTAnimatorStateMachine;
using DTEasings;
using DTObjectPoolManager;
using InControl;

namespace DT.Game.Battle.Lasers {
	public class LaserHit : MonoBehaviour, IRecycleSetupSubscriber {
		// PRAGMA MARK - Public Interface
		public void SetMaterial(Material laserMaterial) {
			Color color = laserMaterial.GetColor("_EmissionColor");

			light_.color = color;
			mistParticleSystem_.SetStartColor(color);
			particleSystemRenderer_.material.SetColor("_EmissionColor", color);
		}


		// PRAGMA MARK - IRecycleSetupSubscriber Implementation
		public void OnRecycleSetup() {
			CoroutineWrapper.DoEaseFor(duration_, EaseType.CubicEaseOut, (float percentage) => {
				light_.intensity = Mathf.Lerp(kLightIntensity, 0.0f, percentage);
			}, () => {
				ObjectPoolManager.Recycle(this);
			});
		}


		// PRAGMA MARK - Internal
		private const float kLightIntensity = 2.0f;

		[Header("Outlets")]
		[SerializeField]
		private Light light_;

		[SerializeField]
		private ParticleSystem mistParticleSystem_;

		[SerializeField]
		private ParticleSystemRenderer particleSystemRenderer_;

		[Header("Properties")]
		[SerializeField]
		private float duration_ = 1.0f;
	}
}