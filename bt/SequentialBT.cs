using UnityEngine;
using System.Collections.Generic;

namespace DC.IA.BT
{
///Executes behaviors in order
/** Executes its children in the order they were added.
If the currently executing child returns BT_FAILURE, BT_ERROR, or BT_RUNNING, this returns the same status.
In the case of BT_RUNNING it keeps the state to continue from that node.
If the currently executing child returns BT_SUCCESS, this begins executing the next child, if it exists. 
It continues in this fashion until one of the children returns BT_FAILURE, BT_ERROR, or BT_RUNNING. 
If all children have successfully executed, it will return BT_SUCCESS.
*/
	public class SequentialBT : BehaviorTree 
	{
		protected int mn_CurrentNode;
		
		protected bool AllNodesExecuted
		{ get { return NodeCount <= mn_CurrentNode; } }
		
		protected bool IsInit
		{ get { return -1 < mn_CurrentNode; } }
		
		public SequentialBT() {}
		
		public override void Init ()
		{
			mn_CurrentNode = -1;
		}
		
		public override STATUS Execute ()
		{
			//Debug.Log("[SequentialBT]");
			if(!HasNodes)
			{
				//Debug.Log("[SequentialBT] No hay nodos");
				return STATUS.SUCCESS;
			}
			
			if(!IsInit)
			{
				//Debug.Log("[SequentialBT] No se ha iniciado, se inica");
				InitNodes();
				ResetNodeCounter();
			}
			
			// Ejecuta todos los nodos si devuelven SUCCESS,
			// sino, ejecuta todos hasta que devuelvan FAILURE o RUNNING
			// si devuelve FAILURE, se resetea el bucle y vuelve a empezar
			// si devuelve RUNNING, la ejecución continua desde ese nodo
			STATUS finalStatus;
			do
			{
				if(AllNodesExecuted)
				{
					//Debug.Log("[SequentialBT] Todos los nodos se han ejecutado");
					return AllNodesExecutedAction();
				}
				finalStatus = ml_Nodes[mn_CurrentNode].Execute();
				
			} while(SequenceCondition(finalStatus));
			
			//Debug.Log("[SequentialBT] Acciones de fin de secuencia");
			EndSequenceAction(finalStatus);
			
			return finalStatus;
		}
		
		protected virtual STATUS AllNodesExecutedAction()
		{
			Init();
			//Debug.Log("[SequentialBT] Ejecutados todos los nodos. Devuelvo SUCCESS");
			return STATUS.SUCCESS;
		}
		
		protected virtual bool SequenceCondition(STATUS status)
		{ 
			if(IsSuccess(status))
			{
				//Debug.Log("[SequentialBT] Aumento contador de nodos");
				++mn_CurrentNode;
				return true;
			}
			return false;
		}
		
		protected virtual void EndSequenceAction(STATUS status)
		{
			if(IsFailure(status))
			{
				Init();
				//Debug.Log("[SequentialBT] FAILURE al final, ejecuto Init");
			}
		}
		
		protected void ResetNodeCounter()
		{
			mn_CurrentNode = 0;
		}
	}
}