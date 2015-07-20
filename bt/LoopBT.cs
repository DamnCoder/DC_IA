using UnityEngine;
using System.Collections;
using System;

namespace DC.IA.BT
{
///A node that repeats its child until the condition is met.
/** 
 * If the child returns BT_FAILURE, LoopBT will also return BT_FAILURE. 
 * However, if the child returns BT_SUCCESS and it hasn't met the condition, 
 * LoopBT will restart it and continue returning BT_RUNNING. 
*/
	public class LoopBT : SequentialBT 
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
		private Func<bool>	endLoopCondition = delegate {return false; };
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
		public LoopBT()
		{}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		
		protected override STATUS AllNodesExecutedAction ()
		{
			ResetNodeCounter();
			if(endLoopCondition())
			{
				//Debug.Log ("[LoopBT] Finaliza el bucle, devuelvo SUCCESS");
				return STATUS.SUCCESS;
			}
			//Debug.Log ("[LoopBT] No finaliza el bucle, reseteo contador de nodos y devuelvo RUNNING");
			return STATUS.RUNNING;
		}
		
		protected override void EndSequenceAction (STATUS status)
		{
			if(!IsRunning(status))
			{
				//Debug.Log ("[LoopBT] Accion final, status != RUNNING, ejecuto Init");
				Init();
			}
		}
		#endregion
	
		//--------------------------------------------------
		// Public Methods
		//--------------------------------------------------
		
		#region PUBLIC_METHODS
		public void SetEndLoopCondition(Func<bool> endLoopCondition)
		{
			this.endLoopCondition = endLoopCondition;
		}
		#endregion
	
		//--------------------------------------------------
		// Private/Protected Methods
		//--------------------------------------------------
	
		#region PRIVATE_PROTECTED_METHODS
		#endregion
	
	}

}