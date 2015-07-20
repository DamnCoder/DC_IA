using UnityEngine;
using System;
using System.Collections.Generic;

namespace DC.IA.BT
{
	/// Enumerates the options for when a parallel node is considered to have failed.
	/**
	- FAIL_ON_ONE indicates that the node will return failure as soon as one of its children fails.
	- FAIL_ON_ALL indicates that all of the node's children must fail before it returns failure.

	If FAIL_ON_ONE and SUCEED_ON_ONE are both active and are both trigerred in the same time step, failure will take precedence.
	*/
	public enum FAILURE_POLICY 
	{
		FAIL_ON_ONE = 0,
		FAIL_ON_ALL = 1,
	}
	
	/// Enumerates the options for when a parallel node is considered to have succeeded.
	/**
	- SUCCEED_ON_ONE indicates that the node will return success as soon as one of its children succeeds.
	- SUCCEED_ON_ALL indicates that all of the node's children must succeed before it returns success.
	*/
	public enum SUCCESS_POLICY 
	{
		SUCCEED_ON_ONE = 0,
		SUCCEED_ON_ALL = 1,
	}

	///Execute behaviors in parallel
	/** There are two policies that control the flow of execution. The first is the policy for failure, 
	 * and the second is the policy for success.
	For failure, the options are "fail when one child fails" and "fail when all children fail".
	For success, the options are similarly "complete when one child completes", and "complete when all children complete".
	*/
	public class ParallelBT : BehaviorTree 
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
		private int me_FailurePolicy;
		private int me_SuccessPolicy;
		
		private bool mb_SuccesOnOne = false;
		private bool mb_SuccessOnAll = true;
		
		private bool mb_FailOnOne = false;
		private bool mb_FailOnAll = true;
		
		private List<Func<STATUS>>	ml_FailPolicyCheck;
		private List<Func<STATUS>>	ml_SuccessPolicyCheck;
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
		public ParallelBT(FAILURE_POLICY failurePolicy, SUCCESS_POLICY successPolicy) 
		{
			me_FailurePolicy = (int) failurePolicy;
			me_SuccessPolicy = (int) successPolicy;
			
			ml_FailPolicyCheck =  new List<Func<STATUS>>();
			ml_FailPolicyCheck.Add(FailOnOneCheck);
			ml_FailPolicyCheck.Add(FailOnAllCheck);
			
			ml_SuccessPolicyCheck = new List<Func<STATUS>>();
			ml_SuccessPolicyCheck.Add(SuccessOnOneCheck);
			ml_SuccessPolicyCheck.Add(SuccessOnAllCheck);
		}
		#endregion
	
		//--------------------------------------------------
		// Overrided/Overloaded methods
		//--------------------------------------------------
		
		#region OVERRIDED_OVERLOADED_METHODS
		public override void Init ()
		{
			base.Init();
			mb_SuccesOnOne = false;
			mb_SuccessOnAll = true;
		
			mb_FailOnOne = false;
			mb_FailOnAll = true;
		}
		
		public override STATUS Execute ()
		{
			Debug.Log("[ParallelBT]");
			if(!HasNodes)
			{
				return STATUS.SUCCESS;
			}
			
			STATUS status = STATUS.RUNNING;
			foreach(BehaviorTreeNode btNode in ml_Nodes)
			{
				status = btNode.Execute();
				
				mb_SuccessOnAll &= IsSuccess(status);
				mb_FailOnAll &= IsFailure(status);
				
				mb_SuccesOnOne |= mb_SuccessOnAll;
				mb_FailOnOne |= mb_FailOnAll;
			}
			
			status = CheckPolicies();
			
			return status;
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
		
		private STATUS FailOnOneCheck()
		{
			if(mb_FailOnOne)
			{
				return STATUS.FAILURE;
			}
			return STATUS.RUNNING;
		}
		
		private STATUS SuccessOnOneCheck()
		{
			if(mb_SuccesOnOne)
			{
				return STATUS.SUCCESS;
			}
			return STATUS.RUNNING;
		}
		
		private STATUS FailOnAllCheck()
		{
			if(mb_FailOnAll)
			{
				return STATUS.FAILURE;
			}
			return STATUS.RUNNING;
		}
		
		private STATUS SuccessOnAllCheck()
		{
			if(mb_SuccessOnAll)
			{
				return STATUS.SUCCESS;
			}
			return STATUS.RUNNING;
		}

		private STATUS CheckPolicies()
		{
			STATUS failPolicyStatus = ml_FailPolicyCheck[me_FailurePolicy]();
			if(failPolicyStatus != STATUS.FAILURE)
			{
				STATUS successPolicyStatus = ml_SuccessPolicyCheck[me_SuccessPolicy]();
				return successPolicyStatus;
			}
			
			return STATUS.FAILURE;
		}
		
		#endregion
	
	}

}