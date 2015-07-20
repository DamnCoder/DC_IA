using UnityEngine;
using System.Collections;

namespace DC.IA.BT
{
///A node that repeats its child a specified number of times.
/** 
 * If the child returns BT_FAILURE, RepeatNode will also return BT_FAILURE. 
 * However, if the child returns BT_SUCCESS and it hasn't completed the specified number of repetitions, 
 * RepeatNode will restart it and continue returning BT_RUNNING. 
*/
	public class RepeatBT : SequentialBT 
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
		private int mn_CurrentIteration;
		private int mn_MaxIterations;
		#endregion
	
		//--------------------------------------------------
		// Accessors
		//--------------------------------------------------
		
		#region ACCESSORS
		private bool NotEndLoop
		{ get { return mn_CurrentIteration < mn_MaxIterations; }}
		#endregion
	
		//--------------------------------------------------
		// Constructors
		//--------------------------------------------------
		
		#region CONSTRUCTORS
		public RepeatBT(int repetitions) 
		{
			this.mn_MaxIterations = repetitions;
		}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		public override void Init ()
		{
			base.Init();
			this.mn_CurrentIteration = 0;
		}
		
		protected override STATUS AllNodesExecutedAction ()
		{
			++mn_CurrentIteration;
			
			if(NotEndLoop)
			{
				Debug.Log ("[RepeatBT] No finaliza el bucle, reseteo contador de nodos y devuelvo RUNNING");
				ResetNodeCounter();
				return STATUS.RUNNING;
			}
			Debug.Log ("[RepeatBT] Finaliza el bucle, devuelvo SUCCESS");
			return STATUS.SUCCESS;
		}
		
		protected override void EndSequenceAction (STATUS status)
		{
			if(!IsRunning(status))//status != STATUS.RUNNING)
			{
				//Debug.Log ("[RepeatBT] Accion final, status != RUNNING, ejecuto Init");
				Init();
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