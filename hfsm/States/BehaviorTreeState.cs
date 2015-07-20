using UnityEngine;
using System.Collections;
using DC.IA.hfsm;
using DC.IA.BT;

public class BehaviorTreeState : State 
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
	protected BehaviorTree bt;
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
	public BehaviorTreeState(string id) : base(id)
	{}
	#endregion

	//--------------------------------------------------
	// Overrided/Overloaded methods
	//--------------------------------------------------
	
	#region OVERRIDED_OVERLOADED_METHODS
	public override void OnCreate ()
	{
		base.OnCreate ();
	}
	
	public override void OnActivate ()
	{
		base.OnActivate ();
	}
	
	public override void OnDeactivate ()
	{
		base.OnDeactivate ();
	}
	
	public override void OnDestroy ()
	{
		base.OnDestroy ();
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
