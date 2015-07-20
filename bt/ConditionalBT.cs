using UnityEngine;
using System.Collections;
using System;

namespace DC.IA.BT
{
	public class ConditionalBT : BehaviorTree
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
		private Func<bool> conditionFunc = delegate { return false; };
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
		public ConditionalBT() {}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		public override BehaviorTree Add (BehaviorTreeNode btNode)
		{
			DebugHelp.Assert(ml_Nodes.Count < 2, "[ERROR] ConditionalBT only needs two children.");
			
			base.Add(btNode);
			
			return this;
		}
		
		public override STATUS Execute ()
		{
			if(!HasNodes)
			{
				return STATUS.SUCCESS;
			}
			
			Init();
			
			if(conditionFunc())
			{
				return ml_Nodes[0].Execute();
			}
			
			return ml_Nodes[1].Execute();
		}
		#endregion
	
		//--------------------------------------------------
		// Public Methods
		//--------------------------------------------------
		
		#region PUBLIC_METHODS
		public void AddCondition(Func<bool> condition)
		{
			this.conditionFunc = condition;
		}
		#endregion
	
		//--------------------------------------------------
		// Private/Protected Methods
		//--------------------------------------------------
	
		#region PRIVATE_PROTECTED_METHODS
		#endregion
	
	}

}