using System;
using System.Collections;
using UnityEngine;

using DTAnimatorStateMachine;
using DTObjectPoolManager;
using InControl;

namespace DT.Game.Battle.Lasers {
	public class ChargingLaser : MonoBehaviour, IRecycleCleanupSubscriber {
		// PRAGMA MARK - Public Interface
		public void UpdateWithPercentage(float percentage) {
			this.transform.localScale = new Vector3(percentage, percentage, percentage);
			pointLight_.range = percentage * kLightRange;

			chargingParticleSystem_.SetEmissionRateOverTime(Mathf.Lerp(kMaxParticleRateOverTime, 0.0f, percentage));
		}

		public void SetLaserMaterial(Material laserMaterial) {
			laserRenderer_.material = laserMaterial;

			Color laserColor = laserMaterial.GetColor("_EmissionColor");
			pointLight_.color = laserColor;

			chargingParticleSystem_.GetComponent<ParticleSystemRenderer>().material = laserMaterial;
		}


		// PRAGMA MARK - IRecycleCleanupSubscriber Implementation
		public void OnRecycleCleanup() {
			this.transform.localScale = Vector3.zero;
			pointLight_.range = 0.0f;
			chargingParticleSystem_.SetEmissionRateOverTime(0.0f);
		}


		// PRAGMA MARK - Internal
		private const float kMaxParticleRateOverTime = 35.0f;
		private const float kLightRange = 4.0f;

		[Header("Outlets")]
		[SerializeField]
		private Light pointLight_;

		[SerializeField]
		private ParticleSystem chargingParticleSystem_;

		[SerializeField]
		private Renderer laserRenderer_;
	}
}