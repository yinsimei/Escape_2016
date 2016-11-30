#if !(UNITY_4_3 || UNITY_4_5)
using UnityEngine;
using System.Collections;

namespace PixelCrushers.DialogueSystem {
	
	public class UIShowHideController {

		public Component panel;

		private Animator animator = null;

		private bool lookedForAnimator = false;

		private Coroutine animCoroutine;

		public UIShowHideController(GameObject go, Component panel) {
			this.panel = panel;
			this.animator = (go != null) ? go.GetComponent<Animator>() : null;
			if (animator == null && panel != null) animator = panel.GetComponent<Animator>();
			this.animCoroutine = null;
		}

		public void Show(string showTrigger, bool pause, System.Action callback, bool wait = true) {
			var newTimeScale = pause ? 0 : Time.timeScale;
			CancelCurrentAnim();
			animCoroutine = DialogueManager.Instance.StartCoroutine(WaitForAnimation(showTrigger, newTimeScale, true, wait, callback));
		}

		public void Hide(string hideTrigger, System.Action callback) {
			CancelCurrentAnim();
			animCoroutine = DialogueManager.Instance.StartCoroutine(WaitForAnimation(hideTrigger, Time.timeScale, false, true, callback));
		}

		private IEnumerator WaitForAnimation(string triggerName, float newTimeScale, bool panelActive, bool wait, System.Action callback) {
			if (panelActive) Tools.SetGameObjectActive(panel, true);
			if (CanTriggerAnimation(triggerName)) {
                CheckAnimatorModeAndTimescale(triggerName); //---Was: Time.timeScale = 1; // Can't guarantee animator is set to Unscaled, so unpause to play.
                animator.SetTrigger(triggerName);
				const float maxWaitDuration = 10;
				float timeout = Time.realtimeSinceStartup + maxWaitDuration;
				var goalHashID = Animator.StringToHash(triggerName);
				var oldHashId = UITools.GetAnimatorNameHash(animator.GetCurrentAnimatorStateInfo(0));
				var currentHashID = oldHashId;
                if (wait && !Mathf.Approximately(0, Time.timeScale)) {
                    while ((currentHashID != goalHashID) && (currentHashID == oldHashId) && (Time.realtimeSinceStartup < timeout)) {
                        yield return null;
                        currentHashID = UITools.GetAnimatorNameHash(animator.GetCurrentAnimatorStateInfo(0));
                    }
                    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
                }
            }
			if (!panelActive) Tools.SetGameObjectActive(panel, false);
			Time.timeScale = newTimeScale;
			animCoroutine = null;
			if (callback != null) callback.Invoke();
		}		

        private void CheckAnimatorModeAndTimescale(string triggerName) {
            if (Mathf.Approximately(0, Time.timeScale) && (animator.updateMode != AnimatorUpdateMode.UnscaledTime) && DialogueDebug.LogWarnings) {
                Debug.LogWarning("Dialogue System: Time is paused but animator mode isn't set to Unscaled Time; the animation triggered by " + triggerName + " won't play.", animator);
            }
        }
		
		private void CancelCurrentAnim() {
			if (animCoroutine != null) {
				DialogueManager.Instance.StopCoroutine(animCoroutine);
				animCoroutine = null;
			}
		}

		public void ClearTrigger(string triggerName) {
			if (HasAnimator() && !string.IsNullOrEmpty(triggerName)) {
				animator.ResetTrigger(triggerName);
			}
		}
		
		private bool CanTriggerAnimation(string triggerName) {
			return HasAnimator() && !string.IsNullOrEmpty(triggerName);
		}
		
		private bool HasAnimator() {
			if ((animator == null) && !lookedForAnimator) {
				lookedForAnimator = true;
				if (panel != null) {
					animator = panel.GetComponent<Animator>();
					if (animator == null) animator = panel.GetComponentInChildren<Animator>();
				}
			}
			return (animator != null);
		}
		
	}
	
}
#endif