using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using DTAnimatorStateMachine;
using DTObjectPoolManager;
using InControl;

namespace DT.Game.LevelEditor {
	public class LevelEditorMenu {
		// PRAGMA MARK - Public Interface
		public LevelEditorMenu(InputDevice inputDevice, Action exitCallback, Action saveCallback) {
			inputDevice_ = inputDevice;
			exitCallback_ = exitCallback;
			saveCallback_ = saveCallback;

			MonoBehaviourWrapper.OnUpdate += HandleUpdate;
		}

		public void Dispose() {
			MonoBehaviourWrapper.OnUpdate -= HandleUpdate;
			HideMenu();
		}


		// PRAGMA MARK - Internal
		private InputDevice inputDevice_;
		private Action exitCallback_;
		private Action saveCallback_;

		private void HandleUpdate() {
			if (inputDevice_.Command.WasPressed && !MenuView.Showing) {
				MenuView.Show(inputDevice_, "LEVEL EDITOR", new Dictionary<string, Action>() {
					{ "BACK TO EDITING", HideMenu },
					{ "SAVE", () => {
						saveCallback_.Invoke();
						HideMenu();
					} },
					{ "EXIT EDITING", () => exitCallback_.Invoke() },
				});
			}
		}

		private void HideMenu() {
			MenuView.Hide();
		}
	}
}