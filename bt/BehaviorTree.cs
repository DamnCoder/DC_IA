using UnityEngine;
using System;
using System.Collections.Generic;

namespace DC.IA.BT
{
	public enum STATUS
	{
		ERROR,
		SUCCESS,
		FAILURE,
		RUNNING,
	}
	
	public interface BehaviorTreeNode
	{
		void Init();
		STATUS Execute();
	}
	
	public abstract class BehaviorTree : BehaviorTreeNode
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
		protected List<BehaviorTreeNode>	ml_Nodes;
		#endregion
	
		//--------------------------------------------------
		// Accessors
		//--------------------------------------------------
		
		#region ACCESSORS
		public int NodeCount
		{ get { return ml_Nodes.Count; } }
		
		public bool HasNodes
		{ get { return 0 < ml_Nodes.Count; } }
		#endregion
	
		//--------------------------------------------------
		// Constructors
		//--------------------------------------------------
		
		#region CONSTRUCTORS
		public BehaviorTree()
		{
			ml_Nodes = new List<BehaviorTreeNode>();
		}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		public virtual void Init()
		{
			//Debug.Log("[BehaviorTree] Init");
			InitNodes();
		}
		
		public abstract STATUS Execute();
		#endregion
	
		//--------------------------------------------------
		// Public Methods
		//--------------------------------------------------
		
		#region PUBLIC_METHODS
		public virtual BehaviorTree Add(BehaviorTreeNode btNode)
		{
			ml_Nodes.Add(btNode);
			return this;
		}
		
		public bool IsSuccess(STATUS status)
		{
			return (status == STATUS.SUCCESS);
		}
		
		public bool IsFailure(STATUS status)
		{
			return (status == STATUS.FAILURE);
		}
		
		public bool IsRunning(STATUS status)
		{
			return (status == STATUS.RUNNING);
		}
		
		public bool IsError(STATUS status)
		{
			return (status == STATUS.ERROR);
		}
		#endregion
	
		//--------------------------------------------------
		// Private/Protected Methods
		//--------------------------------------------------
	
		#region PRIVATE_PROTECTED_METHODS
		protected void InitNodes()
		{
			foreach(BehaviorTreeNode btNode in ml_Nodes)
			{
				btNode.Init();
			}
			
			//Debug.Log("[BehaviorTree] InitNodes");
		}
		#endregion
	
	}
}