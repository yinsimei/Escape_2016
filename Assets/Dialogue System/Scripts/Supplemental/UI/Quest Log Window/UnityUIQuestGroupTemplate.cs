#if !(UNITY_4_3 || UNITY_4_5)
using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem {

	/// <summary>
	/// This component hooks up the elements of a Unity UI quest group template.
	/// Add it to your quest group template and assign the properties.
	/// </summary>
	[AddComponentMenu("Dialogue System/UI/Unity UI/Quest/Unity UI Quest Group Template")]
	public class UnityUIQuestGroupTemplate : MonoBehaviour	{

		[Header("Quest Group Heading")]
		[Tooltip("The quest group name")]
		public UnityEngine.UI.Text heading;

		public bool ArePropertiesAssigned {
			get {
				return (heading != null);
			}
		}

		public void Initialize() {}

	}

}
#endif