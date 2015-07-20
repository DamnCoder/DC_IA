using UnityEngine;
using System.Collections;

public abstract class Behavior 
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
	protected Transform	transform;
	#endregion
	
	//--------------------------------------------------
	// Private/Protected Fields
	//--------------------------------------------------
	
	#region PRIVATE_PROTECTED_FIELDS
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
	public Behavior(Transform target)
	{
		this.transform = target;
	}
	#endregion

	//--------------------------------------------------
	// Overrided/Overloaded methods
	//--------------------------------------------------
	
	#region OVERRIDED_OVERLOADED_METHODS
	#endregion

	//--------------------------------------------------
	// Public Methods
	//--------------------------------------------------
	
	#region PUBLIC_METHODS
	public abstract void Behave();
	#endregion

	//--------------------------------------------------
	// Private/Protected Methods
	//--------------------------------------------------

	#region PRIVATE_PROTECTED_METHODS
	#endregion

}
