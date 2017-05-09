using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DTAnimatorStateMachine;
using DTEasings;
using DTObjectPoolManager;
using InControl;

using DT.Game.Battle.AI;
using DT.Game.Battle.Players;
using DT.Game.GameModes;

namespace DT.Game {
	public static class InGameConstants {
		// PRAGMA MARK - Public Interface
		public static bool AllowChargingLasers = true;
		public static bool AllowBattlePlayerMovement = true;

		public static bool EnableQuacking = false;
		public static bool EnableFlapping = true;

		public static readonly HashSet<BattlePlayer> AllowedChargingLasersWhitelist = new HashSet<BattlePlayer>();

		public static bool IsAllowedToChargeLasers(BattlePlayer battlePlayer) {
			if (AllowChargingLasers == false) {
				return AllowedChargingLasersWhitelist.Contains(battlePlayer);
			}

			return true;
		}
	}
}