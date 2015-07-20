/*
 * Created by SharpDevelop.
 * User: jorge.lopez
 * Date: 28/10/2011
 * Time: 10:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using UnityEngine;

namespace DC.IA.hfsm
{
	/// <summary>
	/// Interfaz comun para todos los estados
	/// de la maquina de estados jerarquica
	/// </summary>
	public interface IState
	{
		//--------------------------------------------------
		// Constants/Enums
		//--------------------------------------------------
		
		#region CONSTANTS_ENUMS
		#endregion

		//--------------------------------------------------
		// Accessors
		//--------------------------------------------------
		
		#region ACCESSORS
		string	 Id
		{ get; set; }
		
		IState	Parent
		{ get; set; }
		
		IState	CurrentState
		{ get; }
		#endregion

		//--------------------------------------------------
		// Methods
		//--------------------------------------------------
		
		#region METHODS
		IState FindInParent(string id);

		IState Find(string id);
		
		IState Get(string id);
		
		bool Set(string id);

		bool SetInParent(string id);
		
		bool SetWithSearch(string id);

		bool SetPreviousState();
		
		bool Add(IState state);

		/*
		 * Inicializa todos los objetos
		 */ 
		void OnCreate();
		
		/*
		 * Destruye todos los objetos
		 */ 
		void OnDestroy();
		
		/*
		 * Inicializa los atributos de los
		 * objetos usados por el estado
		 */ 
		void OnActivate();
		
		/*
		 * Resetea o pausa los objetos
		 * usados por el estado
		 */ 
		void OnDeactivate();

		void OnUpdate();
		#endregion
	}
}
