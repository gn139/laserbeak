using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using DTAnimatorStateMachine;
using DTObjectPoolManager;
using InControl;

using DT.Game.Battle.Stats;
using DT.Game.GameModes;
using DT.Game.Players;

namespace DT.Game.Scoring {
	public class StatsContainer : MonoBehaviour {
		// PRAGMA MARK - Public Interface
		public void Init(Player player) {
			List<StatAward> allAwards = StatsManager.GetStatsFor(player).SelectMany(s => s.GetQualifiedAwards()).ToList();
			StatAward chosenAward = null;
			if (allAwards.Count > 0) {
				chosenAward = allAwards.Random();
			} else {
				chosenAward = new StatAward(sourceStat: null, awardText: "BEST AT: PARTICIPATING");
			}

			awardText_.Text = chosenAward.AwardText;

			Color color = player.Skin.BodyColor;
			awardText_.Color = color;
			separatorImage_.color = color;

			foreach (Stat stat in StatsManager.GetStatsFor(player)) {
				var view = ObjectPoolManager.Create<StatView>(GamePrefabs.Instance.StatView, parent: statViewContainer_);
				view.Init(player, stat.DisplayName, stat.DisplayValue, showMarker: chosenAward.SourceStat == stat);
			}
		}


		// PRAGMA MARK - Internal
		[Header("Outlets")]
		[SerializeField]
		private TextOutlet awardText_;

		[SerializeField]
		private GameObject statViewContainer_;

		[SerializeField]
		private Image separatorImage_;
	}
}