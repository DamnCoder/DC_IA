using UnityEngine;
using System;
using System.Collections.Generic;

namespace DC.IA.hfsm
{
	public class EventState : State
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
		public event Action	OnCreateActions = null;
		public event Action	OnDestroyActions = null;
		
		public event Action	OnActivateActions = null;
		public event Action	OnDeactivateActions = null;
		
		public event Action	OnUpdateActions = null;
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
		
		public EventState (string id) : base(id) { }
		
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		
		public override void OnCreate ()
		{
			EventHelp.Emit(OnCreateActions);
		}
		
		public override void OnDestroy ()
		{
			EventHelp.Emit(OnDestroyActions);
		}
		
		public override void OnActivate ()
		{
			EventHelp.Emit(OnActivateActions);
		}
		
		public override void OnDeactivate ()
		{
			EventHelp.Emit(OnDeactivateActions);
		}
		
		public override void OnUpdate ()
		{
			EventHelp.Emit(OnUpdateActions);
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
