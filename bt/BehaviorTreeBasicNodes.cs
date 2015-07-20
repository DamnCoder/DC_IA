using UnityEngine;
using System.Collections;
using System;

namespace DC.IA.BT
{
	public static class BTNodeFactory
	{
		public static FunctionNode Create(Func<STATUS> function)
		{
			FunctionNode node = new FunctionNode();
			node.AddFunction(function);
			return node;
		}
		
		public static ConditionNode Create(Func<bool> condition)
		{
			ConditionNode node = new ConditionNode();
			node.AddCondition(condition);
			return node;
		}
	}
	
	public class FunctionNode : BehaviorTreeNode
	{
		protected Func<STATUS> function;
		
		public FunctionNode() 
		{
			this.function = delegate { return STATUS.ERROR; };
		}
		
		public void Init () {}
		
		public STATUS Execute ()
		{
			return function();
		}
		
		public BehaviorTreeNode AddFunction(Func<STATUS> function)
		{
			this.function = function;
			return this;
		}
	}
	
	public class ConditionNode : BehaviorTreeNode
	{
		private Func<bool> conditionFunc = delegate { return false; };
		
		public ConditionNode() {}
		
		public void Init () {}
		
		public STATUS Execute ()
		{
			if(conditionFunc()) return STATUS.SUCCESS;

			return STATUS.FAILURE;
		}
		
		public void AddCondition(Func<bool> condition)
		{
			this.conditionFunc = condition;
		}
	}
	
	public class AlwaysRunningNode : FunctionNode
	{
		public AlwaysRunningNode() 
		{
			this.function = delegate { return STATUS.RUNNING; };
		}
	}
	
	public class AlwaysSuccessNode : FunctionNode
	{
		public AlwaysSuccessNode() 
		{
			this.function = delegate { return STATUS.SUCCESS; };
		}
	}
	
	public class AlwaysFailureNode : FunctionNode
	{
		public AlwaysFailureNode() 
		{
			this.function = delegate { return STATUS.FAILURE; };
		}
	}
}