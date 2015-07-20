using UnityEngine;
using System;
using System.Collections.Generic;

namespace DC.IA.hfsm
{
	public class AnimationEventState : State
	{
		//--------------------------------------------------
		// Constants/Enums
		//--------------------------------------------------
		
		#region CONSTANTS_ENUMS
		#endregion
		
		//--------------------------------------------------
		// Public Fields
		//--------------------------------------------------
		
		#region PUBLIC_FIELDS
		#endregion
		
		//--------------------------------------------------
		// Private/Protected Fields
		//--------------------------------------------------
		
		#region PRIVATE_PROTECTED_FIELDS
		private float crossFadeTime;
		private event Action<string, float> AnimationTickEvent = delegate {};
		#endregion
	
		//--------------------------------------------------
		// Accessors
		//--------------------------------------------------
		
		#region ACCESSORS
		#endregion
	
		//--------------------------------------------------
		// Constructors
		//--------------------------------------------------
		
		#region CONSTRUCTORS
		public AnimationEventState (string id, Action<string, float> animationTick, float crossFadeTime) : 
			base(id)
		{
			this.crossFadeTime = crossFadeTime;
			AnimationTickEvent += animationTick;
		}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		
		public override void OnActivate ()
		{
			AnimationTickEvent(Id, crossFadeTime);
		}
		
		#endregion
	
		//--------------------------------------------------
		// Public Methods
		//--------------------------------------------------
		
		#region PUBLIC_METHODS
		#endregion
	
		//--------------------------------------------------
		// Private/Protected Methods
		//--------------------------------------------------
	
		#region PRIVATE_PROTECTED_METHODS
		#endregion
	
	}

}