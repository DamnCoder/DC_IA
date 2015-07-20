using UnityEngine;
using System.Collections;

namespace DC.IA.BT
{
///Executes behaviors in priority order until one of them is successful.
/** Attempts to execute children in the order they were added. 
- If a child returns BT_FAILURE, begin executing the next child if there is one, else return BT_FAILURE.
- If a child returns BT_ERROR, return BT_ERROR.
- If a child returns BT_SUCCESS, return BT_SUCCESS.
- If a child returns BT_RUNNING, return BT_RUNNING, and keeps the state to continue from it in the next execution.
*/
	public class PriorityBT : SequentialBT 
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
		public PriorityBT() {}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		protected override STATUS AllNodesExecutedAction()
		{
			mn_CurrentNode = 0;
			return STATUS.FAILURE;
		}
		
		protected override bool SequenceCondition(STATUS status)
		{
			if(IsFailure(status))
			{
				++mn_CurrentNode;
			}
			return status == STATUS.FAILURE;
		}
		
		protected override void EndSequenceAction(STATUS status)
		{
			if(IsSuccess(status))
			{
				mn_CurrentNode = 0;
			}
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