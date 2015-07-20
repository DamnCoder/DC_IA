/*
 * Created by SharpDevelop.
 * User: jorge.lopez
 * Date: 28/10/2011
 * Time: 10:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace DC.IA.hfsm
{
	/// <summary>
	/// Interfaz comun para todos los estados
	/// de la maquina de estados jerarquica
	/// </summary>
	public class State : IState
	{
		//--------------------------------------------------
		// Special fields
		// In order: (Static/Enums/Constants)
		//--------------------------------------------------
		
		#region STATIC_ENUM_CONSTANTS
		#endregion
		
		//--------------------------------------------------
		// Fields 
		// In order: (Public/Protected/Private)
		//--------------------------------------------------
		
		#region FIELDS
		#endregion
	
		//--------------------------------------------------
		// Accessors
		//--------------------------------------------------
		
		#region ACCESSORS
		public string 			Id
		{ get; set; }
		
		public bool				HasParent
		{ get { return Parent != null; } }
		
		public IState 			Parent
		{ get; set; }

		public ParentType 		GetParent<ParentType>() where ParentType : State
		{ return Parent as ParentType; }
		
		public virtual IState	CurrentState
		{ get { return this; } }
		#endregion

		//--------------------------------------------------
		// Constructors
		//--------------------------------------------------
		
		#region CONSTRUCTORS
		public State(string id)
		{
			this.Id = id;
			this.Parent = null;
		}
		#endregion
		
		//--------------------------------------------------
		// Methods
		// In order: (Public/Protected/Private)
		//--------------------------------------------------

		#region METHODS
		public virtual IState FindInParent(string id) 
		{
			if (!HasParent)
				return null;

			if(Parent.Id == id)
				return Parent;

			return Parent.FindInParent (id);
		}

		public virtual IState Find(string id)
		{
			return null;
		}
		
		public virtual IState Get(string id)
		{
			return null;
		}
		
		public virtual bool Set(string id)
		{
			if(HasParent)
			{
				return Parent.Set(id);
			}
			return false;
		}

		public virtual bool SetInParent(string id)
		{
			if(Parent == null) return false;

			bool success = Parent.Set (id);
			if (!success)
				return Parent.SetInParent (id);
			return success;
		}
		
		public virtual bool SetWithSearch(string id)
		{
			return false;
		}

		public virtual bool SetPreviousState()
		{
			if(HasParent)
				return Parent.SetPreviousState();
			return false;
		}

		public virtual bool Add(IState state)
		{
			return false;
		}
		
		/*
		 * Inicializa todos los objetos
		 */ 
		public virtual void OnCreate()
		{
			//Debug.Log("[HFSM] Creado estado: "+this.Id);
		}
		
		/*
		 * Destruye todos los objetos
		 */ 
		public virtual void OnDestroy()
		{
			//Debug.Log("[HFSM] Destruido estado: "+this.Id);
		}
		
		/*
		 * Inicializa los atributos de los
		 * objetos usados por el estado
		 */ 
		public virtual void OnActivate()
		{
			/*
			if(HasParent)
			{
				if(!Parent.Active)
					Parent.OnActivate();
			}
			*/
			//UnityEngine.Debug.Log("[HFSM] Activado estado: "+this.Id);
		}
		
		/*
		 * Resetea o pausa los objetos
		 * usados por el estado
		 */ 
		public virtual void OnDeactivate()
		{
			//Debug.Log("[HFSM] Desactivado estado: "+this.Id);
		}
		
		/*
		 * Acciones a ejecutar en cada vuelta
		 * del bucle principal
		 */ 
		public virtual void OnUpdate() 
		{}
		
		protected bool SetParentState(string id)
		{
			if(Parent != null)
				return Parent.Set(id);
			return false;
		}
		#endregion
	}
}
