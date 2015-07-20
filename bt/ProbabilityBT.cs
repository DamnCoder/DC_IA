using UnityEngine;
using System.Collections.Generic;

namespace DC.IA.BT
{
///Executes behaviors randomly, based on a given set of weights.
/** The weights are not percentages, but rather simple ratios.
For example, if there were two children with a weight of one, each would have a 50% chance of being executed.
If another child with a weight of eight were added, the previous children would have a 10% chance of being executed, 
and the new child would have an 80% chance of being executed.
This weight system is intended to facilitate the fine-tuning of behaviors.
*/
	public class ProbabilityBT : BehaviorTree 
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
		private float				mf_TotalWeigth;
		private BehaviorTreeNode 	mp_CurrentNode;
		
		private List<KeyValuePair<BehaviorTreeNode, float>>	md_WeightList;
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
		public ProbabilityBT() 
		{
			md_WeightList = new List<KeyValuePair<BehaviorTreeNode, float>>();
			mf_TotalWeigth = 0f;
		}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		public override BehaviorTree Add (BehaviorTreeNode btNode)
		{
			Add (btNode, 1f);
			
			return this;
		}
		
		public override void Init ()
		{
			base.Init();
			mp_CurrentNode = null;
		}
		
		public override STATUS Execute ()
		{
			Debug.Log("[ProbabilityBT]");
			STATUS finalStatus;
			// This means that in the previous execution the choosen node 
			// returned RUNNING
			if(mp_CurrentNode != null)
			{
				finalStatus = mp_CurrentNode.Execute();
				if(finalStatus != STATUS.RUNNING)
					mp_CurrentNode = null;
				return finalStatus;
			}
			
			Init();
			
			float randomWeight = Random.Range(0f, mf_TotalWeigth);
			float weightSum = 0f;
			foreach(KeyValuePair<BehaviorTreeNode, float> weightTuple in md_WeightList)
			{
				weightSum += weightTuple.Value;
				if(randomWeight <= weightSum)
				{
					mp_CurrentNode = weightTuple.Key;
					finalStatus = mp_CurrentNode.Execute();
					if(finalStatus != STATUS.RUNNING)
						mp_CurrentNode = null;
					return finalStatus;
				}
			}
			return STATUS.ERROR;
		}
		#endregion
	
		//--------------------------------------------------
		// Public Methods
		//--------------------------------------------------
		
		#region PUBLIC_METHODS
		public void Add(BehaviorTreeNode btNode, float weight)
		{
			md_WeightList.Add(new KeyValuePair<BehaviorTreeNode, float>(btNode, weight));
			mf_TotalWeigth += weight;
		}
		#endregion
	
		//--------------------------------------------------
		// Private/Protected Methods
		//--------------------------------------------------
	
		#region PRIVATE_PROTECTED_METHODS
		#endregion
	
	}

}