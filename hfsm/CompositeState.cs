/*
 * Created by SharpDevelop.
 * User: jorge.lopez
 * Date: 28/10/2011
 * Time: 10:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DC.IA.hfsm
{
	/// <summary>
	/// Description of CompositeState.
	/// </summary>
	public class CompositeState : State
	{
		//--------------------------------------------------
		// Special fields
		// In order: (Static/Enums/Constants)
		//--------------------------------------------------
		
		#region CONSTANTS_ENUMS
		#endregion
		
		//--------------------------------------------------
		// Fields 
		// In order: (Public/Protected/Private)
		//--------------------------------------------------
		
		#region FIELDS
		private Dictionary<string, IState>	md_States;
		
		private LinkedList<IState>			mll_PreviousStates;
		
		private IState						mp_CurrentState;
		private IState						mp_NextState;
		
		private int							mn_MaxPreviousStates;
		#endregion
		
		//--------------------------------------------------
		// Accessors
		//--------------------------------------------------
		
		#region ACCESSORS
		public override IState CurrentState
		{ 
			get 
			{
				if(mp_CurrentState != null)
					return mp_CurrentState.CurrentState;
				
				return this; 
			} 
		}
		
		public int NumSubstates { get{ return md_States.Count; } }
		
		public int MaxPreviousStates { set { mn_MaxPreviousStates = value; } }
		#endregion
		
		//--------------------------------------------------
		// Constructors
		//--------------------------------------------------
		
		#region CONSTRUCTORS
		public CompositeState(string id) : base(id)
		{
			this.md_States = new Dictionary<string, IState>();
			this.mll_PreviousStates = new LinkedList<IState>();
			this.mp_CurrentState = mp_NextState = null;
			this.mn_MaxPreviousStates = 10;
		}
		#endregion
		
		//--------------------------------------------------
		// Overrided methods
		//--------------------------------------------------
		
		#region OVERRIDED_METHODS
		public override void OnDeactivate ()
		{
			if(mp_CurrentState != null)
			{
				mp_CurrentState.OnDeactivate();
				mp_CurrentState = null;
			}
			mp_NextState = null;
			mll_PreviousStates.Clear();
			
			base.OnDeactivate ();
		}
		
		public override void OnDestroy ()
		{
			base.OnDestroy ();
			RemoveAll();
			
			md_States = null;
			mll_PreviousStates = null;
		}

		public override IState Find(string id)
		{
			if(!md_States.ContainsKey(id)) 
			{
				Dictionary<string, IState>.ValueCollection substateList = md_States.Values;
				foreach(State substate in substateList)
				{				
					return substate.Find(id);
				}
			}
			else
			{
				return md_States[id];
			}
			
			return null;
		}
		
		public override IState Get(string id)
		{
			if(md_States.ContainsKey(id))
			{
				return md_States[id];
			}
			return null;
		}
		
		public override bool Set(string newId)
		{
			if(!Contains(newId))
			{
				Debug.LogWarning ("The state "+newId+" doesn't exist!");
				return false;
			}
			
			// If the state exists
			mp_NextState = md_States[newId];
			
			return true;
		}
		
		public override bool SetWithSearch(string id)
		{
			if(Set (id)) return true;
			
			Dictionary<string, IState>.ValueCollection substateList = md_States.Values;
			foreach(State substate in substateList)
			{
				if(substate.SetWithSearch(id))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Si hay estado previo, se coloca como
		/// estado siguiente.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
		public override bool SetPreviousState()
		{
			//Debug.Log ("Estados previos en "+Id+": "+mll_PreviousStates.Count);
			if(mll_PreviousStates.Count == 0)
				return false;
			
			mp_NextState = mll_PreviousStates.First.Value;
			//Debug.Log ("Siguiente estado "+mp_NextState.Id);
			return true;
		}


		public override bool Add(IState state)
		{
			// Si no hay estado, devolvemos false
			if(state == null)
				return false;
			
			// Si ya existe la clave, devolvemos cierto
			if(Contains(state.Id)) 
				return true;
			
			// Añadimos el estado y le mandamos crearse
			md_States.Add(state.Id, state);
			state.Parent = this;
			state.OnCreate();
			
			return true;
		}
		
		public override void OnUpdate()
		{
			if(StateChangeCondition())
			{
				ChangeToNewState();
			}

			if(mp_CurrentState != null)
			{
				mp_CurrentState.OnUpdate();
			}
		}
		#endregion
	
		//--------------------------------------------------
		// Methods
		// In order: (Public/Protected/Private)
		//--------------------------------------------------
		
		#region METHODS
		
		/// <summary>
		/// Eliminamos el estado que se corresponda con el
		/// nombre pasado como argumento.
		/// </summary>
		/// <param name="id">
		/// A <see cref="System.String"/>
		/// </param>
		public void Remove(string id)
		{
			// Si la clave existe
			if(Contains(id))
			{
				var state = md_States[id];
				
				// Desactivamos el estado si era el actual
				if(state == mp_CurrentState)
					state.OnDeactivate();
				
				// Destruimos el estado y lo sacamos de la tabla
				state.OnDestroy();
				md_States.Remove(id);
			}
		}
		
		/// <summary>
		/// Eliminamos todos los estados 
		/// </summary>
		public void RemoveAll()
		{
			// Desactivamos el estado actual, si hay
			if(mp_CurrentState != null)
				mp_CurrentState.OnDeactivate();
			
			// Destroys all the states
			ApplyActionToSubStates(OnDestroyStateAction);
			
			// Clears the states table
			md_States.Clear();
			
			// Clears the previous states
			mll_PreviousStates.Clear();
			
			mp_CurrentState = mp_NextState = null;
		}

		protected void ApplyActionToSubStates(Action<IState> actionOnSubstate)
		{
			var substateList = md_States.Values;
			foreach(State substate in substateList)
			{
				actionOnSubstate(substate);
			}
		}
		
		protected void ApplyActionToSubStates(Func<IState, bool> conditionOnSubstate, Action<IState> actionOnSubstate)
		{
			var substateList = md_States.Values;
			foreach(State substate in substateList)
			{
				if(conditionOnSubstate(substate))
				{
					actionOnSubstate(substate);
				}
			}
		}

		/*
		 * Realiza el cambio de estado y guarda
		 * el anterior
		 */ 
		protected void ChangeToNewState()
		{
			if(mp_CurrentState != null)
			{
				mp_CurrentState.OnDeactivate();
				
				// The current state goes to the previous states queue
				mll_PreviousStates.AddFirst(mp_CurrentState);
				if(mn_MaxPreviousStates < mll_PreviousStates.Count)
				{
					mll_PreviousStates.RemoveLast();
				}
			}
			
			mp_CurrentState = mp_NextState;
			mp_NextState = null;
			
			mp_CurrentState.OnActivate();
		}
		
		private void OnDestroyStateAction(IState state)
		{
			state.OnDestroy();
		}
		
		private bool Contains(string id)
		{
			return md_States.ContainsKey(id);
		}
		
		private bool StateChangeCondition()
		{
			return (mp_NextState != null && mp_CurrentState != mp_NextState);
		}
		#endregion
	}
}
